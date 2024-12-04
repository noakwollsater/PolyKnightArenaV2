using System.Collections.Generic;
using UnityEngine;

public class EquiptAttachments : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PartDictionaries partDictionaries;
    private ES3Settings settings;

    void Start()
    {
        InitializeES3Settings();
        GetPurchasedItems();
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

    void GetPurchasedItems()
    {
        FindKeysContaining("Own_");
    }

    // Dynamically finds and assigns the models (e.g., hair, torso, etc.)
    void FindKeysContaining(string partialKey)
    {
        Transform modularCharacter = player.transform.Find("Modular_Characters");
        if (settings != null)
        {
            // Retrieve all keys
            var allKeys = ES3.GetKeys(settings);

            // Loop through the keys and find matches
            foreach (var key in allKeys)
            {
                if (key.Contains(partialKey))
                {
                    string modelData = ES3.Load<string>(key, settings);

                    string[] parts = modelData.Split(' ');

                    if (parts[0].Contains("Helmet"))
                    {
                        if (partDictionaries.headAttachParts == null || partDictionaries.headAttachParts?.Count == 0)
                        {
                            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_02_Head_Attachment/Helmet/" + "Chr_HelmetAttachment_00", "HeadAttach", partDictionaries.headAttachParts);
                        }
                        InitializeModelParts(modularCharacter, "All_Gender_Parts/All_02_Head_Attachment/Helmet/" + parts[0], "HeadAttach", partDictionaries.headAttachParts);
                    }
                    if (parts[0].Contains("Back"))
                    {
                        if (partDictionaries.mantleParts == null || partDictionaries.mantleParts?.Count == 0)
                        {
                            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_04_Back_Attachment/" + "Chr_BackAttachment_00", "BackAttachment", partDictionaries.mantleParts);
                        }
                        InitializeModelParts(modularCharacter, "All_Gender_Parts/All_04_Back_Attachment/" + parts[0], "BackAttachment", partDictionaries.mantleParts);
                    }
                    if (parts[0].Contains("Shoulder"))
                    {
                        if (partDictionaries.shoulderParts == null || partDictionaries.shoulderParts?.Count == 0)
                        {
                            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_05_Shoulder_Attachment_Right/" + "Chr_ShoulderAttachRight_00", "RightShoulderwear", partDictionaries.shoulderParts);
                            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_06_Shoulder_Attachment_Left/" + "Chr_ShoulderAttachLeft_00", "LeftShoulderwear", partDictionaries.shoulderParts);
                        }
                        InitializeModelParts(modularCharacter, "All_Gender_Parts/All_05_Shoulder_Attachment_Right/" + parts[0], "RightShoulderwear", partDictionaries.shoulderParts);
                        InitializeModelParts(modularCharacter, "All_Gender_Parts/All_06_Shoulder_Attachment_Left/" + parts[1], "LeftShoulderwear", partDictionaries.shoulderParts);
                    }
                    if (parts[0].Contains("Elbow"))
                    {
                        if (partDictionaries.elbowParts == null || partDictionaries.elbowParts?.Count == 0)
                        {
                            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_07_Elbow_Attachment_Right/" + "Chr_ElbowAttachRight_00", "RightElbowwear", partDictionaries.elbowParts);
                            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_08_Elbow_Attachment_Left/" + "Chr_ElbowAttachLeft_00", "LeftElbowwear", partDictionaries.elbowParts);
                        }
                        InitializeModelParts(modularCharacter, "All_Gender_Parts/All_07_Elbow_Attachment_Right/" + parts[0], "RightElbowwear", partDictionaries.elbowParts);
                        InitializeModelParts(modularCharacter, "All_Gender_Parts/All_08_Elbow_Attachment_Left/" + parts[1], "LeftElbowwear", partDictionaries.elbowParts);
                    }
                    if (parts[0].Contains("Hip"))
                    {
                        if (partDictionaries.hipAttachParts != null || partDictionaries.hipAttachParts?.Count == 0)
                        {
                            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_09_Hips_Attachment/" + "Chr_HipAttachment_00", "HipAttachment", partDictionaries.hipAttachParts);
                        }
                        InitializeModelParts(modularCharacter, "All_Gender_Parts/All_09_Hips_Attachment/" + parts[0], "HipAttachment", partDictionaries.hipAttachParts);
                    }
                    if (parts[0].Contains("Knee"))
                    {
                        if (partDictionaries.kneeParts != null || partDictionaries.kneeParts?.Count == 0)
                        {
                            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_10_Knee_Attachment_Right/" + "Chr_KneeAttachRight_00", "RightKneewear", partDictionaries.kneeParts);
                            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_11_Knee_Attachment_Left/" + "Chr_KneeAttachLeft_00", "LeftKneewear", partDictionaries.kneeParts);
                        }
                        InitializeModelParts(modularCharacter, "All_Gender_Parts/All_10_Knee_Attachment_Right/" + parts[0], "RightKneewear", partDictionaries.kneeParts);
                        InitializeModelParts(modularCharacter, "All_Gender_Parts/All_11_Knee_Attachment_Left/" + parts[1], "LeftKneewear", partDictionaries.kneeParts);
                    }
                }
            }
        }
    }

    void InitializeModelParts(Transform parentTransform, string path, string key, Dictionary<string, GameObject[]> modelDict)
    {
        Transform modelTransform = parentTransform.Find(path);

        if (modelTransform != null)
        {
            GameObject[] newParts;

            // Check if this is a single model (no children)
            if (modelTransform.childCount == 0)
            {
                newParts = new GameObject[1];  // Initialize with a size of 1
                newParts[0] = modelTransform.gameObject;
            }
            else
            {
                newParts = new GameObject[modelTransform.childCount];
                for (int i = 0; i < modelTransform.childCount; i++)
                {
                    newParts[i] = modelTransform.GetChild(i).gameObject;
                }
            }

            // Add or combine parts in the dictionary
            if (modelDict.ContainsKey(key))
            {
                GameObject[] existingParts = modelDict[key];
                GameObject[] combinedParts = new GameObject[existingParts.Length + newParts.Length];
                existingParts.CopyTo(combinedParts, 0);
                newParts.CopyTo(combinedParts, existingParts.Length);
                modelDict[key] = combinedParts;
            }
            else
            {
                modelDict[key] = newParts;
            }
        }
        else
        {
            Debug.LogWarning($"Model path '{path}' not found for key '{key}'.");
        }
    }

    void ChangeModel(string modelName, string modelKey, Dictionary<string, GameObject[]> modelDict)
    {
        if (!modelDict.ContainsKey(modelKey))
        {
            Debug.LogWarning($"Model key '{modelKey}' not found in the dictionary.");
            return;
        }

        GameObject[] models = modelDict[modelKey];
        if (models == null || models.Length == 0)
        {
            Debug.LogWarning($"No models found for key '{modelKey}'.");
            return;
        }

        // Deactivate all models
        foreach (var model in models)
        {
            model.SetActive(false);
        }

        // Find and activate the model by name
        GameObject targetModel = System.Array.Find(models, model => model.name.Contains(modelName, System.StringComparison.OrdinalIgnoreCase));
        if (targetModel != null)
        {
            targetModel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Model '{modelName}' not found in the models for key '{modelKey}'.");
        }
    }


    public void ChangeHeadAttach(string modelName)
    {
        PlayerPrefs.SetString("HeadAttach", modelName);
        PlayerPrefs.Save();

        ChangeModel(modelName, "HeadAttach", partDictionaries.headAttachParts);
    }

    public void ChangeShoulder(string modelName)
    {
        PlayerPrefs.SetString("RightShoulderwear", modelName);
        PlayerPrefs.SetString("LeftShoulderwear", modelName);
        PlayerPrefs.Save();

        ChangeModel(modelName, "RightShoulderwear", partDictionaries.shoulderParts);
        ChangeModel(modelName, "LeftShoulderwear", partDictionaries.shoulderParts);
    }

    public void ChangeElbow(string modelName)
    {
        PlayerPrefs.SetString("RightElbowwear", modelName);
        PlayerPrefs.SetString("LeftElbowwear", modelName);
        PlayerPrefs.Save();

        ChangeModel(modelName, "RightElbowwear", partDictionaries.elbowParts);
        ChangeModel(modelName, "LeftElbowwear", partDictionaries.elbowParts);
    }

    public void ChangeHipAttach(string modelName)
    {
        PlayerPrefs.SetString("HipAttachment", modelName);
        PlayerPrefs.Save();

        ChangeModel(modelName, "HipAttachment", partDictionaries.hipAttachParts);
    }

    public void ChangeKnee(string modelName)
    {
        PlayerPrefs.SetString("RightKneewear", modelName);
        PlayerPrefs.SetString("LeftKneewear", modelName);
        PlayerPrefs.Save();

        ChangeModel(modelName, "RightKneewear", partDictionaries.kneeParts);
        ChangeModel(modelName, "LeftKneewear", partDictionaries.kneeParts);
    }

    public void ChangeMantle(string modelName)
    {
        PlayerPrefs.SetString("BackAttachment", modelName);
        PlayerPrefs.Save();

        ChangeModel(modelName, "BackAttachment", partDictionaries.mantleParts);
    }
}
