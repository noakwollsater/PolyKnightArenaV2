using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.InputSystem;
using Unity.VisualScripting.Antlr3.Runtime;

public class ChangeModelParts : MonoBehaviour
{
    [SerializeField] private Button Savebtn;

    [Header("Player")]
    [SerializeField] public GameObject player;

    [Header("Head")]
    [SerializeField] private Button headWearBtn_L;
    [SerializeField] private Button headWearBtn_R;
    [SerializeField] private Button faceBtn_L;
    [SerializeField] private Button faceBtn_R;
    [SerializeField] private Button eyeBrowsBtn_L;
    [SerializeField] private Button eyeBrowsBtn_R;
    [SerializeField] private Button facialHairBtn_L;
    [SerializeField] private Button facialHairBtn_R;

    [Header("Torso")]
    [SerializeField] private Button torsoBtn_L;
    [SerializeField] private Button torsoBtn_R;
    [SerializeField] private Button UpperArm_L;
    [SerializeField] private Button UpperArm_R;
    [SerializeField] private Button LowerArm_L;
    [SerializeField] private Button LowerArm_R;
    [SerializeField] private Button Hand_L;
    [SerializeField] private Button Hand_R;

    [Header("Hip")]
    [SerializeField] private Button hipBtn_L;
    [SerializeField] private Button hipBtn_R;
    [SerializeField] private Button leg_L;
    [SerializeField] private Button leg_R;

    public PartDictionaries partDictionaries;
    public PartSelection partSelection;

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

    private string gender;

    private void Start()
    {
        partDictionaries = GetComponent<PartDictionaries>();
        partSelection = GetComponent<PartSelection>();

        FindModels();
        AddListeners();
    }

    private void Update()
    {
        gender = PlayerPrefs.GetString("PlayerGender", "Male");
    }

    void AddListeners()
    {
        headWearBtn_R.onClick.AddListener(() => ChangeHeadWear(1));
        headWearBtn_L.onClick.AddListener(() => ChangeHeadWear(-1));
        faceBtn_R.onClick.AddListener(() => ChangeFace(1));
        faceBtn_L.onClick.AddListener(() => ChangeFace(-1));
        eyeBrowsBtn_R.onClick.AddListener(() => ChangeEyebrow(1));
        eyeBrowsBtn_L.onClick.AddListener(() => ChangeEyebrow(-1));
        facialHairBtn_R.onClick.AddListener(() => ChangeFacialWear(1));
        facialHairBtn_L.onClick.AddListener(() => ChangeFacialWear(-1));
        torsoBtn_R.onClick.AddListener(() => ChangeTorso(1));
        torsoBtn_L.onClick.AddListener(() => ChangeTorso(-1));
        UpperArm_R.onClick.AddListener(() => ChangeUpperArm(1));
        UpperArm_L.onClick.AddListener(() => ChangeUpperArm(-1));
        LowerArm_R.onClick.AddListener(() => ChangeLowerArm(1));
        LowerArm_L.onClick.AddListener(() => ChangeLowerArm(-1));
        Hand_R.onClick.AddListener(() => ChangeHands(1));
        Hand_L.onClick.AddListener(() => ChangeHands(-1));
        hipBtn_R.onClick.AddListener(() => ChangeHips(1));
        hipBtn_L.onClick.AddListener(() => ChangeHips(-1));
        leg_R.onClick.AddListener(() => ChangeLeg(1));
        leg_L.onClick.AddListener(() => ChangeLeg(-1));

        Savebtn.onClick.AddListener(() => SaveCharacter());
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
        Debug.Log($"Changed model to index {currentIndex} for key '{modelKey}'");
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

        if (partSelection.isHair)
        {
            ChangeModel(value, "Hair", partDictionaries.hairParts, ref currentHairIndex);

            DisableModel(genderKey, partDictionaries.headwearParts);
            DisableModel("Headwear", partDictionaries.hatParts);

            ActivateModel("Hair", partDictionaries.hairParts);
            ActivateModel(genderKey, partDictionaries.eyeBrowsModels);
            ActivateModel(genderKey, partDictionaries.faceModels);
            if (partSelection.isFacialHair)
            {
                ActivateModel(genderKey, partDictionaries.facialHairModels);
            }
            else if (partSelection.isMask)
            {
                ActivateModel("Facewear", partDictionaries.maskParts);
            }
        }
       else if (partSelection.isHelmet)
        {
            ChangeModel(value, genderKey, partDictionaries.headwearParts, ref currentHeadwearIndex);
            
            DisableModel("Hair", partDictionaries.hairParts);
            DisableModel("Headwear", partDictionaries.hatParts);
            DisableModel("Facewear", partDictionaries.maskParts);
            DisableModel(genderKey, partDictionaries.faceModels);
            DisableModel(genderKey, partDictionaries.eyeBrowsModels);
            if (partSelection.isFacialHair)
            {
                DisableModel(genderKey, partDictionaries.facialHairModels);
            }
            else if (partSelection.isMask)
            {
                DisableModel("Facewear", partDictionaries.maskParts);
            }

            ActivateModel(genderKey, partDictionaries.headwearParts);
        }
        else if (partSelection.isHat)
        {
            ChangeModel(value, "Headwear", partDictionaries.hatParts, ref currentHatsIndex);

            DisableModel("Hair", partDictionaries.hairParts);
            DisableModel(genderKey, partDictionaries.headwearParts);

            ActivateModel(genderKey, partDictionaries.eyeBrowsModels);
            ActivateModel(genderKey, partDictionaries.faceModels);
            ActivateModel("Headwear", partDictionaries.hatParts);
            if (partSelection.isFacialHair)
            {
                ActivateModel(genderKey, partDictionaries.facialHairModels);
            }
            else if (partSelection.isMask)
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

        if (partSelection.isFacialHair)
        {
            ChangeModel(value, genderKey, partDictionaries.facialHairModels, ref currentFacialHairIndex);

            ActivateModel(genderKey, partDictionaries.facialHairModels);
            DisableModel("Facewear", partDictionaries.maskParts);
        }
        else if (partSelection.isMask)
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

    void SaveCharacter()
    {
        PlayerPrefs.SetInt("IsHair", partSelection.isHair ? 1 : 0);
        PlayerPrefs.SetInt("IsHelmet", partSelection.isHelmet ? 1 : 0);
        PlayerPrefs.SetInt("IsHat", partSelection.isHat ? 1 : 0);
        PlayerPrefs.SetInt("IsFacialHair", partSelection.isFacialHair ? 1 : 0);
        PlayerPrefs.SetInt("IsMask", partSelection.isMask ? 1 : 0);

        // Save headwear, facewear, hair, and other body parts indices
        PlayerPrefs.SetInt("HeadwearIndex", currentHeadwearIndex);
        PlayerPrefs.SetInt("HatIndex", currentHatsIndex);
        PlayerPrefs.SetInt("MaskIndex", currentmaskIndex);
        PlayerPrefs.SetInt("HairIndex", currentHairIndex);
        PlayerPrefs.SetInt("FaceIndex", currentFaceIndex);
        PlayerPrefs.SetInt("EyebrowIndex", currentEyebrowIndex);
        PlayerPrefs.SetInt("FacialHairIndex", currentFacialHairIndex);

        // Save torso and arm parts
        PlayerPrefs.SetInt("TorsoIndex", currentTorsoIndex);
        PlayerPrefs.SetInt("RightUpperArmIndex", currentRightUpperArmIndex);
        PlayerPrefs.SetInt("LeftUpperArmIndex", currentLeftUpperArmIndex);
        PlayerPrefs.SetInt("RightLowerArmIndex", currentRightLowerArmIndex);
        PlayerPrefs.SetInt("LeftLowerArmIndex", currentLeftLowerArmIndex);
        PlayerPrefs.SetInt("RightHandIndex", currentRightHandIndex);
        PlayerPrefs.SetInt("LeftHandIndex", currentLeftHandIndex);

        // Save hips and leg parts
        PlayerPrefs.SetInt("HipIndex", currentHipIndex);
        PlayerPrefs.SetInt("RightLegIndex", currentRightLegIndex);
        PlayerPrefs.SetInt("LeftLegIndex", currentLeftLegIndex);

        // Save preferences
        PlayerPrefs.Save();
        Debug.Log("Player preferences saved.");
    }
}
