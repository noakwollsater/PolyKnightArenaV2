using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class Product
{
    [BoxGroup("General Information")]
    public string RightmodelName;

    [BoxGroup("General Information")]
    public string LeftmodelName;

    [BoxGroup("General Information")]
    public int requiredLevel;

    [BoxGroup("General Information")]
    public string productType;

    [BoxGroup("General Information")]
    public string productName;

    [BoxGroup("General Information")]
    [PreviewField]
    public Sprite productImage;

    [BoxGroup("Pricing")]
    public int price;

    [BoxGroup("Details")]
    [TextArea]
    public string description;
}