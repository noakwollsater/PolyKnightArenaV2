using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private Button ChangeHairColorBtn;
    [SerializeField] private Button ChangeSkinColorBtn;
    [SerializeField] private Button ChangeEyeColorBtn;
    [SerializeField] private Button ChangeStubbleColorBtn;
    [SerializeField] private Button ChangeScarColorBtn;
    [SerializeField] private Button ChangeBodyArtColorBtn;
    [SerializeField] private Button ChangePrimaryColorBtn;
    [SerializeField] private Button ChangeSecondaryColorBtn;
    [SerializeField] private Button ChangeLeatherPrimaryColorBtn;
    [SerializeField] private Button ChangeLeatherSecondaryColorBtn;
    [SerializeField] private Button ChangeMetalPrimaryColorBtn;
    [SerializeField] private Button ChangeMetalSecondaryColorBtn;
    [SerializeField] private Button ChangeMetalDarkColorBtn;
    [SerializeField] private Button ResetColorButton;

    [SerializeField] private Transform ColorContainer;
    [SerializeField] private GameObject ColorPrefab;

    [SerializeField] private Material characterMaterial;

    private string currentSelectedPart = "_Color_Hair";
    private Button[] bodyPartButtons;

    private void Start()
    {
        // Store buttons for body parts
        bodyPartButtons = new Button[] {
            ChangePrimaryColorBtn, ChangeSecondaryColorBtn, ChangeLeatherPrimaryColorBtn, ChangeLeatherSecondaryColorBtn,
            ChangeMetalPrimaryColorBtn, ChangeMetalSecondaryColorBtn, ChangeMetalDarkColorBtn, ChangeHairColorBtn,
            ChangeSkinColorBtn, ChangeStubbleColorBtn, ChangeScarColorBtn, ChangeBodyArtColorBtn, ChangeEyeColorBtn
        };

        // Add listeners to category buttons
        ChangeHairColorBtn.onClick.AddListener(() => UpdateColorButtons(hairColors, "_Color_Hair"));
        ChangeSkinColorBtn.onClick.AddListener(() => UpdateColorButtons(skinColors, "_Color_Skin"));
        ChangeEyeColorBtn.onClick.AddListener(() => UpdateColorButtons(eyeColors, "_Color_Eyes"));
        ChangeStubbleColorBtn.onClick.AddListener(() => UpdateColorButtons(hairColors, "_Color_Stubble"));
        ChangeScarColorBtn.onClick.AddListener(() => UpdateColorButtons(scarColors, "_Color_Scar"));
        ChangeBodyArtColorBtn.onClick.AddListener(() => UpdateColorButtons(bodyArtColors, "_Color_BodyArt"));
        ChangePrimaryColorBtn.onClick.AddListener(() => UpdateColorButtons(primaryColors, "_Color_Primary"));
        ChangeSecondaryColorBtn.onClick.AddListener(() => UpdateColorButtons(primaryColors, "_Color_Secondary"));
        ChangeLeatherPrimaryColorBtn.onClick.AddListener(() => UpdateColorButtons(leatherColors, "_Color_Leather_Primary"));
        ChangeLeatherSecondaryColorBtn.onClick.AddListener(() => UpdateColorButtons(leatherColors, "_Color_Leather_Secondary"));
        ChangeMetalPrimaryColorBtn.onClick.AddListener(() => UpdateColorButtons(metalColors, "_Color_Metal_Primary"));
        ChangeMetalSecondaryColorBtn.onClick.AddListener(() => UpdateColorButtons(metalColors, "_Color_Metal_Secondary"));
        ChangeMetalDarkColorBtn.onClick.AddListener(() => UpdateColorButtons(metalColors, "_Color_Metal_Dark"));

        ResetColorButton.onClick.AddListener(ResetColorToDefault);

        // Initialize with default colors
        UpdateColorButtons(skinColors, "_Color_Skin");
    }

    private void UpdateColorButtons(Color[] colors, string part)
    {
        currentSelectedPart = part;

        // Clear the existing buttons in the container
        foreach (Transform child in ColorContainer)
        {
            Destroy(child.gameObject);
        }

        // Generate new buttons
        foreach (Color color in colors)
        {
            GameObject colorButton = Instantiate(ColorPrefab, ColorContainer);
            var buttonIcon = colorButton.transform.Find("ColorButton/ICON").GetComponent<Image>();
            buttonIcon.color = color;

            Button buttonComponent = colorButton.transform.Find("ColorButton").GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => OnColorSelected(color));
        }
    }

    private void OnColorSelected(Color color)
    {
        if (characterMaterial.HasProperty(currentSelectedPart))
        {
            characterMaterial.SetColor(currentSelectedPart, color);
            SaveColor(currentSelectedPart, color); // Save the selected color
            Debug.Log("Applied Color: " + color + " to " + currentSelectedPart);
        }
        else
        {
            Debug.LogWarning("Material does not have property: " + currentSelectedPart);
        }
    }


    public void ResetColorToDefault()
    {
        // Iterate over all body parts and reset their colors to the defaults
        string[] parts = {
        "_Color_Primary", "_Color_Secondary", "_Color_Leather_Primary", "_Color_Leather_Secondary",
        "_Color_Metal_Primary", "_Color_Metal_Secondary", "_Color_Metal_Dark",
        "_Color_Hair", "_Color_Skin", "_Color_Stubble", "_Color_Scar", "_Color_BodyArt", "_Color_Eyes"
        };

        for (int i = 0; i < parts.Length; i++)
        {
            if (characterMaterial.HasProperty(parts[i]))
            {
                characterMaterial.SetColor(parts[i], defaultColors[i]);
            }
        }

        Debug.Log("All colors reset to their default values.");
    }

    private void SaveColor(string part, Color color)
    {
        PlayerPrefs.SetFloat(part + "_R", color.r);
        PlayerPrefs.SetFloat(part + "_G", color.g);
        PlayerPrefs.SetFloat(part + "_B", color.b);
        PlayerPrefs.Save();
        Debug.Log("Saved Color: " + color + " for " + part);
    }

    private Color[] defaultColors = {
    new Color(0.5f, 0.5f, 0.5f), // Default color for Primary
    new Color(0.3f, 0.3f, 0.3f), // Default color for Secondary
    new Color(0.6f, 0.4f, 0.3f), // Leather Primary
    new Color(0.4f, 0.3f, 0.2f), // Leather Secondary
    new Color(0.8f, 0.8f, 0.8f), // Metal Primary
    new Color(0.6f, 0.6f, 0.6f), // Metal Secondary
    new Color(0.2f, 0.2f, 0.2f), // Metal Dark
    new Color(0.7f, 0.5f, 0.3f), // Hair
    new Color(0.96f, 0.76f, 0.65f), //Skin
    new Color(0.4f, 0.3f, 0.3f),  // Stubble
    new Color(0.6f, 0.3f, 0.3f),  // Scar
    new Color(0.7f, 0.4f, 0.2f),  // Body Art
    Color.black   // Eyes
};

    private Color[] hairColors = {
    new Color(0.1f, 0.1f, 0.1f), Color.black, Color.gray, Color.white,
    new Color(0.5f, 0.2f, 0.2f), Color.red, new Color(0.3f, 0.2f, 0.1f), Color.yellow,
    new Color(0.6f, 0.3f, 0.1f), new Color(0.4f, 0.2f, 0.05f), // Auburn
    new Color(0.2f, 0.1f, 0.1f), new Color(0.7f, 0.4f, 0.3f), // Dark browns
    new Color(0.8f, 0.6f, 0.4f), new Color(0.7f, 0.5f, 0.3f), // Light browns
    new Color(0.9f, 0.7f, 0.5f), new Color(0.8f, 0.8f, 0.8f), // Blonde
    new Color(0.3f, 0.4f, 0.5f), new Color(0.2f, 0.5f, 0.3f), // Teal, greenish
    new Color(0.5f, 0.2f, 0.4f), new Color(0.4f, 0.1f, 0.5f), // Purple
    new Color(0.1f, 0.3f, 0.5f), new Color(0.2f, 0.4f, 0.8f), // Dark blue
    new Color(0.6f, 0.2f, 0.7f), new Color(0.5f, 0.2f, 0.6f), // Lavender
    new Color(0.7f, 0.3f, 0.1f), new Color(0.8f, 0.4f, 0.2f), // Copper
    new Color(0.5f, 0.2f, 0.1f), new Color(0.4f, 0.1f, 0.05f), // Dark red
    new Color(0.2f, 0.5f, 0.2f), new Color(0.1f, 0.4f, 0.2f), // Emerald
    new Color(0.3f, 0.3f, 0.8f), new Color(0.2f, 0.2f, 0.6f), // Navy
    new Color(0.6f, 0.7f, 0.2f), new Color(0.4f, 0.6f, 0.1f), // Olive green
    new Color(0.9f, 0.9f, 0.6f), new Color(0.8f, 0.7f, 0.5f), // Platinum
    new Color(0.3f, 0.2f, 0.2f), new Color(0.5f, 0.4f, 0.4f), // Charcoal
    new Color(0.9f, 0.5f, 0.3f), new Color(0.7f, 0.2f, 0.2f)  // Bright red
};

    private Color[] skinColors = {
    // Natural Skin Tones
    new Color(1.0f, 0.8f, 0.6f), new Color(0.96f, 0.76f, 0.65f),
    new Color(0.87f, 0.58f, 0.44f), new Color(0.69f, 0.49f, 0.34f),
    new Color(0.85f, 0.7f, 0.55f), new Color(0.75f, 0.6f, 0.45f),
    new Color(0.7f, 0.5f, 0.4f), new Color(0.6f, 0.4f, 0.3f),
    new Color(0.53f, 0.33f, 0.24f), new Color(0.42f, 0.26f, 0.18f),
    new Color(0.35f, 0.24f, 0.17f), new Color(0.3f, 0.2f, 0.15f),
    new Color(0.9f, 0.7f, 0.6f), new Color(0.7f, 0.5f, 0.5f),
    new Color(0.5f, 0.4f, 0.3f), new Color(0.4f, 0.2f, 0.2f),
    new Color(0.2f, 0.15f, 0.1f), new Color(0.6f, 0.4f, 0.2f),
    new Color(0.5f, 0.3f, 0.1f), new Color(0.2f, 0.1f, 0.05f),
    new Color(0.8f, 0.7f, 0.6f), new Color(0.9f, 0.8f, 0.7f),

    // Warmer and Reddish Tones
    new Color(0.6f, 0.3f, 0.3f), new Color(0.7f, 0.2f, 0.2f),
    new Color(0.9f, 0.6f, 0.4f), new Color(0.8f, 0.4f, 0.3f),
    new Color(0.7f, 0.5f, 0.4f), new Color(0.5f, 0.3f, 0.2f),

    // Green Fantasy Tones
    new Color(0.3f, 0.6f, 0.3f), // Light Green
    new Color(0.2f, 0.5f, 0.2f), // Orc Green
    new Color(0.1f, 0.4f, 0.1f), // Deep Green
    new Color(0.5f, 0.7f, 0.4f), // Pale Greenish Skin
    new Color(0.4f, 0.6f, 0.4f), // Muted Green
    new Color(0.6f, 0.8f, 0.6f), // Pale Lime
    new Color(0.5f, 0.5f, 0.3f), // Olive Skin

    // Additional Fantasy Colors
    new Color(0.6f, 0.3f, 0.7f), // Lavender Skin
    new Color(0.8f, 0.6f, 0.9f), // Pale Purple
    new Color(0.2f, 0.7f, 0.5f), // Teal Green
    new Color(0.4f, 0.7f, 0.6f), // Seafoam Green

    // Final Additions for Diversity
    new Color(0.7f, 0.8f, 0.9f), // Pale Blueish
    new Color(0.9f, 0.9f, 0.7f), // Yellowish Pale
    new Color(0.3f, 0.2f, 0.3f)  // Muted Purple Brown
};

    private Color[] eyeColors = {
    Color.blue, Color.green, Color.cyan, Color.black, Color.gray,
    new Color(0.5f, 0.3f, 0.2f), new Color(0.7f, 0.4f, 0.3f),
    new Color(0.6f, 0.4f, 0.2f), Color.white, new Color(0.9f, 0.8f, 0.6f),
    new Color(0.2f, 0.6f, 0.7f), new Color(0.1f, 0.5f, 0.3f),
    new Color(0.0f, 0.7f, 0.2f), new Color(0.3f, 0.8f, 0.7f),
    new Color(0.1f, 0.4f, 0.9f), new Color(0.6f, 0.2f, 0.2f),
    new Color(0.8f, 0.5f, 0.5f), Color.magenta, Color.yellow,
    new Color(0.2f, 0.3f, 0.4f), new Color(0.4f, 0.6f, 0.8f),
    new Color(0.3f, 0.5f, 0.9f), new Color(0.7f, 0.8f, 0.5f),
    new Color(0.9f, 0.7f, 0.5f), new Color(0.8f, 0.5f, 0.4f),
    new Color(0.4f, 0.5f, 0.6f), new Color(0.6f, 0.4f, 0.3f),
    new Color(0.2f, 0.6f, 0.8f), new Color(0.3f, 0.7f, 0.2f),
    new Color(0.5f, 0.3f, 0.7f), new Color(0.7f, 0.2f, 0.5f),
    new Color(0.8f, 0.8f, 0.3f), new Color(0.7f, 0.4f, 0.6f),
    new Color(0.6f, 0.2f, 0.4f), new Color(0.8f, 0.6f, 0.7f),
    new Color(0.9f, 0.7f, 0.6f), new Color(0.3f, 0.8f, 0.6f),
    new Color(0.2f, 0.5f, 0.3f), new Color(0.1f, 0.7f, 0.2f)
};

    private Color[] scarColors = {
    new Color(0.8f, 0.4f, 0.4f), Color.red, new Color(0.7f, 0.3f, 0.3f),
    new Color(0.9f, 0.5f, 0.5f), new Color(0.6f, 0.2f, 0.2f),
    new Color(0.8f, 0.6f, 0.6f), new Color(0.5f, 0.2f, 0.2f),
    new Color(0.3f, 0.15f, 0.15f), new Color(0.4f, 0.2f, 0.2f),
    new Color(0.7f, 0.3f, 0.2f), new Color(0.8f, 0.5f, 0.4f),
    new Color(0.9f, 0.6f, 0.5f), new Color(0.6f, 0.3f, 0.3f),
    new Color(0.5f, 0.25f, 0.25f), new Color(0.7f, 0.4f, 0.3f),
    new Color(0.8f, 0.3f, 0.2f), new Color(0.6f, 0.4f, 0.4f),
    new Color(0.9f, 0.7f, 0.6f), new Color(0.7f, 0.5f, 0.5f),
    new Color(0.8f, 0.4f, 0.3f), new Color(0.5f, 0.3f, 0.2f),
    new Color(0.6f, 0.3f, 0.2f), new Color(0.3f, 0.2f, 0.2f),
    new Color(0.4f, 0.2f, 0.1f), new Color(0.7f, 0.4f, 0.2f),
    new Color(0.9f, 0.3f, 0.2f), new Color(0.6f, 0.2f, 0.1f),
    new Color(0.8f, 0.5f, 0.3f), new Color(0.7f, 0.2f, 0.2f),
    new Color(0.9f, 0.4f, 0.2f), new Color(0.5f, 0.3f, 0.3f),
    new Color(0.8f, 0.6f, 0.5f), new Color(0.6f, 0.4f, 0.3f),
    new Color(0.7f, 0.2f, 0.3f), new Color(0.4f, 0.3f, 0.3f),
    new Color(0.5f, 0.2f, 0.3f), new Color(0.9f, 0.2f, 0.1f),
    new Color(0.6f, 0.3f, 0.2f), new Color(0.7f, 0.4f, 0.2f),
    new Color(0.8f, 0.2f, 0.1f), new Color(0.6f, 0.1f, 0.1f)
};

    private Color[] bodyArtColors = {
    Color.black, Color.white, Color.red, Color.blue, Color.green, Color.yellow,
    Color.magenta, Color.cyan, Color.gray, new Color(0.7f, 0.2f, 0.4f),
    new Color(0.9f, 0.5f, 0.3f), new Color(0.2f, 0.7f, 0.2f),
    new Color(0.4f, 0.3f, 0.6f), new Color(0.1f, 0.8f, 0.7f),
    new Color(0.8f, 0.4f, 0.2f), new Color(0.6f, 0.3f, 0.1f),
    new Color(0.7f, 0.5f, 0.4f), new Color(0.3f, 0.3f, 0.3f),
    new Color(0.6f, 0.7f, 0.2f), new Color(0.5f, 0.3f, 0.7f),
    new Color(0.8f, 0.2f, 0.5f), new Color(0.3f, 0.7f, 0.4f),
    new Color(0.7f, 0.3f, 0.2f), new Color(0.9f, 0.2f, 0.1f),
    new Color(0.6f, 0.4f, 0.3f), new Color(0.5f, 0.2f, 0.2f),
    new Color(0.8f, 0.6f, 0.5f), new Color(0.4f, 0.5f, 0.3f),
    new Color(0.6f, 0.7f, 0.4f), new Color(0.7f, 0.8f, 0.2f),
    new Color(0.3f, 0.2f, 0.4f), new Color(0.4f, 0.5f, 0.6f),
    new Color(0.8f, 0.5f, 0.6f), new Color(0.7f, 0.2f, 0.5f),
    new Color(0.5f, 0.8f, 0.6f), new Color(0.3f, 0.9f, 0.8f),
    new Color(0.9f, 0.3f, 0.2f), new Color(0.8f, 0.4f, 0.1f),
    new Color(0.6f, 0.2f, 0.5f), new Color(0.2f, 0.6f, 0.9f),
    new Color(0.3f, 0.8f, 0.3f), new Color(0.5f, 0.5f, 0.5f)
};

    private Color[] leatherColors = {
    new Color(0.6f, 0.4f, 0.2f), new Color(0.5f, 0.3f, 0.2f),
    new Color(0.7f, 0.5f, 0.4f), new Color(0.2f, 0.1f, 0.05f),
    new Color(0.8f, 0.7f, 0.5f), new Color(0.3f, 0.2f, 0.1f),
    new Color(0.5f, 0.4f, 0.3f), new Color(0.4f, 0.2f, 0.2f),
    new Color(0.7f, 0.6f, 0.5f), new Color(0.9f, 0.8f, 0.7f),
    new Color(0.5f, 0.25f, 0.1f), new Color(0.2f, 0.15f, 0.1f),
    new Color(0.6f, 0.3f, 0.2f), new Color(0.4f, 0.2f, 0.1f),
    new Color(0.8f, 0.6f, 0.3f), new Color(0.7f, 0.5f, 0.4f),
    new Color(0.3f, 0.3f, 0.3f), new Color(0.4f, 0.2f, 0.05f),
    new Color(0.7f, 0.4f, 0.3f), new Color(0.8f, 0.7f, 0.4f),
    new Color(0.9f, 0.6f, 0.4f), new Color(0.5f, 0.3f, 0.1f),
    new Color(0.6f, 0.5f, 0.4f), new Color(0.8f, 0.2f, 0.1f),
    new Color(0.4f, 0.4f, 0.3f), new Color(0.2f, 0.1f, 0.05f)
};

    private Color[] metalColors = {
    // Classic Metal Colors
    new Color(0.8f, 0.8f, 0.8f), // Silver
    new Color(0.9f, 0.7f, 0.3f), // Gold
    new Color(0.7f, 0.5f, 0.3f), // Bronze
    new Color(0.6f, 0.6f, 0.6f), // Steel
    new Color(0.5f, 0.3f, 0.2f), // Copper
    new Color(0.3f, 0.3f, 0.3f), // Iron
    new Color(0.9f, 0.8f, 0.6f), // Light Gold
    new Color(0.8f, 0.6f, 0.4f), // Aged Bronze
    new Color(0.7f, 0.6f, 0.5f), // Pewter
    new Color(0.5f, 0.5f, 0.5f), // Gunmetal Gray

    // Dark Metals
    new Color(0.2f, 0.2f, 0.2f), // Charcoal Iron
    new Color(0.1f, 0.1f, 0.1f), // Blackened Steel
    new Color(0.4f, 0.3f, 0.2f), // Dark Copper
    new Color(0.3f, 0.25f, 0.15f), // Dark Bronze
    new Color(0.2f, 0.15f, 0.1f), // Blackened Bronze
    new Color(0.1f, 0.05f, 0.05f), // Deep Iron
    new Color(0.6f, 0.5f, 0.4f), // Weathered Metal
    new Color(0.4f, 0.4f, 0.4f), // Matte Steel
    new Color(0.3f, 0.3f, 0.2f), // Rusted Iron
    new Color(0.2f, 0.1f, 0.1f), // Blackened Copper

    // Shiny and Fantasy Metals
    new Color(1.0f, 0.9f, 0.6f), // Polished Gold
    new Color(0.8f, 0.8f, 0.9f), // Reflective Silver
    new Color(0.7f, 0.8f, 0.9f), // Chrome
    new Color(0.9f, 0.6f, 0.2f), // Molten Gold
    new Color(0.5f, 0.6f, 0.8f), // Sapphire Steel
    new Color(0.6f, 0.5f, 0.7f), // Amethyst Iron
    new Color(0.9f, 0.2f, 0.2f), // Crimson Metal
    new Color(0.3f, 0.5f, 0.8f), // Azure Steel
    new Color(0.8f, 0.5f, 0.6f), // Rose Gold
    new Color(0.6f, 0.8f, 0.6f), // Jade Steel

    // Fantasy Colors
    new Color(0.7f, 0.7f, 0.1f), // Tarnished Gold
    new Color(0.3f, 0.7f, 0.3f), // Emerald Metal
    new Color(0.2f, 0.6f, 0.8f), // Aquamarine Steel
    new Color(0.5f, 0.2f, 0.7f), // Violet Metal
    new Color(0.9f, 0.3f, 0.7f), // Mystic Pink
    new Color(0.8f, 0.1f, 0.1f), // Blood Iron
    new Color(0.5f, 0.5f, 0.1f), // Olive Bronze
    new Color(0.4f, 0.6f, 0.2f), // Verdant Metal
    new Color(0.3f, 0.3f, 0.6f), // Midnight Steel
    new Color(0.6f, 0.1f, 0.2f), // Infernal Red
    new Color(0.7f, 0.7f, 0.9f), // Moonlit Silver
    new Color(0.8f, 0.9f, 0.2f)  // Radiant Gold
};

    private Color[] primaryColors = {
    // Red Tones
    new Color(1.0f, 0.0f, 0.0f), // Bright Red
    new Color(0.8f, 0.1f, 0.1f), // Crimson
    new Color(0.6f, 0.0f, 0.0f), // Dark Red
    new Color(1.0f, 0.3f, 0.3f), // Soft Red
    new Color(0.9f, 0.2f, 0.2f), // Scarlet

    // Orange Tones
    new Color(1.0f, 0.5f, 0.0f), // Bright Orange
    new Color(0.8f, 0.4f, 0.1f), // Burnt Orange
    new Color(1.0f, 0.7f, 0.4f), // Soft Orange
    new Color(0.9f, 0.6f, 0.2f), // Golden Orange
    new Color(0.8f, 0.5f, 0.2f), // Dark Orange

    // Yellow Tones
    new Color(1.0f, 1.0f, 0.0f), // Bright Yellow
    new Color(1.0f, 0.9f, 0.4f), // Soft Yellow
    new Color(0.8f, 0.8f, 0.0f), // Mustard
    new Color(1.0f, 0.8f, 0.2f), // Golden Yellow
    new Color(0.9f, 0.7f, 0.1f), // Amber

    // Green Tones
    new Color(0.0f, 1.0f, 0.0f), // Bright Green
    new Color(0.2f, 0.8f, 0.2f), // Lime Green
    new Color(0.0f, 0.6f, 0.0f), // Dark Green
    new Color(0.5f, 1.0f, 0.5f), // Mint Green
    new Color(0.4f, 0.7f, 0.3f), // Moss Green
    new Color(0.0f, 0.5f, 0.2f), // Forest Green

    // Blue Tones
    new Color(0.0f, 0.0f, 1.0f), // Bright Blue
    new Color(0.2f, 0.2f, 0.8f), // Deep Blue
    new Color(0.5f, 0.5f, 1.0f), // Soft Blue
    new Color(0.0f, 0.5f, 1.0f), // Sky Blue
    new Color(0.1f, 0.3f, 0.6f), // Navy Blue
    new Color(0.3f, 0.6f, 0.9f), // Azure

    // Purple Tones
    new Color(0.5f, 0.0f, 0.5f), // Violet
    new Color(0.7f, 0.3f, 0.7f), // Lavender
    new Color(0.8f, 0.6f, 0.8f), // Soft Purple
    new Color(0.3f, 0.0f, 0.5f), // Dark Purple
    new Color(0.6f, 0.2f, 0.6f), // Orchid

    // Pink Tones
    new Color(1.0f, 0.0f, 0.5f), // Hot Pink
    new Color(1.0f, 0.5f, 0.8f), // Soft Pink
    new Color(0.9f, 0.4f, 0.7f), // Light Rose
    new Color(0.7f, 0.2f, 0.4f), // Magenta
    new Color(1.0f, 0.8f, 0.9f), // Blush Pink

    // Neutral and Unique Tones
    new Color(0.8f, 0.8f, 0.8f), // Light Gray
    new Color(0.5f, 0.5f, 0.5f), // Medium Gray
    new Color(0.2f, 0.2f, 0.2f), // Dark Gray
    new Color(1.0f, 1.0f, 1.0f), // White
    new Color(0.0f, 0.0f, 0.0f), // Black
};

}
