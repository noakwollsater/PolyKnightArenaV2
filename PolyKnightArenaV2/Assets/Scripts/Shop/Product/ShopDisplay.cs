using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ShopDisplay : MonoBehaviour
{
    public ShopInventory shopInventory;
    public GameObject productUIPrefab;
    public GameObject FilterPanel;
    public Transform productContainer;

    public GameObject productInfoUIPrefab;
    public Transform InfoContainer;

    [SerializeField] private Button nextBtn;
    [SerializeField] private Button previousBtn;
    [SerializeField] private Button helmetFilter;
    [SerializeField] private Button ShoulderFilter;
    [SerializeField] private Button ElbowFilter;
    [SerializeField] private Button KneeFilter;
    [SerializeField] private Button HipFilter;
    [SerializeField] private Button MantleFilter;
    [SerializeField] private Button DisplayFilterBtn;
    [SerializeField] private Button HideProducts;
    [SerializeField] private Button ActiveHideProducts;

    public Buy buyComponent;

    private ES3Settings settings;

    private int currentPage = 0;  // To track the current page
    private int productsPerPage = 28;  // Number of products per page

    private List<Product> originalProducts;
    private GameObject activeAllDisplayButton = null;

    void Start()
    {
        originalProducts = new List<Product>(shopInventory.products);
        InitializeES3Settings();

        if (buyComponent == null)
        {
            Debug.LogError("Buy component is not found in the scene.");
            return;
        }

        DisplayFilterBtn.onClick.AddListener(() => DisplayFilterPanel());

        previousBtn.onClick.AddListener(PreviousPage);
        nextBtn.onClick.AddListener(NextPage);

        UpdatePaginationButtons();
        DisplayProducts();
    }

    private void Update()
    {
        if (LevelAndCash.Instance == null)
        {
            Debug.LogError("LevelAndCash instance is not initialized.");
            return;
        }

        // Dynamically update Level and Cash from the singleton
        UpdateBuyButtonInteractivity();
    }

    public void DisplayFilterPanel()
    {
        FilterPanel.SetActive(true);
        helmetFilter.onClick.AddListener(() => OnFilterButtonPressed("Helmet", helmetFilter));
        ShoulderFilter.onClick.AddListener(() => OnFilterButtonPressed("Shoulder", ShoulderFilter));
        ElbowFilter.onClick.AddListener(() => OnFilterButtonPressed("Elbow", ElbowFilter));
        KneeFilter.onClick.AddListener(() => OnFilterButtonPressed("Knee", KneeFilter));
        HipFilter.onClick.AddListener(() => OnFilterButtonPressed("Hip", HipFilter));
        MantleFilter.onClick.AddListener(() => OnFilterButtonPressed("Mantle", MantleFilter));

        HideProducts.onClick.AddListener(() => OnHideProducts());

        DisplayFilterBtn.onClick.RemoveAllListeners();
        DisplayFilterBtn.onClick.AddListener(() => HideFilterPanel());
    }

    public void HideFilterPanel()
    {
        FilterPanel.SetActive(false);
        DisplayFilterBtn.onClick.RemoveAllListeners();
        DisplayFilterBtn.onClick.AddListener(() => DisplayFilterPanel());
    }

    void DisplayProducts()
    {
        foreach (Transform child in productContainer)
        {
            Destroy(child.gameObject);
        }

        int startProductIndex = currentPage * productsPerPage;
        int endProductIndex = Mathf.Min(startProductIndex + productsPerPage, shopInventory.products.Count);

        for (int i = startProductIndex; i < endProductIndex; i++)
        {
            var product = shopInventory.products[i];
            GameObject productButtonUI = Instantiate(productUIPrefab, productContainer);
            GameObject productUI = Instantiate(productInfoUIPrefab, InfoContainer);
            ProductMono productMono = productButtonUI.GetComponent<ProductMono>();
            if (productMono != null)
            {
                productMono.infoPanel = productUI; // Assign the infoPanel of the instantiated prefab
            }
            else
            {
                Debug.LogError("ProductMono script is missing on the productUIPrefab.");
            }

            productUI.transform.Find("Content/Content/Item/Name/Label_ItemName").GetComponent<TMP_Text>().text = product.productName;
            productUI.transform.Find("Content/Content/Price/HUD_Stat_Value/Label_Stat_Text").GetComponent<TMP_Text>().text = product.price.ToString();
            productButtonUI.transform.Find("ProductButton/ICON").GetComponent<Image>().sprite = product.productImage;
            productUI.transform.Find("Content/Content/Item/Icon/ICON").GetComponent<Image>().sprite = product.productImage;
            productUI.transform.Find("Content/Content/Item/Name/Label_Description").GetComponent<TMP_Text>().text = product.description;

            Button buyButton = productUI.transform.Find("Content/BuyButton").GetComponent<Button>();

            if (IsProductOwned(product))
            {
                buyButton.interactable = false;
                buyButton.transform.Find("Content/BuyText").GetComponent<TMP_Text>().text = "Owned";
            }
            else
            {
                buyButton.onClick.AddListener(() => buyComponent.BuyProduct(product));
            }
        }
    }

    void UpdateBuyButtonInteractivity()
    {
        int playerLevel = LevelAndCash.Instance.Level;
        int playerCash = LevelAndCash.Instance.Cash;

        foreach (Transform productUI in InfoContainer)
        {
            Product product = originalProducts.FirstOrDefault(p => p.productName == productUI.transform.Find("Content/Content/Item/Name/Label_ItemName").GetComponent<TMP_Text>().text);

            if (product != null)
            {
                Button buyButton = productUI.transform.Find("Content/BuyButton").GetComponent<Button>();
                TMP_Text buyButtonText = buyButton.transform.Find("Content/BuyText").GetComponent<TMP_Text>();

                if (IsProductOwned(product))
                {
                    buyButton.interactable = false;
                    buyButtonText.text = "Owned";
                }
                else if (product.requiredLevel > playerLevel)
                {
                    buyButton.interactable = false;
                    buyButtonText.text = "Required Level: " + product.requiredLevel;
                }
                else if (product.price > playerCash)
                {
                    buyButton.interactable = false;
                    buyButtonText.text = "Not Enough Cash";
                }
                else
                {
                    buyButton.interactable = true;
                    buyButtonText.text = "Buy";
                }
            }
        }
    }

    public void OnHideProducts()
    {
        ActiveHideProducts.gameObject.SetActive(true);
        ActiveHideProducts.onClick.AddListener(() => ShowAllProducts());
        shopInventory.products = originalProducts.Where(product => product.requiredLevel <= LevelAndCash.Instance.Level).ToList();
        currentPage = 0;
        DisplayProducts();
        UpdatePaginationButtons();
    }

    bool IsProductOwned(Product product)
    {
        if (settings == null) InitializeES3Settings();
        return ES3.KeyExists("Owned_" + product.productName, settings);
    }

    private void InitializeES3Settings()
    {
        if (settings == null)
        {
            settings = new ES3Settings(Application.persistentDataPath + "/BoughtData.es3");
        }
    }

    void UpdatePaginationButtons()
    {
        previousBtn.interactable = currentPage > 0;
        nextBtn.interactable = (currentPage + 1) * productsPerPage < shopInventory.products.Count;
    }

    public void NextPage()
    {
        currentPage++;
        DisplayProducts();
        UpdatePaginationButtons();
    }

    public void PreviousPage()
    {
        currentPage--;
        DisplayProducts();
        UpdatePaginationButtons();
    }

    public void RefreshProductUI(Product product)
    {
        foreach (Transform productUI in productContainer)
        {
            TMP_Text productNameText = productUI.transform.Find("InfoPanel/Content/Item/Name/Label_ItemName").GetComponent<TMP_Text>();
            if (productNameText.text == product.productName)
            {
                Button buyButton = productUI.transform.Find("InfoPanel/BuyButton").GetComponent<Button>();
                if (IsProductOwned(product))
                {
                    buyButton.interactable = false;
                    buyButton.transform.Find("BuyText").GetComponent<TMP_Text>().text = "Owned";
                }
                break;
            }
        }
    }

    public void OnFilterButtonPressed(string category, Button filterButton)
    {
        FilterProducts(category);
        {
            Transform buttontext = filterButton.transform.Find("Content");
            buttontext.transform.Find("Label_Button").GetComponent<TMP_Text>().text = "Show All";
            filterButton.onClick.RemoveAllListeners();
            filterButton.onClick.AddListener(() => ShowProducts(filterButton));
        }
    }

    public void ShowProducts(Button filterbutton)
    {
        Transform buttontext = filterbutton.transform.Find("Content");
        buttontext.transform.Find("Label_Button").GetComponent<TMP_Text>().text = filterbutton.name;
        filterbutton.onClick.RemoveAllListeners();
        filterbutton.onClick.AddListener(() => OnFilterButtonPressed(filterbutton.name, filterbutton));
        ShowAllProducts();
    }

    public void FilterProducts(string category)
    {
        shopInventory.products = originalProducts.Where(product => product.productType == category).ToList();
        currentPage = 0;
        DisplayProducts();
        UpdatePaginationButtons();
    }

    public void ShowAllProducts()
    {
        shopInventory.products = new List<Product>(originalProducts);
        ActiveHideProducts.gameObject.SetActive(false);
        currentPage = 0;
        DisplayProducts();
        UpdatePaginationButtons();
    }
}
