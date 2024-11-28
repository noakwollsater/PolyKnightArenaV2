using UnityEngine;
using UnityEngine.UI;

public class PartSelection : MonoBehaviour
{
    [SerializeField] private GameObject headwearerPanel;

    [SerializeField] private Button headbtn;
    [SerializeField] private Button firstWearbtn;
    [SerializeField] private Button secondWearbtn;

    [SerializeField] private Image headImage;
    [SerializeField] private Image firstWearImage;
    [SerializeField] private Image secondWearImage;

    [SerializeField] private Sprite hairSprite;
    [SerializeField] private Sprite helmetSprite;
    [SerializeField] private Sprite hatSprite;

    public bool isHelmet;
    public bool isHair;
    public bool isHat;


    [SerializeField] private GameObject facialwearerPanel;

    [SerializeField] private Button facialbtn;
    [SerializeField] private Button facialFirstWearbtn;

    [SerializeField] private Image facialImage;
    [SerializeField] private Image facialFirstWearImage;

    [SerializeField] private Sprite facialHairSprite;
    [SerializeField] private Sprite facialMaskSprite;

    public bool isFacialHair;
    public bool isMask;

    void Start()
    {
        // Initialize the state based on the current headImage sprite


        // Add button listeners once
        headbtn.onClick.AddListener(SelectHead);
        firstWearbtn.onClick.AddListener(SelectFirstWear);
        secondWearbtn.onClick.AddListener(SelectSecondWear);

        facialbtn.onClick.AddListener(SelectFacial);
        facialFirstWearbtn.onClick.AddListener(SelectFacialFirstWear);
    }

    void Update()
    {
        if (headImage.sprite.name.Contains("Hair"))
        {
            isHair = true;
            isHelmet = false;
            isHat = false;
        }
        else if (headImage.sprite.name.Contains("Helmet"))
        {
            isHelmet = true;
            isHat = false;
            isHair = false;
        }
        else if (headImage.sprite.name.Contains("Hat"))
        {
            isHat = true;
            isHelmet = false;
            isHair = false;
        }

        if (facialImage.sprite.name.Contains("Beard"))
        {
            isFacialHair = true;
        }
        else if (facialImage.sprite.name.Contains("Mask"))
        {
            isMask = true;
        }
    }

    void SelectHead()
    {
        // Toggle the headwearerPanel visibility
        headwearerPanel.SetActive(!headwearerPanel.activeSelf);
    }

    void SelectFirstWear()
    {
        Debug.Log("SelectFirstWear called.");
        Debug.Log($"Before: isHair={isHair}, isHelmet={isHelmet}, isHat={isHat}");

        if (isHair)
        {
            // Swap headImage and firstWearImage sprites
            Sprite tempSprite = headImage.sprite;
            headImage.sprite = firstWearImage.sprite;
            firstWearImage.sprite = tempSprite;
        }
        if (isHelmet)
        {
            // Swap headImage and firstWearImage sprites
            Sprite tempSprite = headImage.sprite;
            headImage.sprite = firstWearImage.sprite;
            firstWearImage.sprite = tempSprite;
        }
        if (isHat)
        {
            // Swap headImage and firstWearImage sprites
            Sprite tempSprite = headImage.sprite;
            headImage.sprite = firstWearImage.sprite;
            firstWearImage.sprite = tempSprite;
        }

        Debug.Log($"After: isHair={isHair}, isHelmet={isHelmet}, isHat={isHat}");
        headwearerPanel.SetActive(false);
    }

    void SelectSecondWear()
    {
        Debug.Log("SelectSecondWear called.");
        Debug.Log($"Before: isHair={isHair}, isHelmet={isHelmet}, isHat={isHat}");

        if (isHair)
        {
            // Swap headImage and secondWearImage sprites
            Sprite tempSprite = headImage.sprite;
            headImage.sprite = secondWearImage.sprite;
            secondWearImage.sprite = tempSprite;
        }
        else if (isHelmet)
        {
            // Swap headImage and secondWearImage sprites
            Sprite tempSprite = headImage.sprite;
            headImage.sprite = secondWearImage.sprite;
            secondWearImage.sprite = tempSprite;
        }
        else if (isHat)
        {
            // Swap headImage and secondWearImage sprites
            Sprite tempSprite = headImage.sprite;
            headImage.sprite = secondWearImage.sprite;
            secondWearImage.sprite = tempSprite;
        }

        Debug.Log($"After: isHair={isHair}, isHelmet={isHelmet}, isHat={isHat}");
        headwearerPanel.SetActive(false);
    }

    void SelectFacial()
    {
        // Toggle the facialwearerPanel visibility
        facialwearerPanel.SetActive(!facialwearerPanel.activeSelf);
    }

    void SelectFacialFirstWear()
    {
        if (isFacialHair)
        {
            facialImage.sprite = facialFirstWearImage.sprite;
            facialFirstWearImage.sprite = facialHairSprite;
            isMask = true;
            isFacialHair = false;
        }
        else if (isMask)
        {
            facialImage.sprite = facialFirstWearImage.sprite;
            facialFirstWearImage.sprite = facialMaskSprite;
            isFacialHair = true;
            isMask = false;
        }
        facialwearerPanel.SetActive(false);
    }
}
