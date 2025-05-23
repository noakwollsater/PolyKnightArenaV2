using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Button EndGameBtn;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text Coins;
    [SerializeField] private TMP_Text XP;


    void Start()
    {
        EndGameBtn.onClick.AddListener(() =>
        {
            PlayerPrefs.SetString("sceneName", "MainMenu");
            SceneManager.LoadScene("LoadingScene");
        });
    }

    public void CountScore()
    {
        int kills = PlayerPrefs.GetInt("kills");
        int baseScore = PlayerPrefs.GetInt("baseScore");
        int score = PlayerPrefs.GetInt("score");
        // Ber�kna bonuspo�ng f�r kills
        int killBonus = kills * 20; // Varje kill ger 20 po�ng
        int totalScore = baseScore + killBonus + score;

        // Coins och XP baserat p� score
        int coins = (int)(totalScore * 0.1); // 10% av score omvandlas till coins
        int xp = (int)(totalScore * 0.2);    // 20% av score blir XP

        // Uppdatera UI
        ScoreText.text = $"Score: {totalScore}";
        Coins.text = coins.ToString();
        XP.text = xp.ToString();
        PlayerPrefs.DeleteKey("dead");
        PlayerPrefs.DeleteKey("kills");
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.DeleteKey("baseScore");
    }
}
