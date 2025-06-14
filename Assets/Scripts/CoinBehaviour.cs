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

    [Header("Sound Effects")]
    // [SerializeField] private ParticleSystem collectParticles;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private float soundVolume = 1f;

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

        // // Visual/Audio effects
        // if (collectParticles != null)
        // {
        //     ParticleSystem particles = Instantiate(collectParticles, transform.position, Quaternion.identity);
        //     particles.Play();
        //     Destroy(particles.gameObject, particles.main.duration);
        // }

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, soundVolume);
        }
        else
        {
            Debug.LogWarning("Collect sound not assigned!");
        }

        // Disable before destruction to prevent double-collection
        meshRenderer.enabled = false;
        Destroy(gameObject, collectSound != null ? collectSound.length : 0.1f); // Small delay for effects to play
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