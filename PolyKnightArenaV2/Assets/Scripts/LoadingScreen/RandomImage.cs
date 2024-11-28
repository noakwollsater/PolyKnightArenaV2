using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomImage : MonoBehaviour
{
    public Image imageComponent;
    public List<Sprite> Image;

    // Start is called before the first frame update
    void Start()
    {
        LoadRandomImage();
    }


    public void LoadRandomImage()
    {
        if (Image.Count == 0)
        {
            Debug.LogError("No images assigned to the list!");
        }

        int randomIndex = Random.Range(0, Image.Count);
        imageComponent.sprite = Image[randomIndex];
    }
}
