using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class InGamePlayerStats : MonoBehaviour
{
    [SerializeField] private InGameData inGameData;

    public int dead;

    [SerializeField] private Slider hp;
    [SerializeField] private TMP_Text hpText;
    private int maxHp;
    private int currentHp;

    [SerializeField] private Slider shield;
    [SerializeField] private TMP_Text shieldText;
    private int maxShield;
    private int currentShield;

    [SerializeField] private TMP_Text killsText;
    private int kills = 0;

    [SerializeField] private TMP_Text scoreText;
    private int score = 0;

    void Start()
    {
        maxHp = PlayerPrefs.GetInt("characterHP", 100);
        hp.maxValue = maxHp;
        currentHp = maxHp;
        hpText.text = currentHp.ToString() + "/" + maxHp.ToString();

        maxShield = PlayerPrefs.GetInt("characterShield", 50);
        shield.maxValue = maxShield;
        currentShield = maxShield;
        shieldText.text = currentShield.ToString() + "/" + maxShield.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dead++;
            PlayerPrefs.SetInt("dead", dead);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            kills++;
            if (inGameData.round == 1)
            {
                score += 100;
            }
            else if(inGameData.round == 2)
            {
                score += 125;
            }
            else if(inGameData.round == 3)
            {
                score += 175;
            }
            else if(inGameData.round == 4)
            {
                score += 250;
            }
            else if(inGameData.round == 5)
            {
                score += 500;
            }

            PlayerPrefs.SetInt("kills", kills);
            PlayerPrefs.SetInt("score", score);
            killsText.text = kills.ToString();
            scoreText.text = score.ToString();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            currentHp -= 10;
            hp.value = currentHp;
            hpText.text = currentHp.ToString() + "/" + maxHp.ToString();
            if (currentHp <= 0)
            {
                dead++;
                PlayerPrefs.SetInt("dead", dead);
            }
        }
    }
}
