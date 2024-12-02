using System.Collections.Generic;
using UnityEngine;

public class EquiptAttachments : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PartDictionaries partDictionaries;
    private ES3Settings settings;

    private int currentHeadAttachIndex = 0;
    private int currentMantleIndex = 0;
    private int currentRightShoulderIndex = 0;
    private int currentLeftShoulderIndex = 0;
    private int currentRightElbowIndex = 0;
    private int currentLeftElbowIndex = 0;
    private int currentHipAttachIndex = 0;
    private int currentRightKneeIndex = 0;
    private int currentLeftKneeIndex = 0;

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
        Debug.Log("Modular Character: " + modularCharacter);
        if (settings != null)
        {
            // Retrieve all keys
            var allKeys = ES3.GetKeys(settings);
            Debug.Log("All keys: " + allKeys.Length);

            // Loop through the keys and find matches
            foreach (var key in allKeys)
            {
                Debug.Log("Key: " + key);
                if (key.Contains(partialKey))
                {
                    string modelData = ES3.Load<string>(key, settings);

                    string[] parts = modelData.Split(' ');
                    Debug.Log("Parts owned: " + parts[0]);

                    if (parts[0].Contains("Helmet"))
                    {
                        if (partDictionaries.headAttachParts == null || partDictionaries.headAttachParts?.Count == 0)
                        {
                            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_02_Head_Attachment/Helmet/" + "Chr_HelmetAttachment_00", "HeadAttach", partDictionaries.headAttachParts);
                        }
                        Debug.Log(parts.Length);
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
                            Debug.Log("Initialized empty shoulder parts");
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
                            Debug.Log("Initialized empty hip parts");
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


                    Debug.Log("Shoulder data: " + partDictionaries.shoulderParts.Count);
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

            // Log initialized parts
            Debug.Log($"Initialized {newParts.Length} parts for {key} at {path}");
        }
        else
        {
            Debug.LogWarning($"Model path '{path}' not found for key '{key}'.");
        }
    }

    void ChangeModel(int value, string modelKey, Dictionary<string, GameObject[]> modelDict, ref int currentIndex)
    {
        if (!modelDict.ContainsKey(modelKey)) return;

        GameObject[] models = modelDict[modelKey];
        if (models == null || models.Length == 0) return;

        // Deactivate all models
        foreach (var model in models)
        {
            model.SetActive(false);
        }

        // Update index and wrap around
        currentIndex = (currentIndex + value + models.Length) % models.Length;

        // Activate the new model
        models[currentIndex].SetActive(true);
    }

    public void ChangeHeadAttach(int value)
    {
        Debug.Log("Changing to value" + value);
        ChangeModel(value, "HeadAttach", partDictionaries.headAttachParts, ref currentHeadAttachIndex);
        Debug.Log("Head Attach index: " + currentHeadAttachIndex);
    }

    public void ChangeShoulder(int value)
    {
        Debug.Log("Changing to value" + value);
        ChangeModel(value, "RightShoulderwear", partDictionaries.shoulderParts, ref currentRightShoulderIndex);
        ChangeModel(value, "LeftShoulderwear", partDictionaries.shoulderParts, ref currentLeftShoulderIndex);
    }
    public void ChangeElbow(int value)
    {
        Debug.Log("Changing to value" + value);
        ChangeModel(value, "RightElbowwear", partDictionaries.elbowParts, ref currentRightElbowIndex);
        ChangeModel(value, "LeftElbowwear", partDictionaries.elbowParts, ref currentLeftElbowIndex);
    }
    public void ChangeHipAttach(int value)
    {
        Debug.Log("Changing to value" + value);
        ChangeModel(value, "HipAttachment", partDictionaries.hipAttachParts, ref currentHipAttachIndex);
    }
    public void ChangeKnee(int value)
    {
        Debug.Log("Changing to value" + value);
        ChangeModel(value, "RightKneewear", partDictionaries.kneeParts, ref currentRightKneeIndex);
        ChangeModel(value, "LeftKneewear", partDictionaries.kneeParts, ref currentLeftKneeIndex);
    }
    public void ChangeMantle(int value)
    {
        Debug.Log("Changing to value" + value);
        ChangeModel(value, "BackAttachment", partDictionaries.mantleParts, ref currentMantleIndex);
    }
}
