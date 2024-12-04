using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadBoughtItems : MonoBehaviour
{
    private ES3Settings settings;
    public string[] boughtItems;

    [SerializeField] private ShopInventory shopInventory;
    [SerializeField] private Transform attachmentContainer;
    [SerializeField] private GameObject attachItem;

    [SerializeField] private EquiptAttachments equiptAttachments;

    [SerializeField] private GameObject ItemPanel;

    [SerializeField] Button headAttachmentSlot;
    [SerializeField] Button mantleAttachmentSlot;
    [SerializeField] Button torsoAttachmentSlot;
    [SerializeField] Button shoulderAttachmentSlot;
    [SerializeField] Button eblowAttachmentSlot;
    [SerializeField] Button hipAttachmentSlot;
    [SerializeField] Button kneeAttachmentSlot;

    private List<Product> originalProducts;

    private List<Product> filteredProducts;

    void Start()
    {
        originalProducts = new List<Product>(shopInventory.products);
        filteredProducts = new List<Product>(shopInventory.products); // Initialize with all products.
        InitializeES3Settings();
        DisplayProducts();

        headAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Head", headAttachmentSlot));
        mantleAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Mantle", mantleAttachmentSlot));
        torsoAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Torso", torsoAttachmentSlot));
        shoulderAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Shoulder", shoulderAttachmentSlot));
        eblowAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Elbow", eblowAttachmentSlot));
        hipAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Hip", hipAttachmentSlot));
        kneeAttachmentSlot.onClick.AddListener(() => OnAttachButtonPressed("Knee", kneeAttachmentSlot));
    }

    public void OnAttachButtonPressed(string category, Button filterButton)
    {
        if (ItemPanel.activeSelf)
        {
            // Close the panel if it's already open.
            ClosePanel();
        }
        else
        {
            // Open the panel and apply the filter.
            ItemPanel.SetActive(true);
            AttachItems(category);

            // Remove listeners to avoid duplication and set up for reopening.
            filterButton.onClick.RemoveAllListeners();
            filterButton.onClick.AddListener(() => OnAttachButtonPressed(category, filterButton));
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
                    ItemButtonUI.transform.Find("ProductButton").GetComponent<Button>().onClick.AddListener(() => EquipSelectedItem(value, item.productType));
                }
            }

            if (!itemFound)
            {
                Debug.Log($"No saved data found for product: {item.productName}");
            }

            index++;
        }
    }


    private void EquipSelectedItem(int value, string category)
    {
        if (category == "Helmet")
        {
            equiptAttachments.ChangeHeadAttach(value);
        }
        else if (category == "Mantle")
        {
            equiptAttachments.ChangeMantle(value);
        }
        else if (category == "Shoulder")
        {
            equiptAttachments.ChangeShoulder(value);
        }
        else if (category == "Elbow")
        {
            Debug.Log("Sending request to ChangeElbow");
            equiptAttachments.ChangeElbow(value);
        }
        else if (category == "Hip")
        {
            equiptAttachments.ChangeHipAttach(value);
        }
        else if (category == "Knee")
        {
            equiptAttachments.ChangeKnee(value);
        }
    }

}
