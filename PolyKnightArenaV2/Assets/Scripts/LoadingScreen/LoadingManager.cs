using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor.Rendering;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    void Start()
    {
        LoadScene(PlayerPrefs.GetString("sceneName"));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadTargetScene(sceneName));
    }

    private IEnumerator LoadTargetScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        PlayerPrefs.DeleteKey("sceneName");

        // Prevent the scene from activating automatically
        operation.allowSceneActivation = false;

        // Update the progress bar as the scene loads (0.0 to 0.9)
        while (operation.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // Normalize to 0-1
            progressBar.value = progress;

            yield return null;
        }

        // The scene is loaded, but activation is pending. Fill the slider to 1.0
        progressBar.value = 1.0f;

        // Optional: Wait for player input or delay
        yield return new WaitForSeconds(1.0f); // Add a small delay for smoothness
        while (!Input.anyKey) // Wait for a key press (or skip this if automatic)
        {
            yield return null;
        }

        // Allow the scene to activate
        operation.allowSceneActivation = true;
    }

}
