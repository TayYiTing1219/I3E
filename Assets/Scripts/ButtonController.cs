using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material pressedMaterial;
    private Material originalMaterial;
    private bool isPressed = false;


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
        if (meshRenderer != null && !isPressed)
            meshRenderer.material = highlightMaterial;
    }

    public void Unhighlight()
    {
        if (meshRenderer != null && !isPressed)
            meshRenderer.material = originalMaterial;
    }

    public void Interact()
    {
        if (isPressed) return;

        isPressed = true;
        meshRenderer.material = pressedMaterial;

        // Remove all active flames
        RemoveAllFlames();

        Debug.Log("Button pressed - flames should be removed");
    }
    private void RemoveAllFlames()
    {
        try
        {
            Flame[] allFlames = FindObjectsByType<Flame>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            foreach (Flame flame in allFlames)
            {
                if (flame != null)
                {
                    flame.DestroyFlame();
                    Debug.Log("Destroyed flame: " + flame.gameObject.name);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error removing flames: " + ex.Message);
        }
    }
}