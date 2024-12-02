using UnityEngine;
using UnityEngine.UI;  // For accessing UI components
using TMPro;          // For TextMeshPro

public class InteractWithObject : MonoBehaviour
{
    public float interactDistance = 5f;         // Distance within which you can interact with the object
    public LayerMask interactableLayer;         // LayerMask to specify which objects are interactable
    public GameObject interactionPrompt;        // Reference to the interaction prompt
    private Camera playerCamera;                // Reference to the player's camera
    private bool isLookingAtInteractable = false;

    void Start()
    {
        // Assign the main camera
        playerCamera = Camera.main;

        // Ensure the prompt is hidden initially
        interactionPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        // Raycast forward from the camera
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            // Check if the object we are looking at is interactable
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                // Display the prompt and check for the "E" key press
                if (!isLookingAtInteractable)
                {
                    ShowPrompt(true);
                }

                //Change the text in the interactText text object if the object is the shop table
                if (hit.collider.gameObject.name == "SM_Prop_StoneTable_01")
                {
                    interactable.interactText.text = "to buy new items";
                }
                //Change the text in the interactText text object if the object is the door
                else if (hit.collider.gameObject.name == "SM_Env_Door_01")
                {
                    interactable.interactText.text = "to leave the shop";
                }




                // Check if the player presses the "E" key
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
                return;
            }
        }

        // Hide the prompt if we are not looking at any interactable object
        if (isLookingAtInteractable)
        {
            ShowPrompt(false);
        }
    }

    void ShowPrompt(bool show)
    {
        interactionPrompt.gameObject.SetActive(show);
        isLookingAtInteractable = show;
    }
}
