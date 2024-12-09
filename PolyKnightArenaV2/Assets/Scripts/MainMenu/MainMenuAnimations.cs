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
    [SerializeField] private float transitionDuration = 0.8f;

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

        PlayButton.onClick.AddListener(() => StartCoroutine(MoveCameraCinematic(
             new Vector3(77.05f, 7.09f, -54.94f),    // Target Position
             Quaternion.Euler(9, -299, 0),          // Target Rotation
             MainMenuPanel,
             PlayMenuPanel,
             "Animations/IdleSit"
        )));
    }

    private IEnumerator MoveCameraCinematic(Vector3 targetPosition, Quaternion targetRotation, GameObject deactivatePanel, GameObject activatePanel, string animationPath)
    {
        Vector3 startPosition = Camera.transform.position;
        Quaternion startRotation = Camera.transform.rotation;
        float elapsedTime = 0;

        // Camera Zoom Settings
        float startFOV = Camera.fieldOfView;  // Starting Field of View
        float targetFOV = 50f;                // Cinematic Zoom Target

        deactivatePanel.SetActive(false);

        // Start Camera Shake Variables
        float shakeMagnitude = 0.1f; // Small shake effect
        transitionDuration = 1.5f;   // Longer duration for cinematic effect
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(animationPath) as RuntimeAnimatorController;
        Player.transform.position = new Vector3(80.096f, 5.323f, -54.375f);
        Player.transform.rotation = Quaternion.Euler(0, 257.19f, 0);
        Player.GetComponent<RotateCharacter>().enabled = false;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsedTime / transitionDuration); // Smooth easing

            // Move Camera in an Arc Path (Cinematic effect)
            Vector3 curveOffset = Vector3.up * Mathf.Sin(t * Mathf.PI) * 1.0f; // Arc-like movement
            Camera.transform.position = Vector3.Lerp(startPosition, targetPosition, t) + curveOffset;

            // Apply Rotation Smoothly
            Camera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            // Add Zoom Effect
            Camera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

            // Optional: Add Shake Effect Near End of Transition
            if (t > 0.7f)
            {
                Camera.transform.position += Random.insideUnitSphere * shakeMagnitude * (1 - t);
            }

            yield return null;
        }

        // Finalize Camera Position and Settings
        Camera.transform.position = targetPosition;
        Camera.transform.rotation = targetRotation;
        Camera.fieldOfView = targetFOV;

        activatePanel.SetActive(true);


        foreach (Button button in BackButtons)
        {
            button.onClick.AddListener(() => StartCoroutine(MoveCameraBackCinematic()));
        }
    }

    private IEnumerator MoveCamera(Vector3 targetPosition, Quaternion targetRotation, GameObject deactivatePanel, GameObject activatePanel, string animationPath)
    {
        Vector3 startPosition = Camera.transform.position;
        Quaternion startRotation = Camera.transform.rotation;
        float elapsedTime = 0;

        deactivatePanel.SetActive(false);

        // Custom behavior for Play Menu
        float customTransitionDuration = transitionDuration; // Default duration
        if (activatePanel == PlayMenuPanel)
        {
            customTransitionDuration = 2.0f; // Slower duration for Play Menu
            
        }

        while (elapsedTime < customTransitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / customTransitionDuration;

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

    private IEnumerator MoveCameraBackCinematic()
    {
        Vector3 targetPosition = new Vector3(77.05f, 6.68f, -56.13f);
        Quaternion targetRotation = Quaternion.Euler(0, 235.075f, 0);

        Vector3 startPosition = Camera.transform.position;
        Quaternion startRotation = Camera.transform.rotation;
        float elapsedTime = 0;

        float startFOV = Camera.fieldOfView;  // Current FOV
        float targetFOV = 60f;               // Standard FOV to reset

        float shakeMagnitude = 0.1f;         // Small shake effect near the end

        // Disable the active panel
        PlayMenuPanel.SetActive(false);

        transitionDuration = 1.5f; // Set a cinematic duration for the return transition
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/Idlebored") as RuntimeAnimatorController;
        Player.transform.position = new Vector3(74.214f, 5.060f, -57.1f);
        Player.transform.rotation = Quaternion.Euler(0, 74.712f, 0);
        Player.GetComponent<RotateCharacter>().enabled = true;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsedTime / transitionDuration); // Smooth easing

            // Add Arc Motion (up and down effect)
            Vector3 curveOffset = Vector3.up * Mathf.Sin(t * Mathf.PI) * 0.8f;
            Camera.transform.position = Vector3.Lerp(startPosition, targetPosition, t) + curveOffset;

            // Apply Smooth Rotation
            Camera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            // Gradually Reset Field of View
            Camera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

            // Add Camera Shake Near End
            if (t > 0.7f)
            {
                Camera.transform.position += Random.insideUnitSphere * shakeMagnitude * (1 - t);
            }

            yield return null;
        }

        // Finalize Camera Position, Rotation, and FOV
        Camera.transform.position = targetPosition;
        Camera.transform.rotation = targetRotation;
        Camera.fieldOfView = targetFOV;

        // Reactivate Main Menu Panel
        MainMenuPanel.SetActive(true);


        // Clean up back button listeners if needed
        foreach (Button button in BackButtons)
        {
            button.onClick.RemoveListener(() => StartCoroutine(MoveCameraBackCinematic()));
        }
    }

}
