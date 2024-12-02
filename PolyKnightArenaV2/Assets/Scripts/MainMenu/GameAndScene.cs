using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameAndScene : MonoBehaviour
{
    [SerializeField] private Button Shopbtn;
    [SerializeField] private Button Quitbtn;

    public static GameAndScene instance { get; private set; }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Shopbtn.onClick.AddListener(LoadScene);
        Quitbtn.onClick.AddListener(QuitGame);
    }

    private void LoadScene()
    {
        PlayerPrefs.SetString("sceneName", "Shop");
        SceneManager.LoadScene("LoadingScene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
