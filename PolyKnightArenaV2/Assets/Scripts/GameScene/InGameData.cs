using TMPro;
using UnityEngine;
using System.Collections;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Camera;
using Opsive.UltimateCharacterController.ThirdPersonController.Camera;
using UnityEngine.UI;

// Spawna AI motståndare. Beroende på rond så ska det spawnas olika typer av fiender. Det ska spawna 2 fiender per gång. Fiender ska spawna under tiden ronden pågår.
// När tiden är slut ska fienderna försvinna.
//Sista ronden ska fienden vara en boss
public class InGameData : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private GameObject instantiatedPlayer;
    [SerializeField] private GameObject Camera;
    [SerializeField] private TMP_Text Timer; //Färdig

    private int death = 0;

    [SerializeField] private TMP_Text CurrentRound;
    public int round = 1;
    private bool isOver;

    public int baseScore;

    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private GameObject RoundObject;
    [SerializeField] private GameObject fadePanel;

    [SerializeField] private CameraController cameraController;
    [SerializeField] private CharacterLocomotion characterLocomotion;
    [SerializeField] private ObjectFader objectFader;
    [SerializeField] private EndGame endGame;

    [SerializeField] private Canvas playerCanva;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject EndGamePanel;

    [SerializeField] private Button restartBtn;

    private float countdownTime = 5f;
    private float mainTimerTime = 5f; //180
    private bool timerActive = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CurrentRound.text = $"ROUND {round}";
        StartCoroutine(CountdownTimer());

        restartBtn.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteKey("dead");
            PlayerPrefs.DeleteKey("kills");
            PlayerPrefs.DeleteKey("score");
            Application.LoadLevel(Application.loadedLevel);
        });
    }

    IEnumerator CountdownTimer()
    {
        if (!isOver)
        {
            // Instantiate the player and reset position
            instantiatedPlayer = Instantiate(Player);
            instantiatedPlayer.transform.position = SpawnPoint.transform.position;
            instantiatedPlayer.transform.rotation = SpawnPoint.transform.rotation;

            // Update the CameraController to follow the newly instantiated player
            if (cameraController != null)
            {
                cameraController.Character = instantiatedPlayer;
            }
            characterLocomotion = instantiatedPlayer.GetComponent<CharacterLocomotion>();
            characterLocomotion.enabled = false;
            RoundObject.SetActive(true);

            // Reset CanvasGroup alpha to fully visible
            CanvasGroup roundCanvasGroup = RoundObject.GetComponent<CanvasGroup>();
            roundCanvasGroup.alpha = 1;

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

            StartCoroutine(MainGameTimer());
        }
    }

    IEnumerator MainGameTimer()
    {
        // Definiera poäng per runda
        int[] roundScores = { 100, 125, 175, 250, 500 }; // Poäng för varje runda
        for (int i = 0; i < round && i < roundScores.Length; i++)
        {
            baseScore += roundScores[i];
        }
        PlayerPrefs.SetInt("baseScore", baseScore);

        characterLocomotion.enabled = true;
        float currentTime = mainTimerTime;

        // Main Timer for 3 minutes
        while (currentTime > 0)
        {
            death = PlayerPrefs.GetInt("dead");
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            Timer.text = $"{minutes:D2}:{seconds:D2}";
            currentTime -= Time.deltaTime;
            yield return null;
            if (death != 0)
            {
                newRound();
            }
        }
        Timer.text = "00:00";
        Debug.Log("Timer Ended!");
        Destroy(instantiatedPlayer);
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
        cameraController.enabled = false;
        characterLocomotion.enabled = false;
        objectFader.enabled = false;
        isOver = true;
        playerCanva.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (death > 0)
        {
            endGame.CountScore();
            deathPanel.SetActive(true);
        }
        else
        {
            endGame.CountScore();
            EndGamePanel.SetActive(true);
        }
        Debug.Log("Game Over!");
    }
}
