using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSController : MonoBehaviour
{
    private CharacterController _controller;
    private Transform _camera;
    private float _horizontal;
    private float _vertical;
    [SerializeField] private float _playerSpeed = 5;
    [SerializeField] private float _jumpHeight = 1;

    private float _gravity = -9.81f;
    private Vector3 _playerGravity;

    private float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f;

    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private float _sensorRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;
    
    public CheckpointData save;
 void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
        save = GameObject.Find("SaveManager").GetComponent<CheckpointData>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        Movement();
        Jump();
    }
    
    void Movement()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);

        if(direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
            Vector3 moveDirection = Quaternion.Euler(0,targetAngle, 0) * Vector3.forward;
            _controller.Move(moveDirection.normalized * _playerSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);

        if(_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = -2;
        }
        if(_isGrounded && Input.GetButtonDown("Jump"))
        {
            _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }
        _playerGravity.y += _gravity * Time.deltaTime;
        _controller.Move(_playerGravity * Time.deltaTime);
    }
    void UserPosition()
    {
        transform.position = new Vector3 (save.playerPosition.x, save.playerPosition.y, save.playerPosition.z);
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Checkpoint1")
        {
            save.Checkpoint = "1"; 
            save.SaveData();
            Debug.Log("Guardado");
        }

        if(collider.gameObject.tag == "Checkpoint2")
        {
            save.Checkpoint = "2";
            save.SaveData();
            Debug.Log("Guardado");
        }

        if(collider.gameObject.tag == "Checkpoint3")
        {
            save.Checkpoint = "3";
            save.SaveData();
            Debug.Log("Guardado");
        }
    }
}
