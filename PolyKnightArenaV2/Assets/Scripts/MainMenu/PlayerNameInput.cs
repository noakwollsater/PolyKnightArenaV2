using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInputField = null;

    public static string DisplayName { get; private set; }  // Static to persist across scenes but reset on game restart

    private void Start()
    {
        // If no name is set, show the name input UI
        SetUpInputField();
        if (!string.IsNullOrEmpty(DisplayName))
        {
            nameInputField.text = DisplayName;
        }
        else if (PlayerPrefs.HasKey("PlayerName"))
        {
            DisplayName = PlayerPrefs.GetString("PlayerName");
            nameInputField.text = DisplayName;
        }
        else
        {
            // Optionally set placeholder text
            nameInputField.text = "Player";
        }
    }

    private void SetUpInputField()
    {
        // Add listener to enable the button when input is not empty
        nameInputField.onValueChanged.AddListener(SavePlayerName);
    }

    public void SavePlayerName(string name)
    {
        // Set the DisplayName to the input field text (this will persist across scenes)
        DisplayName = nameInputField.text;
        PlayerPrefs.SetString("PlayerName", DisplayName);

        Debug.Log("PlayerNameInput.SavePlayerName() called, DisplayName set to: " + DisplayName);
    }
}
