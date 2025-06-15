using UnityEngine;


[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
public class UmbrellaCollect : MonoBehaviour
{
    // Original highlight system
    private MeshRenderer meshRenderer;
    [SerializeField] private Material highlightMaterial;
    private Material originalMaterial;

    // object collection
    private bool isCollected = false;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private float soundVolume = 150f;

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

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, soundVolume);
        }
        else
        {
            Debug.LogWarning("Collect sound not assigned!");
        }

        // Notify the player that an umbrella was collected
        player.OnUmbrellaCollected();

        // Disable before destruction to prevent double-collection
        meshRenderer.enabled = false;
        Destroy(gameObject, collectSound != null ? collectSound.length : 0.1f); // Small delay for effects to play
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;
    }

}