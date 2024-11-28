using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomShopTip : MonoBehaviour
{
    [SerializeField]
    private string[] tips = new string[]
    {
        "Tip: Press 'Q' if you want to leave the shop.",
        "Tip: Here you can buy both attachments and perks!",
        "Tip: Press 'Q' if you want to leave the shop.",
        "Tip: Look at the info on products to see what they are capable of.",
        "Tip: Press 'Q' if you want to leave the shop.",
        "Tip: Remember to check the price before buying. You have - "/* + LevelAndCash.Instance.Cash*/,
        "Tip: Press 'Q' if you want to leave the shop.",
    };
    [SerializeField]
    private string[] heroNames = new string[]
    {
        "Sylvanna",
        "Gundric",
        "Grokkar",
        "Titanix",
        "Brandor",
        "Marinda",
        "Brungor",
        "Nymira"
    };
    [SerializeField] private Image ImageHero;
    [SerializeField] private Sprite[] Heroes;
    [SerializeField] private TMP_Text HeroName;
    [SerializeField] private TMP_Text Tip;
    [SerializeField] private GameObject TipPanel;

    private void Start()
    {
        // Start the tips cycle
        StartCoroutine(ShowTips());
    }

    private IEnumerator ShowTips()
    {
        while (true)
        {
            // Show the tip panel
            TipPanel.SetActive(true);

            // Update the tip and hero
            RandomTipText();
            RandomHeroImage();

            // Wait for 10 seconds
            yield return new WaitForSeconds(300);

            // Hide the tip panel
            TipPanel.SetActive(false);

            // Wait for 10 seconds before showing the next tip
            yield return new WaitForSeconds(300);
        }
    }

    public void RandomTipText()
    {
        int randomTip = Random.Range(0, tips.Length);
        Tip.text = tips[randomTip];
    }

    public void RandomHeroImage()
    {
        int randomHero = Random.Range(0, Heroes.Length);
        ImageHero.sprite = Heroes[randomHero];

        // Match hero name based on sprite
        if (ImageHero.sprite.name.Contains("Sylvanna"))
        {
            HeroName.text = heroNames[0];
        }
        else if (ImageHero.sprite.name.Contains("Gundric"))
        {
            HeroName.text = heroNames[1];
        }
        else if (ImageHero.sprite.name.Contains("Grokkar"))
        {
            HeroName.text = heroNames[2];
        }
        else if (ImageHero.sprite.name.Contains("Titanix"))
        {
            HeroName.text = heroNames[3];
        }
        else if (ImageHero.sprite.name.Contains("Brandor"))
        {
            HeroName.text = heroNames[4];
        }
        else if (ImageHero.sprite.name.Contains("Marinda"))
        {
            HeroName.text = heroNames[5];
        }
        else if (ImageHero.sprite.name.Contains("Brungor"))
        {
            HeroName.text = heroNames[6];
        }
        else if (ImageHero.sprite.name.Contains("Nymira"))
        {
            HeroName.text = heroNames[7];
        }
    }
}
