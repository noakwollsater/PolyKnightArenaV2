using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimations : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject PlayMenuPanel;
    [SerializeField] private GameObject SettingsMenuPanel;
    [SerializeField] private GameObject CharacterMenuPanel;

    [SerializeField] private Button PlayButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button CharacterButton;
    [SerializeField] private Button backtoCustomize;
    [SerializeField] private Button[] BackButtons;
    [SerializeField] private Button ColorButton;

    [SerializeField] private GameObject Player;
    [SerializeField] private float transitionDuration = 1.5f;

    void Start()
    {
        CharacterButton.onClick.AddListener(() => StartCoroutine(MoveCamera(
            new Vector3(75.815f, 6.369f, -56.687f),
            Quaternion.Euler(0, 245, 0),
            MainMenuPanel,
            CharacterMenuPanel,
            "Animations/CharacterCustomize"
        )));
        ColorButton.onClick.AddListener(() => StartCoroutine(MoveCamera(
            new Vector3(75.815f, 6.369f, -56.687f),
            Quaternion.Euler(0, 228, 0),
            MainMenuPanel,
            CharacterMenuPanel,
            "Animations/CharacterCustomize"
       )));
       backtoCustomize.onClick.AddListener(() => StartCoroutine(MoveCamera(
            new Vector3(75.815f, 6.369f, -56.687f),
            Quaternion.Euler(0, 245, 0),
            MainMenuPanel,
            CharacterMenuPanel,
            "Animations/CharacterCustomize"
        )));
    }

    private IEnumerator MoveCamera(Vector3 targetPosition, Quaternion targetRotation, GameObject deactivatePanel, GameObject activatePanel, string animationPath)
    {
        Vector3 startPosition = Camera.transform.position;
        Quaternion startRotation = Camera.transform.rotation;
        float elapsedTime = 0;

        deactivatePanel.SetActive(false);

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            Camera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            Camera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            yield return null;
        }

        Camera.transform.position = targetPosition;
        Camera.transform.rotation = targetRotation;

        activatePanel.SetActive(true);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(animationPath) as RuntimeAnimatorController;

        foreach (Button button in BackButtons)
        {
            button.onClick.AddListener(() => StartCoroutine(MoveCameraBack()));
        }
    }

    private IEnumerator MoveCameraBack()
    {
        Vector3 targetPosition = new Vector3(77.05f, 6.68f, -56.13f);
        Quaternion targetRotation = Quaternion.Euler(0, 235.075f, 0);

        Vector3 startPosition = Camera.transform.position;
        Quaternion startRotation = Camera.transform.rotation;
        float elapsedTime = 0;

        CharacterMenuPanel.SetActive(false);

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            Camera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            Camera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            yield return null;
        }

        Camera.transform.position = targetPosition;
        Camera.transform.rotation = targetRotation;

        MainMenuPanel.SetActive(true);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/Idlebored") as RuntimeAnimatorController;

        foreach (Button button in BackButtons)
        {
            button.onClick.RemoveListener(() => StartCoroutine(MoveCameraBack()));
        }
    }
}
