using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    bool isOpen = false;
    float closedRotationY;
    float openRotationY;

    void Start()
    {
        closedRotationY = transform.eulerAngles.y;
        openRotationY = closedRotationY + 90f;
    }

    public void Interact()
    {
        UnityEngine.Vector3 doorRotation = transform.eulerAngles;

        if (isOpen)
        {
            // Close the door
            doorRotation.y = closedRotationY;
        }
        else
        {
            // Open the door
            doorRotation.y = openRotationY;
        }
        
        transform.eulerAngles = doorRotation;
        isOpen = !isOpen; // Toggle the state
    }
}
