using TMPro;
using UnityEngine;
using System.Collections;
using Opsive.UltimateCharacterController.Character;

public class InGameData : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Camera;
    [SerializeField] private TMP_Text Timer;
    [SerializeField] private TMP_Text Score;
    [SerializeField] private TMP_Text Kills;
    [SerializeField] private TMP_Text CurrentRound;

    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private GameObject RoundObject;

    [SerializeField] private CharacterLocomotion CharacterLocomotion;

    private float countdownTime = 5f; // 5-second countdown
    private float mainTimerTime = 180f; // 3 minutes in seconds
    private bool timerActive = false;

    void Start()
    {
        CurrentRound.text = "ROUND 1"; // Display current round
        // Start the countdown first
        StartCoroutine(CountdownTimer());
    }



    IEnumerator CountdownTimer()
    {
        CharacterLocomotion.enabled = false; // Disable character movement during countdown
        RoundObject.SetActive(true); // Display the round object
        float currentTime = countdownTime;

        // Countdown Timer for 5 seconds
        while (currentTime > 0)
        {
            Timer.text = $"00:0{Mathf.CeilToInt(currentTime)}"; // Display in 00:05 format
            currentTime -= Time.deltaTime;
            yield return null;
        }
        //Fade Round Object out
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            RoundObject.GetComponent<CanvasGroup>().alpha = i;
            yield return null;
        }

        Timer.text = "00:00";
        yield return new WaitForSeconds(1f); // Wait 1 second before starting the main timer
        // Start the main timer (3 minutes)
        StartCoroutine(MainGameTimer());
    }
    IEnumerator MainGameTimer()
    {
        CharacterLocomotion.enabled = true; // Enable character movement
        float currentTime = mainTimerTime;

        // Main Timer for 3 minutes
        while (currentTime > 0)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            Timer.text = $"{minutes:D2}:{seconds:D2}"; // Display as MM:SS
            currentTime -= Time.deltaTime;
            yield return null;
        }

        Timer.text = "00:00"; // Timer ends
        Debug.Log("Timer Ended!");
    }
}
