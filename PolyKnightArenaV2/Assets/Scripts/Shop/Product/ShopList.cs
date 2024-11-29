using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.UIElements;



public class ShopInventory : MonoBehaviour
{
    [ListDrawerSettings(Expanded = true, DraggableItems = false, ShowPaging = true, NumberOfItemsPerPage = 5)]
    public List<Product> products;

    // Optionally, add some helper methods if needed
    [Button("Add Product")]
    public void AddProduct()
    {
        products.Add(new Product { productName = "New Product", price = 0 });
    }

    void Start()
    {
        products = new List<Product>();
        headProducts();
        elbowProducts();
        shoulderProducts();
        kneeProducts();
        mantleProducts();
        hipProducts();
    }

    //Hard code products
    public void headProducts()
    {
        products.Add(new Product
        {
            productName = "Horse Tail",
            requiredLevel = 8,
            price = 1200,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_02",
            LeftmodelName = null,
            description = "Adding a touch of elegance and flair to a knight's armor.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_02")
        });

        products.Add(new Product
        {
            productName = "Helmet Crest",
            requiredLevel = 12,
            price = 2400,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_01",
            LeftmodelName = null,
            description = "An ornamental crest for a knight's helmet.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_01")
         });

        products.Add(new Product
        {
            productName = "Feathered Crest",
            requiredLevel = 15,
            price = 4000,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_10",
            LeftmodelName = null,
            description = "A crest adorned with small feathers, symbolizing honor and valor.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_10")
        });

        products.Add(new Product
        {
            productName = "Feathered Plume",
            requiredLevel = 22,
            price = 8000,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_05",
            LeftmodelName = null,
            description = "Adding an air of elegance and majesty to the knight’s appearance.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_05")
        });

        products.Add(new Product
        {
            productName = "Helmet Crest Emblem",
            requiredLevel = 28,
            price = 10000,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_09",
            LeftmodelName = null,
            description = "A small, pointed emblem attached to the front of the helmet, symbolizing honor and bravery.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_09")
        });

        products.Add(new Product
        {
            productName = "Side Feathered Plume",
            requiredLevel = 33,
            price = 13000,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_13",
            LeftmodelName = null,
            description = "A graceful side-mounted plume featuring multiple feathers, adding a touch of elegance and flair to the helmet.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_13")
        });

        products.Add(new Product
        {
            productName = "Antler Horns",
            requiredLevel = 55,
            price = 25000,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_04",
            LeftmodelName = null,
            description = "A rugged helmet attachment featuring antler-like horns, giving the wearer a wild and powerful presence.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_04")
        });

        products.Add(new Product
        {
            productName = "Cross Emblem",
            requiredLevel = 60,
            price = 30000,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_11",
            LeftmodelName = null,
            description = "A distinguished cross-shaped helmet emblem, symbolizing faith and protection.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_11")
        });

        products.Add(new Product
        {
            productName = "Side Feathered Plume",
            requiredLevel = 62,
            price = 33000,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_12",
            LeftmodelName = null,
            description = "A graceful side-mounted plume featuring multiple feathers, adding a touch of elegance and flair to the helmet.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_12")
        });

        products.Add(new Product
        {
            productName = "Asterix",
            requiredLevel = 65,
            price = 37000,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_03",
            LeftmodelName = null,
            description = "These wings add a touch of mythical style and agility, reminiscent of legendary heroes.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_03")
        });

        products.Add(new Product
        {
            productName = "Steel Mohawk",
            requiredLevel = 70,
            price = 40000,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_06",
            LeftmodelName = null,
            description = "A bold helmet attachment resembling a metallic mohawk, giving the wearer a fierce and rebellious look.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_06")
        });

        products.Add(new Product
        {
            productName = "Dragon Horns",
            requiredLevel = 80,
            price = 45000,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_07",
            LeftmodelName = null,
            description = "A fierce helmet attachment shaped like dragon horns, giving the wearer a powerful, mythical appearance.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_07")
        });
        products.Add(new Product
        {
            productName = "Crown",
            requiredLevel = 100,
            price = 100000,
            productType = "Helmet",
            RightmodelName = "Chr_HelmetAttachment_08",
            LeftmodelName = null,
            description = "A majestic helmet attachment resembling a crown, giving the wearer a regal and noble appearance.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Head/Chr_HelmetAttachment_08")
        });
    }
    public void elbowProducts()
    {
        products.Add(new Product
        {
            productName = "Elbow Guard",
            requiredLevel = 7,
            price = 550,
            productType = "Elbow",
            RightmodelName = "Chr_ElbowAttachRight_01",
            LeftmodelName = "Chr_ElbowAttachLeft_01",
            description = "A simple elbow guard, providing basic protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Elbow/Chr_ElbowAttachLeft_01")
        });

        products.Add(new Product
        {
            productName = "Reinforced Elbow Guard",
            requiredLevel = 10,
            price = 1500,
            productType = "Elbow",
            RightmodelName = "Chr_ElbowAttachRight_02",
            LeftmodelName = "Chr_ElbowAttachLeft_02",
            description = "A more advanced elbow guard, providing better protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Elbow/Chr_ElbowAttachLeft_02")
        });

        products.Add(new Product
        {
            productName = "Elbow Guard with Spikes",
            requiredLevel = 13,
            price = 3500,
            productType = "Elbow",
            RightmodelName = "Chr_ElbowAttachRight_06",
            LeftmodelName = "Chr_ElbowAttachLeft_06",
            description = "An advanced elbow guard featuring spikes, providing both protection and intimidation.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Elbow/Chr_ElbowAttachLeft_06")
        });

        products.Add(new Product
        {
            productName = "Spiked Elbow Guard",
            requiredLevel = 20,
            price = 6000,
            productType = "Elbow",
            RightmodelName = "Chr_ElbowAttachRight_03",
            LeftmodelName = "Chr_ElbowAttachLeft_03",
            description = "An elbow guard featuring sharped style, providing both protection and intimidation.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Elbow/Chr_ElbowAttachLeft_03")
        });

        products.Add(new Product
        {
            productName = "Elbow Guard with Shield",
            requiredLevel = 35,
            price = 15000,
            productType = "Elbow",
            RightmodelName = "Chr_ElbowAttachRight_04",
            LeftmodelName = "Chr_ElbowAttachLeft_04",
            description = "An advanced elbow guard featuring a shield, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Elbow/Chr_ElbowAttachLeft_04")
        });

        products.Add(new Product
        {
            productName = "Heavy Elbow Protector",
            requiredLevel = 40,
            price = 20000,
            productType = "Elbow",
            RightmodelName = "Chr_ElbowAttachRight_05",
            LeftmodelName = "Chr_ElbowAttachLeft_05",
            description = "A robust elbow guard offering maximum protection for intense combat situations.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Elbow/Chr_ElbowAttachLeft_05")
        });
    }
    public void shoulderProducts()
    {
        products.Add(new Product
        {
            productName = "Shoulder Guard",
            requiredLevel = 9,
            price = 600,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_17",
            LeftmodelName = "Chr_ShoulderAttachLeft_17",
            description = "A simple shoulder guard, providing basic protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_17")
        });

        products.Add(new Product
        {
            productName = "Reinforced Shoulder",
            requiredLevel = 17,
            price = 1200,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_04",
            LeftmodelName = "Chr_ShoulderAttachLeft_04",
            description = "A more advanced shoulder guard, providing better protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_04")
        });

        products.Add(new Product
        {
            productName = "Hex Plate Shoulders",
            requiredLevel = 16,
            price = 10000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_01",
            LeftmodelName = "Chr_ShoulderAttachLeft_01",
            description = "A pair of sturdy shoulder guards featuring a hexagonal design for added durability and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_01")
        });

        products.Add(new Product
        {
            productName = "Angular Shoulder Plates",
            requiredLevel = 14,
            price = 3750,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_02",
            LeftmodelName = "Chr_ShoulderAttachLeft_02",
            description = "A pair of angular shoulder plates, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_02")
        });

        products.Add(new Product
        {
            productName = "Spiked Shield Shoulder Guard",
            requiredLevel = 29,
            price = 12500,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_03",
            LeftmodelName = "Chr_ShoulderAttachLeft_03",
            description = "An advanced shoulder guard featuring a shield and spikes, providing both protection and intimidation.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_03")
        });

        products.Add(new Product
        {
            productName = "Shoulder Guard with Spikes",
            requiredLevel = 25,
            price = 10000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_06",
            LeftmodelName = "Chr_ShoulderAttachLeft_06",
            description = "An advanced shoulder guard featuring spikes, providing both protection and intimidation.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_06")
        });

        products.Add(new Product
        {
            productName = "Reinforced Spiked Pauldrons",
            requiredLevel = 43,
            price = 20000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_05",
            LeftmodelName = "Chr_ShoulderAttachLeft_05",
            description = "A pair of robust shoulder guards featuring spikes, providing both protection and intimidation.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_05")
        });

        products.Add(new Product
        {
            productName = "Guarded Flare Shoulders",
            requiredLevel = 66,
            price = 35000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_07",
            LeftmodelName = "Chr_ShoulderAttachLeft_07",
            description = "A pair of shoulder guards featuring a flared design, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_07")
        });

        products.Add(new Product
        {
            productName = "Fortified Spiked Shoulders",
            requiredLevel = 73,
            price = 40000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_08",
            LeftmodelName = "Chr_ShoulderAttachLeft_08",
            description = "A pair of robust shoulder guards featuring spikes, providing both protection and intimidation.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_08")
        });

        products.Add(new Product
        {
            productName = "Wolf Head Pauldrons",
            requiredLevel = 85,
            price = 45000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_09",
            LeftmodelName = "Chr_ShoulderAttachLeft_09",
            description = "Intimidating shoulder guards shaped like snarling wolf heads, embodying the spirit of the wild.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_09")
        });

        products.Add(new Product
        {
            productName = "Reinforced Spiked Pauldrons",
            requiredLevel = 56,
            price = 30000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_10",
            LeftmodelName = "Chr_ShoulderAttachLeft_10",
            description = "A pair of robust shoulder guards featuring spikes, providing both protection and intimidation.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_10")
        });

        products.Add(new Product
        {
            productName = "Reinforced Plate Pauldrons",
            requiredLevel = 52,
            price = 23500,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_11",
            LeftmodelName = "Chr_ShoulderAttachLeft_11",
            description = "A pair of sturdy shoulder guards featuring a plate design for added durability and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_11")
        });

        products.Add(new Product
        {
            productName = "Reinforced Pauldrons",
            requiredLevel = 44,
            price = 19000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_12",
            LeftmodelName = "Chr_ShoulderAttachLeft_12",
            description = "A pair of robust shoulder guards, providing maximum protection for intense combat situations.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_12")
        });

        products.Add(new Product
        {
            productName = "Reinforced Shoulder Guards",
            requiredLevel = 11,
            price = 2500,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_13",
            LeftmodelName = "Chr_ShoulderAttachLeft_13",
            description = "A pair of sturdy shoulder guards, providing basic protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_13")
        });

        products.Add(new Product
        {
            productName = "Knights Pauldrons",
            requiredLevel = 21,
            price = 7000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_14",
            LeftmodelName = "Chr_ShoulderAttachLeft_14",
            description = "A pair of shoulder guards featuring a knightly design, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_14")
        });

        products.Add(new Product
        {
            productName = "Leathered Reinforced Shoulder Protection",
            requiredLevel = 6,
            price = 500,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_15",
            LeftmodelName = "Chr_ShoulderAttachLeft_15",
            description = "A pair of leather shoulder guards, providing basic protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_15")
        });

        products.Add(new Product
        {
            productName = "Leather Shoulder Guards",
            requiredLevel = 5,
            price = 400,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_16",
            LeftmodelName = "Chr_ShoulderAttachLeft_16",
            description = "A pair of leather shoulder guards, providing basic protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_16")
        });

        products.Add(new Product
        {
            productName = "Bear headed Pauldrons",
            requiredLevel = 93,
            price = 83000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_18",
            LeftmodelName = "Chr_ShoulderAttachLeft_18",
            description = "Intimidating shoulder guards shaped like snarling bear heads, embodying the spirit of the wild.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_18")
        });

        products.Add(new Product
        {
            productName = "King's Pauldrons",
            requiredLevel = 76,
            price = 45000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_19",
            LeftmodelName = "Chr_ShoulderAttachLeft_19",
            description = "A pair of shoulder guards featuring a regal design, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_19")
        });

        products.Add(new Product
        {
            productName = "Juggernaut Pauldrons",
            requiredLevel = 83,
            price = 74000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_20",
            LeftmodelName = "Chr_ShoulderAttachLeft_20",
            description = "A pair of robust shoulder guards, providing maximum protection for intense combat situations.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_20")
        });

        products.Add(new Product
        {
            productName = "Paladin Pauldrons",
            requiredLevel = 86,
            price = 78000,
            productType = "Shoulder",
            RightmodelName = "Chr_ShoulderAttachRight_21",
            LeftmodelName = "Chr_ShoulderAttachLeft_21",
            description = "A pair of shoulder guards featuring a paladin design, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Shoulder/Chr_ShoulderAttachLeft_21")
        });
    }
    public void kneeProducts()
    {
        products.Add(new Product
        {
            productName = "Knee Guard",
            requiredLevel = 5,
            price = 500,
            productType = "Knee",
            RightmodelName = "Chr_KneeAttachRight_10",
            LeftmodelName = "Chr_KneeAttachLeft_10",
            description = "A simple knee guard, providing basic protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Knee/Chr_KneeAttachLeft_10")
        });

        products.Add(new Product
        {
            productName = "Reinforced Knee Guard",
            requiredLevel = 11,
            price = 1000,
            productType = "Knee",
            RightmodelName = "Chr_KneeAttachRight_02",
            LeftmodelName = "Chr_KneeAttachLeft_02",
            description = "A more advanced knee guard, providing better protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Knee/Chr_KneeAttachLeft_02")
        });

        products.Add(new Product
        {
            productName = "Spiked Knee Guard",
            requiredLevel = 23,
            price = 5000,
            productType = "Knee",
            RightmodelName = "Chr_KneeAttachRight_06",
            LeftmodelName = "Chr_KneeAttachLeft_06",
            description = "An advanced knee guard featuring spikes, providing both protection and intimidation.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Knee/Chr_KneeAttachLeft_06")
        });

        products.Add(new Product
        {
            productName = "Knee Guard with Shield",
            requiredLevel = 13,
            price = 500,
            productType = "Knee",
            RightmodelName = "Chr_KneeAttachRight_04",
            LeftmodelName = "Chr_KneeAttachLeft_04",
            description = "An advanced knee guard featuring a shield, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Knee/Chr_KneeAttachLeft_04")
        });

        products.Add(new Product
        {
            productName = "Heavy Knee Protector",
            requiredLevel = 18,
            price = 8000,
            productType = "Knee",
            RightmodelName = "Chr_KneeAttachRight_08",
            LeftmodelName = "Chr_KneeAttachLeft_08",
            description = "A robust knee guard offering maximum protection for intense combat situations.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Knee/Chr_KneeAttachLeft_08")
        });

        products.Add(new Product
        {
            productName = "Knight's Knee Guard",
            requiredLevel = 24,
            price = 6000,
            productType = "Knee",
            RightmodelName = "Chr_KneeAttachRight_01",
            LeftmodelName = "Chr_KneeAttachLeft_01",
            description = "A pair of sturdy knee guards featuring a knightly design, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Knee/Chr_KneeAttachLeft_01")
        });

        products.Add(new Product
        {
            productName = "Reinforced Rubbber Knee Guard",
            requiredLevel = 16,
            price = 4000,
            productType = "Knee",
            RightmodelName = "Chr_KneeAttachRight_03",
            LeftmodelName = "Chr_KneeAttachLeft_03",
            description = "A pair of rubber knee guards, providing basic protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Knee/Chr_KneeAttachLeft_03")
        });

        products.Add(new Product
        {
            productName = "Soldier's knee guard",
            requiredLevel = 9,
            price = 1500,
            productType = "Knee",
            RightmodelName = "Chr_KneeAttachRight_05",
            LeftmodelName = "Chr_KneeAttachLeft_05",
            description = "A pair of knee guards featuring a soldier design, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Knee/Chr_KneeAttachLeft_05")
        });

        products.Add(new Product
        {
            productName = "King's Guards Knee Pads",
            requiredLevel = 32,
            price = 13000,
            productType = "Knee",
            RightmodelName = "Chr_KneeAttachRight_07",
            LeftmodelName = "Chr_KneeAttachLeft_07",
            description = "A pair of knee pads featuring a regal design, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Knee/Chr_KneeAttachLeft_07")
        });

        products.Add(new Product
        {
            productName = "Reinforced Knee Guard+",
            requiredLevel = 28,
            price = 10000,
            productType = "Knee",
            RightmodelName = "Chr_KneeAttachRight_09",
            LeftmodelName = "Chr_KneeAttachLeft_09",
            description = "A pair of sturdy knee guards, providing basic protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Knee/Chr_KneeAttachLeft_09")
        });

        products.Add(new Product
        {
            productName = "Flat Knee Guard",
            requiredLevel = 43,
            price = 20000,
            productType = "Knee",
            RightmodelName = "Chr_KneeAttachRight_11",
            LeftmodelName = "Chr_KneeAttachLeft_11",
            description = "A pair of flat knee guards, providing basic protection for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Knee/Chr_KneeAttachLeft_11")
        });
    }
    public void mantleProducts()
    {
        products.Add(new Product
        {
            productName = "Small Backpack",
            requiredLevel = 32,
            price = 13500,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_02",
            LeftmodelName = null,
            description = "A small backpack, providing extra storage space for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_02")
        });

        products.Add(new Product
        {
            productName = "Large Backpack",
            requiredLevel = 53,
            price = 10000,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_01",
            LeftmodelName = null,
            description = "A large backpack, providing extra storage space for the wearer.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_01")
        });

        products.Add(new Product
        {
            productName = "Night Crows mantle",
            requiredLevel = 64,
            price = 23250,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_03",
            LeftmodelName = null,
            description = "A black mantle featuring a pair of night crows, symbolizing mystery and darkness.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_03")
        });

        products.Add(new Product
        {
            productName = "Bear Leather",
            requiredLevel = 72,
            price = 38500,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_04",
            LeftmodelName = null,
            description = "A rugged mantle made from bear leather, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_04")
        });

        products.Add(new Product
        {
            productName = "King's Cloak",
            requiredLevel = 81,
            price = 46760,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_05",
            LeftmodelName = null,
            description = "A regal cloak featuring a royal design, providing both protection and style.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_05")
        });

        products.Add(new Product
        {
            productName = "The Adventurer",
            requiredLevel = 54,
            price = 15000,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_06",
            LeftmodelName = null,
            description = "A rugged mantle featuring a compass and map, symbolizing adventure and exploration.",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_06")
        });

        products.Add(new Product
        {
            productName = "The Stripper",
            requiredLevel = 34,
            price = 11120,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_07",
            LeftmodelName = null,
            description = "A thinner mantle for the happy people",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_07")
        });

        products.Add(new Product
        {
            productName = "The spiked model mantle",
            requiredLevel = 26,
            price = 9000,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_08",
            LeftmodelName = null,
            description = "A mantle with a spiked look",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_08")
        });

        products.Add(new Product
        {
            productName = "Joker's mantle",
            requiredLevel = 42,
            price = 19000,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_09",
            LeftmodelName = null,
            description = "A shorter mantle for the ones luring",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_09")
        });

        products.Add(new Product
        {
            productName = "The Knight's Mantle",
            requiredLevel = 37,
            price = 15000,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_10",
            LeftmodelName = null,
            description = "A mantle for the knights",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_10")
        });

        products.Add(new Product
        {
            productName = "Kings hand's mantle",
            requiredLevel = 23,
            price = 8000,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_11",
            LeftmodelName = null,
            description = "A mantle for the kings hand",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_11")
        });
        products.Add(new Product
        {
            productName = "The prince",
            requiredLevel = 63,
            price = 23000,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_12",
            LeftmodelName = null,
            description = "A mantle for the prince",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_12")
        });

        products.Add(new Product
        {
            productName = "The queen",
            requiredLevel = 76,
            price = 35000,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_13",
            LeftmodelName = null,
            description = "A mantle for the queen",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_13")
        });

        products.Add(new Product
        {
            productName = "The Dead",
            requiredLevel = 95,
            price = 80000,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_14",
            LeftmodelName = null,
            description = "A mantle for the dead",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_14")
        });

        products.Add(new Product
        {
            productName = "The King's Mantle",
            requiredLevel = 99,
            price = 100000,
            productType = "Mantle",
            RightmodelName = "Chr_BackAttachment_15",
            LeftmodelName = null,
            description = "A mantle for the king",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Mantle/Chr_BackAttachment_15")
        });
    }
    public void hipProducts()
    {
        products.Add(new Product
        {
            productName = "Small Coins Bag",
            requiredLevel = 51,
            price = 10000,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_01",
            LeftmodelName = null,
            description = "A small coin bag, providing extra coins in games",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_01")
        });
        
        products.Add(new Product
        {
            productName = "Large Coins Bag",
            requiredLevel = 71,
            price = 32000,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_07",
            LeftmodelName = null,
            description = "A large coin bag, providing extra coins in games",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_07")
        });

        products.Add(new Product
        {
            productName = "Experience booster drink",
            requiredLevel = 57,
            price = 19500,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_02",
            LeftmodelName = null,
            description = "A drink that boosts the experience gained in games",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_02")
        });

        products.Add(new Product
        {
            productName = "Health booster drink",
            requiredLevel = 34,
            price = 8000,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_05",
            LeftmodelName = null,
            description = "A drink that boosts the health gained in games",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_05")
        });

        products.Add(new Product
        {
            productName = "Perk adder pouch x2",
            requiredLevel = 26,
            price = 12000,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_03",
            LeftmodelName = null,
            description = "A pouch that adds extra perk to the player",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_03")
        });

        products.Add(new Product
        {
            productName = "Rope attachment",
            requiredLevel = 42,
            price = 18000,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_04",
            LeftmodelName = null,
            description = "A rope that allows you to jump higher",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_04")
        });

        products.Add(new Product
        {
            productName = "Perk adder pouch",
            requiredLevel = 37,
            price = 15000,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_06",
            LeftmodelName = null,
            description = "A pouch that adds an extra perk to the player",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_06")
        });

        products.Add(new Product
        {
            productName = "Experience booster x1",
            requiredLevel = 74,
            price = 40000,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_08",
            LeftmodelName = null,
            description = "A drink that boosts the experience gained in games",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_08")
        });

        products.Add(new Product
        {
            productName = "Health booster drink x2",
            requiredLevel = 95,
            price = 80000,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_09",
            LeftmodelName = null,
            description = "A drink that boosts the health gained in games",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_09")
        });

        products.Add(new Product
        {
            productName = "Perk adder pouch x3",
            requiredLevel = 92,
            price = 110000,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_10",
            LeftmodelName = null,
            description = "A pouch that adds extra perk to the player",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_10")
        });

        products.Add(new Product
        {
            productName = "Extra weapon pouch",
            requiredLevel = 78,
            price = 45000,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_11",
            LeftmodelName = null,
            description = "A pouch that allows you to carry an extra weapon",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_11")
        });

        products.Add(new Product
        {
            productName = "Strength booster drink",
            requiredLevel = 83,
            price = 75000,
            productType = "Hip",
            RightmodelName = "Chr_HipsAttachment_12",
            LeftmodelName = null,
            description = "A drink that boosts the strength gained in games",
            productImage = Resources.Load<Sprite>("Images/ProductIcons/Hip/Chr_HipsAttachment_12")
        });
    }
}
//100 levlar = 78 produkter , 10 perks 
//Upptagna levlar från hjälmar = 8,12,15,22,28,33,55,60,62,65,70,80,100
//Upptagna levlar från armbågar = 7,10,20,35,40,13
//Upptagna levlar från axlar = 6,9,25,16,14,29,43,66,73,85,56,52,44,11,21,5,17,93,76,83,86
//Upptagna levlar från knän = 5,11,23,13,18,24,16,9,32,28,43
//Upptagna levlar från mantlar = 32,53,64,72,81,54,34,26,42,37,23,63,76,95,99
//Upptagna levlar från höfter =