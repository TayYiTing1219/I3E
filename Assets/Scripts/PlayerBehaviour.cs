using UnityEngine;
using System.Collections; // Required for IEnumerator
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Instance { get; private set; }

    int health = 100;
    int max_health = 100;
    int score = 0;
    bool canInteract = false;
    DoorBehaviour currentDoor;
    CoinBehaviour currentCoin;
    ButtonController currentButton;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float fireStrength = 0f;
    [SerializeField] float interactionDistance = 5f;

    [Header("Respawn Settings")]
    [SerializeField] private Transform respawnPoint; // Assign in inspector
    [SerializeField] private float respawnDelay = 2f;
    private bool isRespawning = false;

    private IEnumerator Respawn()
    {
        if (isRespawning || respawnPoint == null) yield break;
        
        isRespawning = true;
        Debug.Log("Respawning...");
        
        // Optionally, disable player controls here

        yield return new WaitForSeconds(respawnDelay);

        // Move player to respawn point and restore health
        transform.position = respawnPoint.position;
        health = max_health;
        Debug.Log("Player respawned!");

        // Optionally, re-enable player controls here

        isRespawning = false;
    }

    public void ModifyHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, max_health);
        Debug.Log($"Health: {health}/{max_health}");
        
        if (health <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    public void ModifyScore(int amount) => score += amount;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnFire()
    {
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * fireStrength);
    }

    void OnInteract()
    {
        if (!canInteract) return;

        if (currentCoin != null)
        {
            currentCoin.Collect(this);
            currentCoin = null;
        }
        else if (currentButton != null)
        {
            currentButton.ToggleFlame();
        }
        else if (currentDoor != null)
        {
            currentDoor.Interact();
        }
    }

    void Update()
    {
        HandleInteractions();
    }

    void HandleInteractions()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, interactionDistance))
        {
            canInteract = false;
            
            // Reset previous interactions
            if (currentCoin != null) currentCoin.Unhighlight();
            if (currentButton != null) currentButton.Unhighlight();

            // Check for interactables
            if (hitInfo.collider.CompareTag("Collectable"))
            {
                currentCoin = hitInfo.collider.GetComponent<CoinBehaviour>();
                currentCoin.Highlight();
                canInteract = true;
            }
            else if (hitInfo.collider.CompareTag("Button"))
            {
                currentButton = hitInfo.collider.GetComponent<ButtonController>();
                currentButton.Highlight();
                canInteract = true;
            }
        }
        else
        {
            ClearInteractions();
        }

        if (canInteract)
        {
            if (currentCoin != null)
            {
                currentCoin.Collect(this);
                currentCoin = null;
            }
            else if (currentButton != null)
            {
                currentButton.ToggleFlame();
            }
        }
    }

    void ClearInteractions()
    {
        if (currentCoin != null) currentCoin.Unhighlight();
        if (currentButton != null) currentButton.Unhighlight();
        
        currentCoin = null;
        currentButton = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door")) currentDoor = other.GetComponent<DoorBehaviour>();
        else if (other.CompareTag("Collectable")) currentCoin = other.GetComponent<CoinBehaviour>();
        else if (other.CompareTag("Button")) currentButton = other.GetComponent<ButtonController>();
        
        canInteract = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door")) currentDoor = null;
        else if (other.CompareTag("Collectable")) currentCoin = null;
        else if (other.CompareTag("Button")) currentButton = null;
        
        canInteract = false;
    }
}