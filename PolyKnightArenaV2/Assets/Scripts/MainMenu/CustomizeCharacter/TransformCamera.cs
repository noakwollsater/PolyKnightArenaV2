using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TransformCamera : MonoBehaviour
{
    [SerializeField] private Camera Camera;

    [SerializeField] private Image gender;

    [SerializeField] private GameObject Player;

    [SerializeField] private Button headBtn;
    [SerializeField] private Button torsoBtn;
    [SerializeField] private Button hipBtn;
    [SerializeField] private Button backButton;
    [SerializeField] private Button ColorBtn;

    [SerializeField] private Sprite[] fullBody;
    [SerializeField] private Sprite[] head;
    [SerializeField] private Sprite[] torso;
    [SerializeField] private Sprite[] hip;

    [SerializeField] private Image headImage;
    [SerializeField] private Image torsoImage;
    [SerializeField] private Image hipImage;

    [SerializeField] private GameObject HeadButtons;
    [SerializeField] private GameObject TorsoButtons;
    [SerializeField] private GameObject HipButtons;

    private bool isZoomed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        headBtn.onClick.AddListener(ZoomHead);
        torsoBtn.onClick.AddListener(ZoomTorso);
        hipBtn.onClick.AddListener(ZoomHip);
        backButton.onClick.AddListener(ZoomOut);
        ColorBtn.onClick.AddListener(ZoomOut);
    }
    public void UpdateAllSprites()
    {
        SetSpritesByGender(headImage, head);
        SetSpritesByGender(torsoImage, torso);
        SetSpritesByGender(hipImage, hip);
    }


    public void ZoomOut()
    {
        Camera.transform.position = new Vector3(75.815f, 6.369f, -56.687f);
        Camera.fieldOfView = 60;

        isZoomed = false;
        ZoomedAnimation();

        HeadButtons.SetActive(false);
        TorsoButtons.SetActive(false);
        HipButtons.SetActive(false);

        SetSpritesByGender(headImage, head);
        SetSpritesByGender(torsoImage, torso);
        SetSpritesByGender(hipImage, hip);

        UpdateButtonListeners(ZoomHead, ZoomTorso, ZoomHip);
    }

    private void ZoomHead()
    {
        Camera.transform.position = new Vector3(75.088f, 6.653f, -56.734f);
        Camera.fieldOfView = 32;

        isZoomed = true;
        ZoomedAnimation();

        HeadButtons.SetActive(true);
        TorsoButtons.SetActive(false);
        HipButtons.SetActive(false);

        SetSpritesByGender(headImage, fullBody);
        ClearOtherImages(headImage);

        UpdateButtonListeners(ZoomOut, ZoomTorso, ZoomHip);
    }

    private void ZoomTorso()
    {
        Camera.transform.position = new Vector3(75.088f, 6.22f, -56.734f);
        Camera.fieldOfView = 46;

        isZoomed = true;
        ZoomedAnimation();

        TorsoButtons.SetActive(true);
        HeadButtons.SetActive(false);
        HipButtons.SetActive(false);

        SetSpritesByGender(torsoImage, fullBody);
        ClearOtherImages(torsoImage);

        UpdateButtonListeners(ZoomHead, ZoomOut, ZoomHip);
    }

    private void ZoomHip()
    {
        Camera.transform.position = new Vector3(75.088f, 5.56f, -56.734f);
        Camera.fieldOfView = 46;

        isZoomed = true;
        ZoomedAnimation();

        HipButtons.SetActive(true);
        HeadButtons.SetActive(false);
        TorsoButtons.SetActive(false);

        SetSpritesByGender(hipImage, fullBody);
        ClearOtherImages(hipImage);

        UpdateButtonListeners(ZoomHead, ZoomTorso, ZoomOut);
    }

    private void SetSpritesByGender(Image targetImage, Sprite[] sprites)
    {
        if (gender.sprite.name.Contains("Female"))
        {
            targetImage.sprite = sprites[0];
        }
        else
        {
            targetImage.sprite = sprites[1];
        }
    }

    private void ClearOtherImages(Image activeImage)
    {
        if (headImage != activeImage) SetSpritesByGender(headImage, head);
        if (torsoImage != activeImage) SetSpritesByGender(torsoImage, torso);
        if (hipImage != activeImage) SetSpritesByGender(hipImage, hip);
    }

    private void UpdateButtonListeners(UnityEngine.Events.UnityAction headAction,
                                        UnityEngine.Events.UnityAction torsoAction,
                                        UnityEngine.Events.UnityAction hipAction)
    {
        headBtn.onClick.RemoveAllListeners();
        headBtn.onClick.AddListener(headAction);

        torsoBtn.onClick.RemoveAllListeners();
        torsoBtn.onClick.AddListener(torsoAction);

        hipBtn.onClick.RemoveAllListeners();
        hipBtn.onClick.AddListener(hipAction);
    }

    private void ZoomedAnimation()
    {
        if (isZoomed)
        {
            Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/CharacterZoomed") as RuntimeAnimatorController;
        }
        else if (!isZoomed)
        {
            Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/CharacterCustomize") as RuntimeAnimatorController;
        }
    }
}
