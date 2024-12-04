using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Buy : MonoBehaviour
{
    [SerializeField] private Button BuyBtn;

    private ES3Settings boughtSettings;
    private ES3Settings saveDataSettings;

    private void Start()
    {
        // Initiera ES3Settings
        InitializeES3Settings();
        InitializeES3SaveSettings();

        // Ladda LevelAndCash data vid start (redan hanterat av singleton)
        if (LevelAndCash.Instance == null)
        {
            Debug.LogError("LevelAndCash instance is not initialized!");
        }
        else
        {
            LevelAndCash.Instance.LoadLevelAndCash(saveDataSettings);
        }
    }

    // Köp produkt-metod
    public void BuyProduct(Product product)
    {
        // Kontrollera om produkten är null
        if (product == null)
        {
            Debug.LogError("Product is null!");
            return;
        }

        // Kontrollera att LevelAndCash-instansen är redo
        if (LevelAndCash.Instance == null)
        {
            Debug.LogError("LevelAndCash instance is not initialized!");
            return;
        }

        // Hämta spelarens nuvarande Level och Coins från singleton
        int playerLevel = LevelAndCash.Instance.Level;
        int playerCoins = LevelAndCash.Instance.Cash;

        // Logga spelarens status och produktinformation
        Debug.Log($"Level: {playerLevel} | Coins: {playerCoins}");
        Debug.Log($"Attempting to buy: {product.productName} | Price: {product.price} | Required Level: {product.requiredLevel}");

        // Utför köp om villkoren är uppfyllda
        if (playerCoins >= product.price && playerLevel >= product.requiredLevel)
        {
            // Ta bort pengar och spara uppdaterade värden
            LevelAndCash.Instance.RemoveCash(product.price);

            // Lägg till modellen och spara ägande
            AddModel(product);
            ES3.Save<bool>("Owned_" + product.productName, true, boughtSettings);

            Debug.Log($"Bought product: {product.productName}");

            // Uppdatera UI i ShopDisplay
            FindObjectOfType<ShopDisplay>().RefreshProductUI(product);
        }
        else
        {
            Debug.LogWarning("Not enough coins or insufficient level to buy this product.");
        }
    }

    // Lägg till modell och spara specifika modelldetaljer
    void AddModel(Product product)
    {
        // Säkerställ att ES3-inställningar är initierade
        if (boughtSettings == null)
        {
            InitializeES3Settings();
        }

        // Kontrollera att produktens modellnamn inte är tomma
        if (!string.IsNullOrEmpty(product.RightmodelName))
        {
            string modelData = $"{product.RightmodelName} {product.LeftmodelName}";
            ES3.Save<string>("Own_" + product.productName, modelData, boughtSettings);
            Debug.Log($"Model saved for product: {product.productName}");
        }
        else
        {
            Debug.LogWarning($"Model names are missing for product: {product.productName}");
        }
    }

    // Initiera ES3Settings för köpta produkter
    private void InitializeES3Settings()
    {
        if (boughtSettings == null)
        {
            boughtSettings = new ES3Settings(Application.persistentDataPath + "/BoughtData.es3");
            Debug.Log("ES3Settings initialized for BoughtData.");
        }
    }

    // Initiera ES3Settings för sparade data
    private void InitializeES3SaveSettings()
    {
        if (saveDataSettings == null)
        {
            saveDataSettings = new ES3Settings(Application.persistentDataPath + "/SaveData.es3", ES3.EncryptionType.AES, "K00a03j23s50a25");
            Debug.Log("ES3Settings initialized for SaveData.");
        }
    }
}
