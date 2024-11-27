using UnityEngine;
using UnityEngine.UI;

public class GenderSelection : MonoBehaviour
{
    [SerializeField] private Button genderBtn;  // Button to toggle gender
    [SerializeField] private Image genderImage; // Image to display the selected gender
    [SerializeField] private Sprite[] genders;  // Array of gender icons [0 = Female, 1 = Male]

    [SerializeField] private GameObject player; // Reference to the player object
    [SerializeField] private TransformCamera transformCamera; // Reference to TransformCamera script

    public static GenderSelection instance { get; private set; }

    private Transform maleParts;   // Reference to male body parts
    private Transform femaleParts; // Reference to female body parts

    public bool isFemale = false;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        FindParts();

        genderBtn.onClick.AddListener(ChangeGender);

        UpdateGenderVisuals();
    }

    private void FindParts()
    {
        // Locate the "ModularCharacter" and find its child transforms
        Transform modularCharacter = player.transform.Find("Modular_Characters");
        if (modularCharacter != null)
        {
            maleParts = modularCharacter.Find("Male_Parts");
            femaleParts = modularCharacter.Find("Female_Parts");
        }
        else
        {
            Debug.LogError("ModularCharacter or its parts not found.");
        }
    }

    private void ChangeGender()
    {
        isFemale = !isFemale; // Toggle gender

        UpdateGenderVisuals();

        // Notify TransformCamera to update sprites
        if (transformCamera != null)
        {
            transformCamera.UpdateAllSprites();
            transformCamera.ZoomOut();
        }
        else
        {
            Debug.LogWarning("TransformCamera reference is missing. Please assign it in the Inspector.");
        }
    }

    private void UpdateGenderVisuals()
    {
        // Update the button image to reflect the selected gender
        genderImage.sprite = isFemale ? genders[0] : genders[1];

        // Enable/disable body parts based on the selected gender
        if (femaleParts != null) femaleParts.gameObject.SetActive(isFemale);
        if (maleParts != null) maleParts.gameObject.SetActive(!isFemale);
    }
}
