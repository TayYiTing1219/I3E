using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    bool isOpen = false;
    float initialRotationY; // Store only the initial closed rotation

    void Start()
    {
        initialRotationY = transform.eulerAngles.y;
    }

    public void Interact()
    {
        // Toggle state first
        isOpen = !isOpen;
        
        // Apply rotation based on current state (works repeatedly)
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y = isOpen ? initialRotationY + 90f : initialRotationY;
        transform.eulerAngles = newRotation;
    }
}