using UnityEngine;
using UnityEngine.UI;

public class TransformToEquipment : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject EquipmentPanel;
    [SerializeField] private GameObject CharacterInfoPanel;
    [SerializeField] private Button EqupmentBtn;
    [SerializeField] private Button BackBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EqupmentBtn.onClick.AddListener(TransformCamPlayer);
        BackBtn.onClick.AddListener(GoBack);
    }

    void TransformCamPlayer()
    {
        Camera.transform.position = new Vector3(52.693f, 6.079f, -51.042f);
        Camera.transform.rotation = Quaternion.Euler(0, 423.379f, 0);

        Player.transform.position = new Vector3(54.744f, 5.060f, -50.529f);
        Player.transform.rotation = Quaternion.Euler(0, 223.176f, 0);

        EquipmentPanel.SetActive(true);
        CharacterInfoPanel.SetActive(false);
    }
    void GoBack()
    {
        Camera.transform.position = new Vector3(75.815f, 6.369f, -56.687f);
        Camera.transform.rotation = Quaternion.Euler(0, 235.07f, 0);

        Player.transform.position = new Vector3(74.214f, 5.060f, -57.1f);
        Player.transform.rotation = Quaternion.Euler(0, 74.712f, 0);

        EquipmentPanel.SetActive(false);
        CharacterInfoPanel.SetActive(true);
    }
}
