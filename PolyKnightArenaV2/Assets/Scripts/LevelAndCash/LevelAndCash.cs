using UnityEngine;
using ES3Internal;

public class LevelAndCash : MonoBehaviour
{
    public static LevelAndCash Instance { get; private set; }

    public int Level { get; private set; } = 1;
    public int Cash { get; private set; } = 0;
    public float LevelProgress { get; private set; } = 0f;
    public float MaxLevelProgress { get; private set; } = 1000f;

    private ES3Settings settings;

    private void Awake()
    {
        // Singleton-pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Ladda data vid start
        settings = new ES3Settings(); // Anpassa om du har specifik ES3-konfiguration
        LoadLevelAndCash(settings);
    }

    public void SaveLevel()
    {
        ES3.Save("Level", Level, "SaveFile.es3");
        ES3.Save("LevelProgress", LevelProgress, "SaveFile.es3");
        ES3.Save("MaxLevelProgress", MaxLevelProgress, "SaveFile.es3");
        Debug.Log("Level saved.");
    }

    public void SaveCash()
    {
        ES3.Save("Cash", Cash, "SaveFile.es3");
        Debug.Log("Cash saved: " + Cash);
    }

    public void LoadLevelAndCash(ES3Settings settings)
    {
        if (ES3.FileExists("SaveFile.es3"))
        {
            // Ladda sparad data
            Level = ES3.Load<int>("Level", "SaveFile.es3", 1); // Default: 1
            Cash = ES3.Load<int>("Cash", "SaveFile.es3", 0);   // Default: 0
            LevelProgress = ES3.Load<float>("LevelProgress", "SaveFile.es3", 0f);
            MaxLevelProgress = ES3.Load<float>("MaxLevelProgress", "SaveFile.es3", 1000f);

            Debug.Log($"Data loaded: Level={Level}, Cash={Cash}, LevelProgress={LevelProgress}, MaxLevelProgress={MaxLevelProgress}");
        }
        else
        {
            // Spara initiala värden om fil saknas
            SaveLevel();
            SaveCash();
        }
    }

    public void AddCash(int amount)
    {
        if (amount < 0) return; // Förhindra negativa värden
        Cash += amount;
        SaveCash();
    }

    public void RemoveCash(int amount)
    {
        if (amount < 0 || Cash - amount < 0) return; // Förhindra negativa resultat
        Cash -= amount;
        SaveCash();
    }

    public void AddLevelProgress(float amount)
    {
        if (amount < 0) return; // Förhindra negativa värden
        LevelProgress += amount;
        LevelSystem();
        SaveLevel();
    }

    private void LevelSystem()
    {
        float levelMultiplier = 1.2f;

        // Öka svårighetsgrad vid högre nivåer
        if (Level > 10) levelMultiplier = 1.25f;
        if (Level > 20) levelMultiplier = 1.3f;
        if (Level > 30) levelMultiplier = 1.35f;
        if (Level > 40) levelMultiplier = 1.4f;
        if (Level > 50) levelMultiplier = 1.45f;
        if (Level > 60) levelMultiplier = 1.5f;

        if (LevelProgress >= MaxLevelProgress)
        {
            Level++;
            MaxLevelProgress *= levelMultiplier;
            LevelProgress = 0;
            Debug.Log("Level Up! New Level: " + Level);
            SaveLevel();
        }
    }

    public void ResetData()
    {
        Level = 1;
        Cash = 0;
        LevelProgress = 0;
        MaxLevelProgress = 1000f;

        SaveLevel();
        SaveCash();
        Debug.Log("Data reset.");
    }
}
