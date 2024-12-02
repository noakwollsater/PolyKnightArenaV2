using Opsive.UltimateCharacterController.Camera;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Traits;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopScene : MonoBehaviour
{
    [SerializeField] private CameraController Camera;
    [SerializeField] private CharacterLocomotion CharacterLocomotion;


    [SerializeField] private TMP_Text Level;
    [SerializeField] private TMP_Text Coins;

    // Start is called before the first frame update
    [SerializeField] Canvas ShopUI;
    [SerializeField] Canvas SettingsUI;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CloseUI();
        DisplayInfo();
    }

    private void DisplayInfo()
    {
        int level = LevelAndCash.Instance.Level;
        int cash = LevelAndCash.Instance.Cash;
        Level.text = "Level: " + level;
        Coins.text = "Coins: " + $"<color=green>{cash}</color>";
    }

    void CloseUI()
    {
        //Press tab or esq to close the n
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShopUI.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Camera.GetComponent<CameraController>().enabled = true;
            CharacterLocomotion.GetComponent<CharacterLocomotion>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsUI.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Camera.GetComponent<CameraController>().enabled = true;
            CharacterLocomotion.GetComponent<CharacterLocomotion>().enabled = true;
        }
    }
}
