using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomTip : MonoBehaviour
{
    [SerializeField] private string[] tips = new string[]
    {
        "Tip: You can buy Perks and Attachments in the shop",
        "Tip: Your fighting depends on your gear.",
        "Tip: Different weapons different fighting style.",
        "Tip: Kick him in the nuts, dont work in this game.",
        "Tip: Eaither you're the sheep or the wolf.",
        "Tip: Only one can win, it dont matter if its your bro."
    };

    [SerializeField] private Image ImageHero;
    [SerializeField] private Sprite[] Heroes;
    [SerializeField] private TMP_Text HeroName;
    [SerializeField] private TMP_Text Tip;

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

    void Start()
    {
        RandomTipText();
        RandomHeroImage();
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
