using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointData : MonoBehaviour
{
    [SerializeField] private Text CheckpointText;
    [SerializeField] public string Checkpoint;
    [SerializeField] public Vector3 userPosition;
    private CharacterController controller;
    TPSController player;
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<CharacterController>();
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        userPosition = transform.position;
    }
    public void SaveData()
    {
        PlayerPrefs.SetString("Checkpoint", Checkpoint);
        PlayerPrefs.SetFloat("positionX", userPosition.x);
        PlayerPrefs.SetFloat("positionY", userPosition.y);
        PlayerPrefs.SetFloat("positionZ", userPosition.z);
        LoadData();
    }
    void LoadData()
    {
        CheckpointText.text = "Checkpoint: " + PlayerPrefs.GetString("Checkpoint", Checkpoint);
        userPosition = new Vector3(PlayerPrefs.GetFloat("positionX",0), PlayerPrefs.GetFloat("positionY",0), PlayerPrefs.GetFloat("positionZ",0));
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("Checkpoint");
        PlayerPrefs.DeleteKey("positionX");
        PlayerPrefs.DeleteKey("positionY");
        PlayerPrefs.DeleteKey("positionZ");
        LoadData();
    }
}
