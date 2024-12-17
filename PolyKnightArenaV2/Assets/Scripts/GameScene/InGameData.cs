using TMPro;
using UnityEngine;
using System.Collections;
using Opsive.UltimateCharacterController.Character;

// Spawna AI motståndare. Beroende på rond så ska det spawnas olika typer av fiender. Det ska spawna 2 fiender per gång. Fiender ska spawna under tiden ronden pågår.
// När tiden är slut ska fienderna försvinna.
//Sista ronden ska fienden vara en boss
public class InGameData : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Camera;
    [SerializeField] private TMP_Text Timer; //Färdig

    [SerializeField] private TMP_Text Kills;
    private int currentKills = 0;
    private int death = 0;

    [SerializeField] private TMP_Text CurrentRound;
    private int round = 1;

    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private GameObject RoundObject;
    [SerializeField] private GameObject fadePanel;

    [SerializeField] private CharacterLocomotion CharacterLocomotion;

    private float countdownTime = 5f;
    private float mainTimerTime = 5f; //180
    private bool timerActive = false;

    void Start()
    {
        CurrentRound.text = $"ROUND {round}";
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer()
    {
        RoundObject.SetActive(true);

        // Reset CanvasGroup alpha to fully visible
        CanvasGroup roundCanvasGroup = RoundObject.GetComponent<CanvasGroup>();
        roundCanvasGroup.alpha = 1;

        // Disable CharacterLocomotion to prevent player movement
        CharacterLocomotion.enabled = false;

        float currentTime = countdownTime;

        while (currentTime > 0)
        {
            Timer.text = $"00:0{Mathf.CeilToInt(currentTime)}";
            currentTime -= Time.deltaTime;
            yield return null;
        }

        // Fade Round Object out
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            roundCanvasGroup.alpha = i;
            yield return null;
        }

        Timer.text = "00:00";
        yield return new WaitForSeconds(1f);
        fadePanel.SetActive(false);

        // Re-enable CharacterLocomotion and reset the camera
        CharacterLocomotion.enabled = true;

        // Reinitialize the camera controller to follow the player
        var cameraController = Camera.GetComponent<Opsive.UltimateCharacterController.Camera.CameraController>();
        if (cameraController != null)
        {
            cameraController.Character = Player; // Reattach the camera to the player
            cameraController.PositionImmediately(); // Instantly position the camera
        }

        StartCoroutine(MainGameTimer());
    }

    IEnumerator MainGameTimer()
    {
        float currentTime = mainTimerTime;

        // Main Timer for 3 minutes
        while (currentTime > 0)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            Timer.text = $"{minutes:D2}:{seconds:D2}";
            currentTime -= Time.deltaTime;
            yield return null;
        }
        Timer.text = "00:00";
        Debug.Log("Timer Ended!");
        newRound();
    }
    private void newRound()
    {
        RoundObject.SetActive(false);
        if (round <= 4 && death == 0)
        {
            fadePanel.SetActive(true);
            round++;
            Debug.Log(round);
            Player.transform.position = SpawnPoint.transform.position;
            Player.transform.rotation = SpawnPoint.transform.rotation;
            CurrentRound.text = $"ROUND {round}";
            StartCoroutine(CountdownTimer());
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
