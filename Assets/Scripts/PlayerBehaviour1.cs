using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerBehaviour1 : MonoBehaviour
{

    int maxHealth = 100;
    int currentHealth = 100;

    int score = 0;

    bool canInteract = false;
    CoinBehaviour1 currentCoin;
    // DoorBehaviour currentDoor;

    [SerializeField]
    GameObject projectfile;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float fireStrenth = 0f;

    [SerializeField]
    float interactionDistance = 5f;

    [SerializeField]
    TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "Score: " + score;
        GameManager1.instance.TestFunction();
    }

    void Update()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * interactionDistance, Color.green);

        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo))
        {
            if (currentCoin != null)
            {
                currentCoin.Unhighlight(); // Unhighlight the coin if raycast no longer detects it
            }
            // Debug.Log("Raycast hit: " + hitInfo.collider.gameObject.name);
            if (hitInfo.collider.CompareTag("Collectable"))
            {
                // Set the canInteract flag to true
                // Get the CoinBehaviour component from the detected object
                canInteract = true;
                currentCoin = hitInfo.collider.gameObject.GetComponent<CoinBehaviour1>();
                currentCoin.Highlight(); // Highlight the coin when detected by raycast
            }
        }
        else if (currentCoin != null)
        {
            // If the raycast does not hit a collectable, unhighlight the current coin
            currentCoin.Unhighlight();
            currentCoin = null; // Clear the current coin reference
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("HealingArea"))
        {
            if (currentHealth < maxHealth)
            {
                ++currentHealth;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                Debug.Log("Healing: " + currentHealth);
            }
        }
    }

    void OnInteract()
    {
        if (canInteract)
        {
            if (currentCoin != null)
            {
                Debug.Log("Interacting with coin");
                currentCoin.Collect(this);
            }
            // else if (currentDoor != null)
            // {
            //     Debug.Log("Interacting with door");
            //     currentDoor.Interact();
            // }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Collectable"))
        {
            canInteract = true;
            currentCoin = other.GetComponent<CoinBehaviour1>();
            currentCoin.Highlight(); // Highlight the coin when player enters trigger
        }
        // else if (other.CompareTag("Door"))
        // {
        //     canInteract = true;
        //     currentDoor = other.GetComponent<DoorBehaviour>();
        // }
    }

    public void ModifyScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
        scoreText.text = "Score: " + score; // Update the score text
    }

    void OnTriggerExit(Collider other)
    {
        if (currentCoin != null)
        {
            if (other.gameObject == currentCoin.gameObject)
            {
                canInteract = false;
                currentCoin.Unhighlight(); // Unhighlight the coin when player exits trigger
                currentCoin = null;
            }
        }
    }

    void OnFire()
    {
        GameObject newProjectile = Instantiate(projectfile, spawnPoint.position, spawnPoint.rotation);
        Vector3 fireForce = spawnPoint.forward * fireStrenth;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
    }
}
