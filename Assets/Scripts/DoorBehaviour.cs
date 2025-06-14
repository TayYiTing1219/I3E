using System.Numerics;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    bool isOpen = false;
    bool canInteract = false;
    float closedRotationY;
    float openRotationY;
    CoinBehaviour currentCoin;
    DoorBehaviour currentDoor;

    void Start()
    {
        closedRotationY = transform.eulerAngles.y;
        openRotationY = closedRotationY + 90f;
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log(other.gameObject.name);
    //     if (other.CompareTag("Collectible"))
    //     {
    //         canInteract = true;
    //         currentCoin = other.GetComponent<CoinBehaviour>();
    //     }
    //     else if (other.CompareTag("Door"))
    //     {
    //         canInteract = true;
    //         currentDoor = other.GetComponent<DoorBehaviour>();
    //     }
    // }
    
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

    // void OnInteract()
    // {
    //     if (canInteract)
    //     {
    //         if (currentCoin != null)
    //         {
    //             Debug.Log("Interacting with coin");
    //             currentCoin.Collect(this);
    //         }
    //         else if (currentDoor != null)
    //         {
    //             Debug.Log("Interacting with door");
    //             currentDoor.Interact(); 
    //         }
    //     }
    // }
}
