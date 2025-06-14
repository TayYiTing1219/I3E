// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.SocialPlatforms.Impl;

// public class CoinBehaviour : MonoBehaviour
// {
//     MeshRenderer meshRenderer; // MeshRenderer to change the material of the coin
//     [SerializeField]
//     Material HighlightMaterial; // Material to highlight the coin
//     Material OriginalMaterial; // Original material of the coin

//     void Start()
//     {
//         // get the MeshRenderer component attached to this GameObject
//         // store it in the meshRenderer variable
//         meshRenderer = GetComponent<MeshRenderer>();

//         // store the original color of the coin
//         OriginalMaterial = meshRenderer.material;
//     }
//     public void Highlight()
//     {
//         meshRenderer.material = HighlightMaterial; // Change to highlight material
//     }

//     public void Unhighlight()
//     {
//         meshRenderer.material = OriginalMaterial; // Change back to the original material
//     }

//     public int coinValue = 100; // Value of the coin

//     public void Collect(PlayerBehaviour player)
//     {
//         player.ModifyScore(coinValue); // Call the method to modify the score
//         Destroy(gameObject); // Destroy the coin object
//     }
// }


using UnityEngine;

[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
public class CoinBehaviour : MonoBehaviour
{
    // Original highlight system
    private MeshRenderer meshRenderer;
    [SerializeField] private Material highlightMaterial;
    private Material originalMaterial;
    
    // Coin value and collection
    public int coinValue = 100;
    private bool isCollected = false;

    // Audio/Visual effects
    [Header("Effects")]
    [SerializeField] private ParticleSystem collectParticles;
    [SerializeField] private AudioClip collectSound;

    void Start()
    {
        // Original material setup
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;

        // Auto-configure collider
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    // Original highlight functions
    public void Highlight()
    {
        if (meshRenderer != null)
            meshRenderer.material = highlightMaterial;
    }

    public void Unhighlight()
    {
        if (meshRenderer != null)
            meshRenderer.material = originalMaterial;
    }

    // Enhanced collection system
    public void Collect(PlayerBehaviour player)
    {
        if (isCollected) return;
        isCollected = true;

        // Original score modification
        player.ModifyScore(coinValue);

        // New collectable tracking
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddCollectable();
        }
        else
        {
            Debug.LogWarning("GameManager instance not found!");
        }

        // Visual/Audio effects
        if (collectParticles != null)
        {
            ParticleSystem particles = Instantiate(collectParticles, transform.position, Quaternion.identity);
            particles.Play();
            Destroy(particles.gameObject, particles.main.duration);
        }

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        // Disable before destruction to prevent double-collection
        meshRenderer.enabled = false;
        Destroy(gameObject, 0.1f); // Small delay for effects to play
    }

    // Auto-collection when player touches
    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        if (other.TryGetComponent<PlayerBehaviour>(out var player))
        {
            Collect(player);
        }
    }
}