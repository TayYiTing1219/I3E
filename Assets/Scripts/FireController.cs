using UnityEngine;

public class FireController : MonoBehaviour
{
    [Header("Fire Settings")]
    public GameObject fireEffect; // Assign your fire particle system in Inspector
    public float extinguishRange = 3f; // How close player needs to be
    public KeyCode extinguishKey = KeyCode.E; // Key to press

    [Header("Object Settings")]
    public Color highlightColor = Color.green; // Color to identify interactable object
    private Material originalMaterial;
    private Renderer objectRenderer;
    private bool canExtinguish = false;

    void Start()
    {
        // Get the object's renderer and store original material
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }

        // Ensure fire starts active
        if (fireEffect != null)
        {
            fireEffect.SetActive(true);
        }
    }

    void Update()
    {
        // Check for E key press when in range
        if (canExtinguish && Input.GetKeyDown(extinguishKey))
        {
            ExtinguishFire();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canExtinguish = true;
            HighlightObject(true);
            Debug.Log("Press E to extinguish fire");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canExtinguish = false;
            HighlightObject(false);
        }
    }

    void HighlightObject(bool highlight)
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = highlight ? highlightColor : originalMaterial.color;
        }
    }

    void ExtinguishFire()
    {
        if (fireEffect != null)
        {
            fireEffect.SetActive(false);
            Debug.Log("Fire extinguished!");
            
            // Optional: Play extinguishing sound
            // AudioSource.PlayClipAtPoint(extinguishSound, transform.position);
        }
        
        // Disable further interactions
        canExtinguish = false;
        HighlightObject(false);
        
        // Optional: Disable the script after extinguishing
        // this.enabled = false;
    }
}