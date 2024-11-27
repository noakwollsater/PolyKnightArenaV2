using UnityEngine;

public class RotateCharacter : MonoBehaviour
{
    public float rotationSpeed = 100f; // Adjust the speed of rotation

    private bool isDragging = false;
    private float lastMouseX;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
            lastMouseX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float deltaX = Input.mousePosition.x - lastMouseX;
            lastMouseX = Input.mousePosition.x;
            float rotationAmount = deltaX * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, -rotationAmount); // Rotate the character around the Y axis
        }
    }
}
