using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PartDictionaries partDictionaries;

    private int currentHeadwearIndex = 0;
    private int currentHatsIndex = 0;
    private int currentmaskIndex = 0;
    private int currentHairIndex = 0;
    private int currentFaceIndex = 0;
    private int currentEyebrowIndex = 0;
    private int currentFacialHairIndex = 0;

    private int currentRightUpperArmIndex = 0;
    private int currentLeftUpperArmIndex = 0;
    private int currentRightLowerArmIndex = 0;
    private int currentLeftLowerArmIndex = 0;
    private int currentRightHandIndex = 0;
    private int currentLeftHandIndex = 0;
    private int currentTorsoIndex = 0;

    private int currentHipIndex = 0;
    private int currentLeftLegIndex = 0;
    private int currentRightLegIndex = 0;

    private string headAttachKey = "00";
    private string mantleKey = "00";
    private string shoulderKey = "00";
    private string elbowKey = "00";
    private string hipAttachKey = "00";
    private string kneeKey = "00";

    private bool isHair;
    private bool isHelmet;
    private bool isHat;
    private bool isFacialHair;
    private bool isMask;

    private string gender;

    private ES3Settings settings;

    void Start()
    {
        if (settings == null)
        {
            settings = new ES3Settings(Application.persistentDataPath + "/BoughtData.es3");
            Debug.Log("ES3Settings initialized. Save path: " + Application.persistentDataPath + "/BoughtData.es3");
            ES3.Save("Bought Items", "Initialize", settings);
        }
        selectMale();
        FindModels();
        GetPurchasedItems();
        LoadPlayerPrefs();
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

    void selectMale()
    {
        gender = "Male";
        SetGenderPartsActive(partDictionaries.femaleParts, false);
        SetGenderPartsActive(partDictionaries.maleParts, true);
    }

    // Select female parts
    void selectFemale()
    {
        gender = "Female";
        SetGenderPartsActive(partDictionaries.maleParts, false);
        SetGenderPartsActive(partDictionaries.femaleParts, true);
    }

    // Helper function to activate/deactivate gender-specific parts
    void SetGenderPartsActive(GameObject[] parts, bool isActive)
    {
        if (parts != null)
        {
            foreach (var part in parts)
            {
                if (part != null)
                {
                    part.SetActive(isActive);
                }
            }
        }
    }

    void FindModels()
    {
        Transform modularCharacter = player.transform.Find("Modular_Characters");

        if (modularCharacter != null)
        {
            partDictionaries.maleParts = FindGenderParts(modularCharacter, "Male_Parts");
            partDictionaries.femaleParts = FindGenderParts(modularCharacter, "Female_Parts");

            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_01_Hair", "Hair", partDictionaries.hairParts);
            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_00_HeadCoverings/HeadCoverings_Base_Hair", "Headwear", partDictionaries.hatParts); //No hair
            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_00_HeadCoverings/HeadCoverings_No_Hair", "Headwear", partDictionaries.hatParts); //No hair
            InitializeModelParts(modularCharacter, "All_Gender_Parts/All_00_HeadCoverings/HeadCoverings_No_FacialHair", "Facewear", partDictionaries.maskParts); //No facial hair
            InitializeModelParts(modularCharacter, "Female_Parts/Female_00_Head/Female_Head_No_Elements", "HumanFemale", partDictionaries.headwearParts);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_00_Head/Female_Head_All_Elements", "HumanFemale", partDictionaries.faceModels);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_01_Eyebrows", "HumanFemale", partDictionaries.eyeBrowsModels);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_02_FacialHair", "HumanFemale", partDictionaries.facialHairModels);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_03_Torso", "HumanFemale", partDictionaries.torsoParts);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_04_Arm_Upper_Right", "HumanFemale_RightUpperArm", partDictionaries.upperArmParts);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_05_Arm_Upper_Left", "HumanFemale_LeftUpperArm", partDictionaries.upperArmParts);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_06_Arm_Lower_Right", "HumanFemale_RightLowerArm", partDictionaries.lowerArmParts);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_07_Arm_Lower_Left", "HumanFemale_LeftLowerArm", partDictionaries.lowerArmParts);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_08_Hand_Right", "HumanFemale_RightHand", partDictionaries.handParts);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_09_Hand_Left", "HumanFemale_LeftHand", partDictionaries.handParts);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_10_Hips", "HumanFemale", partDictionaries.hipParts);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_11_Leg_Right", "HumanFemale_RightLeg", partDictionaries.legParts);
            InitializeModelParts(modularCharacter, "Female_Parts/Female_12_Leg_Left", "HumanFemale_LeftLeg", partDictionaries.legParts);


            InitializeModelParts(modularCharacter, "Male_Parts/Male_00_Head/Male_Head_No_Elements", "HumanMale", partDictionaries.headwearParts);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_00_Head/Male_Head_All_Elements", "HumanMale", partDictionaries.faceModels);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_01_Eyebrows", "HumanMale", partDictionaries.eyeBrowsModels);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_02_FacialHair", "HumanMale", partDictionaries.facialHairModels);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_03_Torso", "HumanMale", partDictionaries.torsoParts);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_04_Arm_Upper_Right", "HumanMale_RightUpperArm", partDictionaries.upperArmParts);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_05_Arm_Upper_Left", "HumanMale_LeftUpperArm", partDictionaries.upperArmParts);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_06_Arm_Lower_Right", "HumanMale_RightLowerArm", partDictionaries.lowerArmParts);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_07_Arm_Lower_Left", "HumanMale_LeftLowerArm", partDictionaries.lowerArmParts);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_08_Hand_Right", "HumanMale_RightHand", partDictionaries.handParts);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_09_Hand_Left", "HumanMale_LeftHand", partDictionaries.handParts);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_10_Hips", "HumanMale", partDictionaries.hipParts);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_11_Leg_Right", "HumanMale_RightLeg", partDictionaries.legParts);
            InitializeModelParts(modularCharacter, "Male_Parts/Male_12_Leg_Left", "HumanMale_LeftLeg", partDictionaries.legParts);



        }
        else
        {
            Debug.LogError("Modular_Characters not found on player!");
        }
    }

    // Finds gender-specific parts by name under the character model
    GameObject[] FindGenderParts(Transform parentTransform, string genderPartName)
    {
        List<GameObject> genderParts = new List<GameObject>();

        foreach (Transform child in parentTransform)
        {
            if (child.name.Contains(genderPartName))
            {
                genderParts.Add(child.gameObject);
            }
        }
        return genderParts.ToArray();
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

    void ChangeModel(int value, string modelKey, Dictionary<string, GameObject[]> modelDict, ref int currentIndex)
    {
        if (!modelDict.ContainsKey(modelKey))
        {
            Debug.LogWarning($"Model key '{modelKey}' not found in modelDict.");
            return;
        }

        GameObject[] models = modelDict[modelKey];
        if (models == null || models.Length == 0)
        {
            Debug.LogWarning($"No models found for key '{modelKey}'.");
            return;
        }

        // Deactivate all models in the current key group
        foreach (var model in models)
        {
            model.SetActive(false);
        }

        // Update index and ensure it wraps around
        currentIndex = (currentIndex + value + models.Length) % models.Length;

        // Activate the new model
        models[currentIndex].SetActive(true);
    }
    void DisableModel(string modelKey, Dictionary<string, GameObject[]> modelDict)
    {
        if (!modelDict.ContainsKey(modelKey))
        {
            Debug.LogWarning($"Model key '{modelKey}' not found in modelDict.");
            return;
        }

        GameObject[] models = modelDict[modelKey];
        if (models == null || models.Length == 0)
        {
            Debug.LogWarning($"No models found for key '{modelKey}'.");
            return;
        }

        // Get the parent GameObject of the first model in the group
        GameObject parent = models[0].transform.parent?.gameObject;

        if (parent != null)
        {
            // Deactivate the parent GameObject
            parent.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"No parent object found for key '{modelKey}'.");
        }
    }

    void ActivateModel(string modelKey, Dictionary<string, GameObject[]> modelDict)
    {
        if (!modelDict.ContainsKey(modelKey))
        {
            Debug.LogWarning($"Model key '{modelKey}' not found in modelDict.");
            return;
        }

        GameObject[] models = modelDict[modelKey];
        if (models == null || models.Length == 0)
        {
            Debug.LogWarning($"No models found for key '{modelKey}'.");
            return;
        }

        // Get the parent GameObject of the first model in the group
        GameObject parent = models[0].transform.parent?.gameObject;

        if (parent != null)
        {
            // Activate the parent GameObject
            parent.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"No parent object found for key '{modelKey}'.");
        }
    }

    void ChangeFace(int value)
    {
        string key = gender == "Male" ? "HumanMale" : "HumanFemale";
        ChangeModel(value, key, partDictionaries.faceModels, ref currentFaceIndex);
    }
    void ChangeHeadWear(int value)
    {
        string genderKey = gender == "Male" ? "HumanMale" : "HumanFemale";

        if (isHair)
        {
            ChangeModel(value, "Hair", partDictionaries.hairParts, ref currentHairIndex);

            DisableModel(genderKey, partDictionaries.headwearParts);
            DisableModel("Headwear", partDictionaries.hatParts);

            ActivateModel("Hair", partDictionaries.hairParts);
            ActivateModel(genderKey, partDictionaries.eyeBrowsModels);
            ActivateModel(genderKey, partDictionaries.faceModels);
            if (isFacialHair)
            {
                ActivateModel(genderKey, partDictionaries.facialHairModels);
            }
            else if (isMask)
            {
                ActivateModel("Facewear", partDictionaries.maskParts);
            }
        }
        else if (isHelmet)
        {
            ChangeModel(value, genderKey, partDictionaries.headwearParts, ref currentHeadwearIndex);

            DisableModel("Hair", partDictionaries.hairParts);
            DisableModel("Headwear", partDictionaries.hatParts);
            DisableModel("Facewear", partDictionaries.maskParts);
            DisableModel(genderKey, partDictionaries.faceModels);
            DisableModel(genderKey, partDictionaries.eyeBrowsModels);
            if (isFacialHair)
            {
                DisableModel(genderKey, partDictionaries.facialHairModels);
            }
            else if (isMask)
            {
                DisableModel("Facewear", partDictionaries.maskParts);
            }

            ActivateModel(genderKey, partDictionaries.headwearParts);
        }
        else if (isHat)
        {
            ChangeModel(value, "Headwear", partDictionaries.hatParts, ref currentHatsIndex);

            DisableModel("Hair", partDictionaries.hairParts);
            DisableModel(genderKey, partDictionaries.headwearParts);

            ActivateModel(genderKey, partDictionaries.eyeBrowsModels);
            ActivateModel(genderKey, partDictionaries.faceModels);
            ActivateModel("Headwear", partDictionaries.hatParts);
            if (isFacialHair)
            {
                ActivateModel(genderKey, partDictionaries.facialHairModels);
            }
            else if (isMask)
            {
                ActivateModel("Facewear", partDictionaries.maskParts);
            }
        }
        else
        {
            Debug.LogWarning("No valid headwear type is selected in partSelection.");
        }
    }
    void ChangeEyebrow(int value)
    {
        string key = gender == "Male" ? "HumanMale" : "HumanFemale";
        ChangeModel(value, key, partDictionaries.eyeBrowsModels, ref currentEyebrowIndex);
    }
    void ChangeFacialWear(int value)
    {
        string genderKey = gender == "Male" ? "HumanMale" : "HumanFemale";

        if (isFacialHair)
        {
            ChangeModel(value, genderKey, partDictionaries.facialHairModels, ref currentFacialHairIndex);

            ActivateModel(genderKey, partDictionaries.facialHairModels);
            DisableModel("Facewear", partDictionaries.maskParts);
        }
        else if (isMask)
        {
            ChangeModel(value, "Facewear", partDictionaries.maskParts, ref currentmaskIndex);

            DisableModel(genderKey, partDictionaries.facialHairModels);
            ActivateModel("Facewear", partDictionaries.maskParts);
        }

    }

    void ChangeTorso(int value)
    {
        string key = gender == "Male" ? "HumanMale" : "HumanFemale";
        ChangeModel(value, key, partDictionaries.torsoParts, ref currentTorsoIndex);
    }
    void ChangeUpperArm(int value)
    {
        string keyRight = gender == "Male" ? "HumanMale_RightUpperArm" : "HumanFemale_RightUpperArm";
        string keyLeft = gender == "Male" ? "HumanMale_LeftUpperArm" : "HumanFemale_LeftUpperArm";

        ChangeModel(value, keyRight, partDictionaries.upperArmParts, ref currentRightUpperArmIndex);
        ChangeModel(value, keyLeft, partDictionaries.upperArmParts, ref currentLeftUpperArmIndex);
    }
    void ChangeLowerArm(int value)
    {
        string KeyRight = gender == "Male" ? "HumanMale_RightLowerArm" : "HumanFemale_RightLowerArm";
        string KeyLeft = gender == "Male" ? "HumanMale_LeftLowerArm" : "HumanFemale_LeftLowerArm";

        ChangeModel(value, KeyRight, partDictionaries.lowerArmParts, ref currentRightLowerArmIndex);
        ChangeModel(value, KeyLeft, partDictionaries.lowerArmParts, ref currentLeftLowerArmIndex);
    }
    void ChangeHands(int value)
    {
        string KeyRight = gender == "Male" ? "HumanMale_RightHand" : "HumanFemale_RightHand";
        string KeyLeft = gender == "Male" ? "HumanMale_LeftHand" : "HumanFemale_LeftHand";

        ChangeModel(value, KeyRight, partDictionaries.handParts, ref currentRightHandIndex);
        ChangeModel(value, KeyLeft, partDictionaries.handParts, ref currentLeftHandIndex);
    }

    void ChangeHips(int value)
    {
        string key = gender == "Male" ? "HumanMale" : "HumanFemale";
        ChangeModel(value, key, partDictionaries.hipParts, ref currentHipIndex);
    }
    void ChangeLeg(int value)
    {
        string KeyRight = gender == "Male" ? "HumanMale_RightLeg" : "HumanFemale_RightLeg";
        string KeyLeft = gender == "Male" ? "HumanMale_LeftLeg" : "HumanFemale_LeftLeg";
        ChangeModel(value, KeyRight, partDictionaries.legParts, ref currentRightLegIndex);
        ChangeModel(value, KeyLeft, partDictionaries.legParts, ref currentLeftLegIndex);
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
        ChangeModel(modelName, "HeadAttach", partDictionaries.headAttachParts);
    }

    public void ChangeShoulder(string modelName)
    {
        ChangeModel(modelName, "RightShoulderwear", partDictionaries.shoulderParts);
        ChangeModel(modelName, "LeftShoulderwear", partDictionaries.shoulderParts);
    }

    public void ChangeElbow(string modelName)
    {
        ChangeModel(modelName, "RightElbowwear", partDictionaries.elbowParts);
        ChangeModel(modelName, "LeftElbowwear", partDictionaries.elbowParts);
    }

    public void ChangeHipAttach(string modelName)
    {
        ChangeModel(modelName, "HipAttachment", partDictionaries.hipAttachParts);
    }

    public void ChangeKnee(string modelName)
    {
        ChangeModel(modelName, "RightKneewear", partDictionaries.kneeParts);
        ChangeModel(modelName, "LeftKneewear", partDictionaries.kneeParts);
    }

    public void ChangeMantle(string modelName)
    {
        ChangeModel(modelName, "BackAttachment", partDictionaries.mantleParts);
    }

    private void LoadPlayerPrefs()
    {
        // Load gender
        gender = PlayerPrefs.GetString("PlayerGender", "Male"); // Default to Male if no saved data
        if (gender == "Male")
            selectMale();
        else
            selectFemale();

        isHair = PlayerPrefs.GetInt("IsHair", 0) == 1;
        isHelmet = PlayerPrefs.GetInt("IsHelmet", 0) == 1;
        isHat = PlayerPrefs.GetInt("IsHat", 0) == 1;
        isFacialHair = PlayerPrefs.GetInt("IsFacialHair", 0) == 1;
        isMask = PlayerPrefs.GetInt("IsMask", 0) == 1;

        // Load headwear, facewear, hair, and other body parts indices
        currentHeadwearIndex = PlayerPrefs.GetInt("HeadwearIndex", 0);
        currentHatsIndex = PlayerPrefs.GetInt("HatIndex", 0);
        currentmaskIndex = PlayerPrefs.GetInt("MaskIndex", 0);
        currentHairIndex = PlayerPrefs.GetInt("HairIndex", 0);
        currentFaceIndex = PlayerPrefs.GetInt("FaceIndex", 0);
        currentEyebrowIndex = PlayerPrefs.GetInt("EyebrowIndex", 0);
        currentFacialHairIndex = PlayerPrefs.GetInt("FacialHairIndex", 0);

        // Apply the loaded data to the player model
        ChangeHeadWear(0);
        ChangeFace(0);
        ChangeEyebrow(0);
        ChangeFacialWear(0);

        // Load torso and arm parts
        currentTorsoIndex = PlayerPrefs.GetInt("TorsoIndex", 0);
        currentRightUpperArmIndex = PlayerPrefs.GetInt("RightUpperArmIndex", 0);
        currentLeftUpperArmIndex = PlayerPrefs.GetInt("LeftUpperArmIndex", 0);
        currentRightLowerArmIndex = PlayerPrefs.GetInt("RightLowerArmIndex", 0);
        currentLeftLowerArmIndex = PlayerPrefs.GetInt("LeftLowerArmIndex", 0);
        currentRightHandIndex = PlayerPrefs.GetInt("RightHandIndex", 0);
        currentLeftHandIndex = PlayerPrefs.GetInt("LeftHandIndex", 0);

        ChangeTorso(0);
        ChangeUpperArm(0);
        ChangeLowerArm(0);
        ChangeHands(0);

        // Load hips and leg parts
        currentHipIndex = PlayerPrefs.GetInt("HipIndex", 0);
        currentRightLegIndex = PlayerPrefs.GetInt("RightLegIndex", 0);
        currentLeftLegIndex = PlayerPrefs.GetInt("LeftLegIndex", 0);

        ChangeHips(0);
        ChangeLeg(0);

        // Load attachments
        headAttachKey = PlayerPrefs.GetString("HeadAttach", "00");
        Debug.Log($"Loaded HeadAttach: {headAttachKey}");
        mantleKey = PlayerPrefs.GetString("BackAttachment", "00");
        shoulderKey = PlayerPrefs.GetString("LeftShoulderwear", "00");
        elbowKey = PlayerPrefs.GetString("LeftElbowwear", "00");
        hipAttachKey = PlayerPrefs.GetString("HipAttachment", "00");
        kneeKey = PlayerPrefs.GetString("LeftKneewear", "00");

        ChangeHeadAttach(headAttachKey);
        ChangeMantle(mantleKey);
        ChangeShoulder(shoulderKey);
        ChangeElbow(elbowKey);
        ChangeHipAttach(hipAttachKey);
        ChangeKnee(kneeKey);

        Debug.Log("Player preferences loaded.");
    }
}
