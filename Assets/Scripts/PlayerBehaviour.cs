using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBehaviour : MonoBehaviour
{
    int health = 100;
    int max_health = 100;
    int score = 0; // Player's score

    bool canInteract = false; // Flag to check if the player can interact with an object
    DoorBehaviour currentDoor; // Declare a variable to hold the current door object
    CoinBehaviour currentCoin; // Declare a variable to hold the current coin object
    CardboardCollectable currentCardboard; // Declare a variable to hold the current collectable object
    UmbrellaCollect currentUmbrella; // Declare a variable to hold the current umbrella object


    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float fireStrength = 0f; // Strength of the fire force applied to the projectile

    [SerializeField]
    float interactionDistance = 5f; // Distance within which the player can interact with objects
    
    private Vector3 startPosition; // Store the starting position

    void Start()
    {
        startPosition = transform.position; // Initialize the starting position
    }

    public void ModifyHealth(int amount)
    {
        health += amount;
        
        // Check if health dropped below zero
        if (health <= 0)
        {
            health = 0;
            Respawn();
        }
        else if (health > max_health)
        {
            health = max_health;
        }
    }
    void Respawn()
    {
        // Reset position to start
        transform.position = startPosition;
        
        // Reset health
        health = max_health;
        
        // Optional: Reset velocity if player has a Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
        Debug.Log("Player respawned!");
    }

    public void ModifyScore(int amount)
    {
        score += amount; // Modify the score by the specified amount
        Debug.Log("Score: " + score); // Log the current score
    }

    void OnFire()
    {
        // instantiate a new projectile at the spawn point's position and rotation
        // store the spawned porojectile to the 'newProjectile' variable
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);

        // create a new vector3 variable 'Fireforce'
        // set it to the forward direction of the spawn point multiplied by the fire strength
        // this will determine the direction and speed of the projectile
        Vector3 fireForce = spawnPoint.forward * fireStrength;

        // get the Rigidbody component of the new projectile
        // add a force to the projectile defined by the fireForce variable
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
    }


    void Update()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, interactionDistance))
        {
            Debug.Log("Raycast hit: " + hitInfo.collider.gameObject.name);
            canInteract = false;
            if (hitInfo.collider.CompareTag("Door"))
            {
                currentDoor = hitInfo.collider.GetComponent<DoorBehaviour>();
                canInteract = true;
                Debug.Log("Door detected");
            }

            else if (hitInfo.collider.gameObject.CompareTag("Collectable"))
            {
                if (currentCoin != null)
                {
                    currentCoin.Unhighlight(); // Unhighlight the previous coin
                }
                // set the canInteract flag to true
                // and assign the currentCoin variable to the CoinBehaviour component of the hit object
                canInteract = true;
                currentCoin = hitInfo.collider.GetComponent<CoinBehaviour>();
                currentCoin.Highlight(); // Highlight the coin when in range
            }

            else if (hitInfo.collider.gameObject.CompareTag("Cardboard"))
            {
                if (currentCardboard != null)
                {
                    currentCardboard.Unhighlight(); // Unhighlight the previous collectable
                }
                // set the canInteract flag to true
                // and assign the currentCardboard variable to the CardboardCollectable component of the hit object
                canInteract = true;
                currentCardboard = hitInfo.collider.GetComponent<CardboardCollectable>();
                currentCardboard.Highlight(); // Highlight the collectable when in range
            }

            else if (hitInfo.collider.gameObject.CompareTag("Umbrella"))
            {
                if (currentUmbrella != null)
                {
                    currentUmbrella.Unhighlight(); // Unhighlight the previous umbrella
                }
                // set the canInteract flag to true
                // and assign the currentUmbrella variable to the UmbrellaCollect component of the hit object
                canInteract = true;
                currentUmbrella = hitInfo.collider.GetComponent<UmbrellaCollect>();
                currentUmbrella.Highlight(); // Highlight the umbrella when in range
            }

        }
        else if (currentCoin != null)
        {
            currentCoin.Unhighlight(); // Unhighlight the coin if not in range
            currentCoin = null; // Reset currentCoin if raycast does not hit a collectable
        }
        else if (currentCardboard != null)
        {
            currentCardboard.Unhighlight(); // Unhighlight the collectable if not in range
            currentCardboard = null; // Reset currentCardboard if raycast does not hit a collectable
        }
        else if (currentUmbrella != null)
        {
            currentUmbrella.Unhighlight(); // Unhighlight the umbrella if not in range
            currentUmbrella = null; // Reset currentUmbrella if raycast does not hit a collectable
        }

        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (currentDoor != null)
            {
                Debug.Log("Interacting with door");
                currentDoor.Interact();
            }
            else if (currentCoin != null)
            {
                Debug.Log("Interacting with coin");
                currentCoin.Collect(this);
                currentCoin = null;
                canInteract = false;
            }
            else if (currentCardboard != null)
            {
                Debug.Log("Interacting with cardboard");
                currentCardboard.Collect(this);
                currentCardboard = null;
                canInteract = false;
            }
            else if (currentUmbrella != null)
            {
                Debug.Log("Interacting with umbrella");
                currentUmbrella.Collect(this);
                currentUmbrella = null;
                canInteract = false;
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            currentDoor = other.GetComponent<DoorBehaviour>();
            canInteract = true;
        }
        else if (other.CompareTag("Collectable"))
        {
            currentCoin = other.GetComponent<CoinBehaviour>();
            canInteract = true;
            Debug.Log("Player can collect: " + other.gameObject.name);
        }
        else if (other.CompareTag("Cardboard"))
        {
            currentCardboard = other.GetComponent<CardboardCollectable>();
            canInteract = true;
            Debug.Log("Player can collect: " + other.gameObject.name);
        }
        else if (other.CompareTag("Umbrella"))
        {
            currentUmbrella = other.GetComponent<UmbrellaCollect>();
            canInteract = true;
            Debug.Log("Player can collect: " + other.gameObject.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            currentDoor = null;
            canInteract = false;
        }
        else if (other.CompareTag("Collectable"))
        {
            currentCoin = null;
            canInteract = false;
        }
        else if (other.CompareTag("Cardboard"))
        {
            currentCardboard = null;
            canInteract = false;
        }
        else if (other.CompareTag("Umbrella"))
        {
            currentUmbrella = null;
            canInteract = false;
        }
    }
}
