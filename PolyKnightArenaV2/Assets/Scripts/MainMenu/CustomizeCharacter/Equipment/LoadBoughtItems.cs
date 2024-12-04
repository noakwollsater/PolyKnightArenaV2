using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class LoadBoughtItems : MonoBehaviour
{
    private ES3Settings settings;
    private ES3Settings equipSettings;
    public string[] boughtItems;

    [SerializeField] private ShopInventory shopInventory;
    [SerializeField] private Transform attachmentContainer;
    [SerializeField] private GameObject attachItem;

    [SerializeField] private EquiptAttachments equiptAttachments;

    [SerializeField] private GameObject ItemPanel;

    [SerializeField] Button headAttachmentSlot;
    [SerializeField] Button mantleAttachmentSlot;
    [SerializeField] Button shoulderAttachmentSlot;
    [SerializeField] Button eblowAttachmentSlot;
    [SerializeField] Button hipAttachmentSlot;
    [SerializeField] Button kneeAttachmentSlot;

    // Add these serialized fields to reference the button icons.
    [SerializeField] Image headAttachmentIcon;
    [SerializeField] Image mantleAttachmentIcon;
    [SerializeField] Image shoulderAttachmentIcon;
    [SerializeField] Image elbowAttachmentIcon;
    [SerializeField] Image hipAttachmentIcon;
    [SerializeField] Image kneeAttachmentIcon;
    [SerializeField] Sprite defaultAttachmentIcon;

    private List<Product> originalProducts;
    private List<Product> filteredProducts;

    void Start()
    {
        originalProducts = new List<Product>(shopInventory.products);
        filteredProducts = new List<Product>(shopInventory.products); // Initialize with all products.


        InitializeES3Settings();
        LoadEquippedAttachments();
        DisplayProducts();

        headAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Helmet", headAttachmentSlot));
        mantleAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Mantle", mantleAttachmentSlot));
        shoulderAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Shoulder", shoulderAttachmentSlot));
        eblowAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Elbow", eblowAttachmentSlot));
        hipAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Hip", hipAttachmentSlot));
        kneeAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Knee", kneeAttachmentSlot));
    }

    private void LoadEquippedAttachments()
    {
        string headAttachIconName = ES3.LoadString("HeadAttach_Icon", "", equipSettings);
        string headkey = "Head";
        if (!string.IsNullOrEmpty(headAttachIconName))
        {
            headAttachmentIcon.sprite = FindSpriteByName(headkey, headAttachIconName);
        }
        else
        {
            headAttachmentIcon.sprite = defaultAttachmentIcon;
        }

        string mantleAttachIconName = ES3.LoadString("MantleAttach_Icon", "", equipSettings);
        string mantlekey = "Mantle";
        if (!string.IsNullOrEmpty(mantleAttachIconName))
        {
            mantleAttachmentIcon.sprite = FindSpriteByName(mantlekey, mantleAttachIconName);
        }
        else
        {
            mantleAttachmentIcon.sprite = defaultAttachmentIcon;
        }

        string shoulderAttachIconName = ES3.LoadString("ShoulderAttach_Icon", "", equipSettings);
        string shoulderkey = "Shoulder";
        if (!string.IsNullOrEmpty(shoulderAttachIconName))
        {
            shoulderAttachmentIcon.sprite = FindSpriteByName(shoulderkey, shoulderAttachIconName);
        }
        else
        {
            shoulderAttachmentIcon.sprite = defaultAttachmentIcon;
        }

        string elbowAttachIconName = ES3.LoadString("ElbowAttach_Icon", "", equipSettings);
        string elbowkey = "Elbow";
        if (!string.IsNullOrEmpty(elbowAttachIconName))
        {
            elbowAttachmentIcon.sprite = FindSpriteByName(elbowkey, elbowAttachIconName);
        }
        else
        {
            elbowAttachmentIcon.sprite = defaultAttachmentIcon;
        }

        string hipAttachIconName = ES3.LoadString("HipAttach_Icon", "", equipSettings);
        string hipkey = "Hip";
        if (!string.IsNullOrEmpty(hipAttachIconName))
        {
            hipAttachmentIcon.sprite = FindSpriteByName(hipkey, hipAttachIconName);
        }
        else
        {
            hipAttachmentIcon.sprite = defaultAttachmentIcon;
        }

        string kneeAttachIconName = ES3.LoadString("KneeAttach_Icon", "", equipSettings);
        string kneekey = "Knee";
        if (!string.IsNullOrEmpty(kneeAttachIconName))
        {
            kneeAttachmentIcon.sprite = FindSpriteByName(kneekey, kneeAttachIconName);
        }
        else
        {
            kneeAttachmentIcon.sprite = defaultAttachmentIcon;
        }
    }

    private Sprite FindSpriteByName(string key,string spriteName)
    {
        return Resources.Load<Sprite>($"Images/ProductIcons/{key}/{spriteName}");
    }



    public void OnAttachButtonPressed(string category, Button filterButton)
    {
        if (ItemPanel.activeSelf)
        {
            // If the panel is already open, check if the category is the same
            // as the currently displayed one.
            if (filteredProducts.Any(product => product.productType.Equals(category, StringComparison.OrdinalIgnoreCase)))
            {
                // Close the panel if the same category is clicked.
                ClosePanel();
            }
            else
            {
                // Switch to the new category if a different button is clicked.
                AttachItems(category);
            }
        }
        else
        {
            // Open the panel and apply the filter.
            ItemPanel.SetActive(true);
            AttachItems(category);
        }
    }


    public void AttachItems(string category)
    {
        // Filter products based on the selected category.
        filteredProducts = originalProducts.Where(product => product.productType.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

        if (filteredProducts.Count == 0)
        {
            Debug.LogWarning($"No products found for category: {category}");
        }

        DisplayProducts(filteredProducts); // Display only the filtered products.
    }


    private void ClosePanel()
    {
        ItemPanel.SetActive(false);
        ShowAllItems(); // Reset the display to show all items after closing.
    }


    private void InitializeES3Settings()
    {
        if (settings == null)
        {
            settings = new ES3Settings(Application.persistentDataPath + "/BoughtData.es3");
            Debug.Log("ES3Settings initialized. Save path: " + Application.persistentDataPath + "/BoughtData.es3");
            ES3.Save("Bought Items", "Initialize", settings);
        }
        if (equipSettings == null)
        {
            equipSettings = new ES3Settings(Application.persistentDataPath + "/EquipedData.es3");
            Debug.Log("ES3Settings initialized. Save path: " + Application.persistentDataPath + "/EquipedData.es3");
            ES3.Save("Equiped Item", "Initialize", equipSettings);
        }
    }

    private void ShowAllItems()
    {
        filteredProducts = new List<Product>(originalProducts); // Reset the filtered list.
        DisplayProducts(filteredProducts);
    }

    void DisplayProducts(List<Product> productsToShow = null)
    {
        // Use default (filtered) products if no list is provided.
        var products = productsToShow ?? filteredProducts;

        // Clear the current items in the attachment container.
        foreach (Transform child in attachmentContainer)
        {
            Destroy(child.gameObject);
        }

        var allKeys = ES3.GetKeys(settings);
        int index = 1;

        foreach (var item in products)
        {
            bool itemFound = false;

            foreach (var key in allKeys)
            {
                int value = index;
                if (key.Contains("Own_" + item.productName))
                {
                    itemFound = true;

                    // Create a new button for the item.
                    GameObject ItemButtonUI = Instantiate(attachItem, attachmentContainer);
                    ItemButtonUI.transform.Find("ProductButton/ICON").GetComponent<Image>().sprite = item.productImage;
                    ItemButtonUI.transform.Find("ProductButton").GetComponent<Button>().onClick.AddListener(() => EquipSelectedItem(item.RightmodelName.Substring(item.RightmodelName.Length - 2), item.productType, item.productImage));
                }
            }
        }
    }

    private void SaveEquippedAttachment(string key, string modelName, string iconName)
    {
        ES3.Save(key + "_Icon", iconName, equipSettings); // Save the icon name
        Debug.Log($"Saved {key}: {modelName}, Icon: {iconName}");
    }


    private void EquipSelectedItem(string modelName, string category, Sprite productImage)
    {
        if (category == "Helmet")
        {
            if (headAttachmentIcon.sprite == null || headAttachmentIcon.sprite.name != productImage.name)
            {
                equiptAttachments.ChangeHeadAttach(modelName);
                headAttachmentIcon.sprite = productImage;

                // Save to persistent storage
                SaveEquippedAttachment("HeadAttach", modelName, productImage.name);
            }
            else
            {
                equiptAttachments.ChangeHeadAttach("00");
                headAttachmentIcon.sprite = defaultAttachmentIcon;

                // Clear saved data
                SaveEquippedAttachment("HeadAttach", "00", "");
            }
        }
        // Repeat for other categories
        else if (category == "Mantle")
        {
            if (mantleAttachmentIcon.sprite == null || mantleAttachmentIcon.sprite.name != productImage.name)
            {
                equiptAttachments.ChangeMantle(modelName);
                mantleAttachmentIcon.sprite = productImage;

                SaveEquippedAttachment("MantleAttach", modelName, productImage.name);
            }
            else
            {
                equiptAttachments.ChangeMantle("00");
                mantleAttachmentIcon.sprite = defaultAttachmentIcon;

                SaveEquippedAttachment("MantleAttach", "00", "");
            }
        }

        else if (category == "Shoulder")
        {
            if (shoulderAttachmentIcon.sprite == null || shoulderAttachmentIcon.sprite.name != productImage.name)
            {
                equiptAttachments.ChangeShoulder(modelName);
                shoulderAttachmentIcon.sprite = productImage;

                SaveEquippedAttachment("ShoulderAttach", modelName, productImage.name);
            }
            else
            {
                equiptAttachments.ChangeShoulder("00");
                shoulderAttachmentIcon.sprite = defaultAttachmentIcon;

                SaveEquippedAttachment("ShoulderAttach", "00", "");
            }
        }

        else if (category == "Elbow")
        {
            if (elbowAttachmentIcon.sprite == null || elbowAttachmentIcon.sprite.name != productImage.name)
            {
                equiptAttachments.ChangeElbow(modelName);
                elbowAttachmentIcon.sprite = productImage;

                SaveEquippedAttachment("ElbowAttach", modelName, productImage.name);
            }
            else
            {
                equiptAttachments.ChangeElbow("00");
                elbowAttachmentIcon.sprite = defaultAttachmentIcon;

                SaveEquippedAttachment("ElbowAttach", "00", "");
            }
        }

        else if (category == "Hip")
        {
            if (hipAttachmentIcon.sprite == null || hipAttachmentIcon.sprite.name != productImage.name)
            {
                equiptAttachments.ChangeHipAttach(modelName);
                hipAttachmentIcon.sprite = productImage;

                SaveEquippedAttachment("HipAttach", modelName, productImage.name);
            }
            else
            {
                equiptAttachments.ChangeHipAttach("00");
                hipAttachmentIcon.sprite = defaultAttachmentIcon;

                SaveEquippedAttachment("HipAttach", "00", "");
            }
        }

        else if (category == "Knee")
        {
            if (kneeAttachmentIcon.sprite == null || kneeAttachmentIcon.sprite.name != productImage.name)
            {
                equiptAttachments.ChangeKnee(modelName);
                kneeAttachmentIcon.sprite = productImage;

                SaveEquippedAttachment("KneeAttach", modelName, productImage.name);
            }
            else
            {
                equiptAttachments.ChangeKnee("00");
                kneeAttachmentIcon.sprite = defaultAttachmentIcon;

                SaveEquippedAttachment("KneeAttach", "00", "");
            }
        }
    }


}
