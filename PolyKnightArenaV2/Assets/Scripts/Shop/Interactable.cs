using Opsive.UltimateCharacterController.Camera;
using Opsive.UltimateCharacterController.Character;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    [SerializeField] Canvas interactCanvas = null;
    [SerializeField] public TMP_Text interactText = null;
    [SerializeField] private CameraController Camera = null;
    [SerializeField] private CharacterLocomotion CharacterLocomotion = null;

    ES3Settings settings;

    void Start()
    {
        if (settings == null)
        {
            settings = new ES3Settings(Application.persistentDataPath + "/SaveData.es3", ES3.EncryptionType.AES, "K00a03j23s50a25");
            Debug.Log("ES3Settings initialized. Save path: " + Application.persistentDataPath + "/SaveData.es3");
        }
    }
    public void Interact()
    {
        if(gameObject.name == "SM_Prop_StoneTable_01")
        {
            // Define your interaction logic here
            Debug.Log("Interacted with " + gameObject.name);
            interactText.GetComponent<TMP_Text>().text = "to buy new items";
            // Example: open a door, pick up an item, etc.
            //open Canvas
            interactCanvas.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //Freeze the camera
            Camera.GetComponent<CameraController>().enabled = false;
            //Freeze the character
            CharacterLocomotion.GetComponent<CharacterLocomotion>().enabled = false;
        }
        else if(gameObject.name == "SM_Env_Door_01")
        {
            //Change the text in the interactText text object
            interactText.GetComponent<TMP_Text>().text = "to leave the shop";
            PlayerPrefs.SetString("sceneName", "MainMenu");
            SceneManager.LoadScene("LoadingScene");
            // Define your interaction logic here
            Debug.Log("Interacted with " + gameObject.name);
        }

    }
}
