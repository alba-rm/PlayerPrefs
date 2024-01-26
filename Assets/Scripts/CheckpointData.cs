using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointData : MonoBehaviour
{
    [SerializeField] private Text CheckpointText;
    [SerializeField] public string Checkpoint;
    [SerializeField] public Vector3 playerPosition;
    public TPSController controller;
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<TPSController>();
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = controller.transform.position;
    }
    public void SaveData()
    {
        PlayerPrefs.SetString("Checkpoint", Checkpoint);
        PlayerPrefs.SetFloat("positionX", controller.transform.position.x);
        PlayerPrefs.SetFloat("positionY", controller.transform.position.y);
        PlayerPrefs.SetFloat("positionZ", controller.transform.position.z);
        LoadData();
    }
    void LoadData()
    {
        CheckpointText.text = "Checkpoint: " + PlayerPrefs.GetString("Checkpoint", Checkpoint);
        playerPosition = new Vector3(PlayerPrefs.GetFloat("positionX"), PlayerPrefs.GetFloat("positionY"), PlayerPrefs.GetFloat("positionZ"));
        controller.transform.position = playerPosition;
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
