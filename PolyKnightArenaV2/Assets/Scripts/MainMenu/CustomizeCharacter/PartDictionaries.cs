using System.Collections.Generic;
using UnityEngine;

public class PartDictionaries : MonoBehaviour
{
    [Header("Gender Body Parts")]
    public GameObject[] maleParts;
    public GameObject[] femaleParts;

    [Header("Model Parts")]
    //Head parts
    public Dictionary<string, GameObject[]> headwearParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> hatParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> maskParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> hairParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> faceModels = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> eyeBrowsModels = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> facialHairModels = new Dictionary<string, GameObject[]>();

    //Torso parts
    public Dictionary<string, GameObject[]> torsoParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> upperArmParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> lowerArmParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> handParts = new Dictionary<string, GameObject[]>();

    //Hip parts
    public Dictionary<string, GameObject[]> hipParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> legParts = new Dictionary<string, GameObject[]>();

    //Attachments
    public Dictionary<string, GameObject[]> headAttachParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> mantleParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> shoulderParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> elbowParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> hipAttachParts = new Dictionary<string, GameObject[]>();
    public Dictionary<string, GameObject[]> kneeParts = new Dictionary<string, GameObject[]>();

    public Dictionary<string, int> defaultIndices;

    public void InitializeDefaults()
    {
        // Define the default indices for each customizable part
        defaultIndices = new Dictionary<string, int>
    {
        {"HeadwearIndex", 0},
        {"HatIndex", 0},
        {"MaskIndex", 0},
        {"HeadAttach", 0},
        {"HairIndex", 0},
        {"FaceIndex", 0},
        {"EyebrowIndex", 0},
        {"FacialHairIndex", 0},
        {"TorsoIndex", 0},
        {"MantleIndex", 0},
        {"RightShoulderIndex", 0},
        {"LeftShoulderIndex", 0},
        {"RightUpperArmIndex", 0},
        {"LeftUpperArmIndex", 0},
        {"LeftEblowIndex", 0},
        {"RightEblowIndex", 0},
        {"RightLowerArmIndex", 0},
        {"LeftLowerArmIndex", 0},
        {"RightHandIndex", 0},
        {"LeftHandIndex", 0},
        {"HipIndex", 0},
        {"HipAttachIndex", 0},
        {"RightKneeIndex", 0},
        {"LeftKneeIndex", 0},
        {"RightLegIndex", 0},
        {"LeftLegIndex", 0}
    };
    }
}
