using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerMenu : MonoBehaviour
{
    [SerializeField] private Image mapChoice;
    [SerializeField] private List<Sprite> mapImages;

    [SerializeField] private TMP_Text mapName;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    [SerializeField] private Button playButton;

    [SerializeField] private GameObject player;

    private string selectedScene; // Store the selected scene

    public string SelectedScene => selectedScene; // Public property to access selected scene

    public void LinkMapToScene(string mapName)
    {
        switch (mapName)
        {
            case "De_Mirage":
                selectedScene = "Scene_Map_01";
                break;
            case "De_Dust2":
                selectedScene = "Scene_Map_02";
                break;
            case ("Nuketown"):
                selectedScene = "Scene_Map_03";
                break;
            case ("Rust"):
                selectedScene = "Scene_Map_04";
                break;
            default:
                selectedScene = "Scene_Map_01"; // Default to a map
                break;
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetString("sceneName", selectedScene);
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
        InitializeMapSelection();
    }
    private void InitializeMapSelection()
    {
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        previousButton.onClick.AddListener(() => ChangeMap(-1));
        nextButton.onClick.AddListener(() => ChangeMap(1));
    }

    private void ChangeMap(int change)
    {
        CmdChangeMap(change);
    }

    private void CmdChangeMap(int change)
    {
        // Calculate new index
        int index = mapImages.IndexOf(mapChoice.sprite);
        index += change;

        // Wrap around the map selection
        if (index < 0) index = mapImages.Count - 1;
        else if (index >= mapImages.Count) index = 0;

        Debug.Log($"Changing map to index: {index}"); // Debug statement

        // Update the map for all clients
        RpcChangeMap(index);
    }

    private void RpcChangeMap(int index)
    {
        if (index >= 0 && index < mapImages.Count) // Ensure index is valid
        {
            mapChoice.sprite = mapImages[index];
            mapName.text = mapImages[index].name;
            Debug.Log($"Map changed to: {mapImages[index].name}"); // Debug statement

            LinkMapToScene(mapImages[index].name); // Link the map to the scene
        }
        else
        {
            Debug.LogError("Invalid index for map change."); // Debugging error case
        }
    }
}
