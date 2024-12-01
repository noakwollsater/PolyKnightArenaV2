using UnityEngine;
using UnityEngine.UI;

public class ProductMono : MonoBehaviour
{
    [SerializeField] public Button openinfoButton;
    [SerializeField] public GameObject infoPanel;

    // Static reference to the currently active infoPanel
    private static GameObject activeInfoPanel = null;

    void Start()
    {
        if (openinfoButton == null)
        {
            Debug.LogError("openinfoButton is not assigned.");
            return;
        }

        if (infoPanel == null)
        {
            Debug.LogError("infoPanel is not assigned.");
            return;
        }

        openinfoButton.onClick.AddListener(OpenInfo);
        Debug.Log("openinfoButton listener assigned.");
    }

    private void OpenInfo()
    {
        // Close the currently active panel if there is one
        if (activeInfoPanel != null && activeInfoPanel != infoPanel)
        {
            activeInfoPanel.SetActive(false);
        }

        // Activate the current panel
        infoPanel.SetActive(true);
        activeInfoPanel = infoPanel;

        // Get the position of the button
        Transform buttonRect = openinfoButton.GetComponent<Transform>();
        Transform infoPanelRect = infoPanel.GetComponent<Transform>();

        if (buttonRect != null && infoPanelRect != null)
        {
            // Position the infoPanel near the button
            infoPanelRect.position = buttonRect.position;
            infoPanelRect.position += new Vector3(348, 200, 0);

            // Adjust position if it exceeds certain bounds
            if (infoPanelRect.position.y > 800)
            {
                infoPanelRect.position += new Vector3(0, -300, 0);
            }
            if (infoPanelRect.position.x > 1800)
            {
                infoPanelRect.position += new Vector3(-570, 0, 0);
            }
        }

        openinfoButton.onClick.RemoveListener(OpenInfo);
        openinfoButton.onClick.AddListener(CloseInfo);
    }

    private void CloseInfo()
    {
        // Deactivate the panel
        infoPanel.SetActive(false);

        // Clear the static reference if this was the active panel
        if (activeInfoPanel == infoPanel)
        {
            activeInfoPanel = null;
        }

        openinfoButton.onClick.RemoveListener(CloseInfo);
        openinfoButton.onClick.AddListener(OpenInfo);
    }
}
