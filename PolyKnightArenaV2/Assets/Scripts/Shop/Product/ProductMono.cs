using UnityEngine;
using UnityEngine.UI;

public class ProductMono : MonoBehaviour
{
    [SerializeField] private Button openinfoButton;
    [SerializeField] private GameObject infoPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openinfoButton.onClick.AddListener(OpenInfo);
    }

    private void OpenInfo()
    {
        infoPanel.SetActive(true);

        openinfoButton.onClick.RemoveListener(OpenInfo);
        openinfoButton.onClick.AddListener(CloseInfo);
    }

    private void CloseInfo()
    {
        infoPanel.SetActive(false);

        openinfoButton.onClick.RemoveListener(CloseInfo);
        openinfoButton.onClick.AddListener(OpenInfo);
    }
}
