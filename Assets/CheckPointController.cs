using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointController : MonoBehaviour
{

    public Text checkText;

    private void Awake() {
        CargarCheckpoint();
        CheckpointText();
    }

    private void CargarCheckpoint(){
        transform.position = new Vector3(PlayerPrefs.GetFloat("PosicionX", 0), PlayerPrefs.GetFloat("PosicionY", 0), PlayerPrefs.GetFloat("PosicionZ", 0));
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("Checkpoint")){
            PlayerPrefs.SetFloat("PosicionX", other.transform.position.x);
            PlayerPrefs.SetFloat("PosicionY", other.transform.position.y);
            PlayerPrefs.SetFloat("PosicionZ", other.transform.position.z);

            PlayerPrefs.SetString("CheckpointName", other.gameObject.GetComponent<CheckjPointsProperties>().name);
            CheckpointText();
        }
    }

    private void CheckpointText(){
        checkText.text = PlayerPrefs.GetString("CheckpointName", "Ninguno :)");
    }
}
