using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] Material pressedMaterial;
    [SerializeField] Material unpressedMaterial;
    
    [Header("Flame Settings")]
    [SerializeField] ParticleSystem flameParticles; // Assign existing flamethrower's ParticleSystem
    [SerializeField] Collider flameCollider; // Assign existing flamethrower's Collider
    
    private MeshRenderer buttonRenderer;
    private bool isFlameActive;

    void Awake()
    {
        buttonRenderer = GetComponent<MeshRenderer>();
        isFlameActive = flameParticles != null ? flameParticles.isPlaying : false;
        UpdateButtonAppearance();
    }

    public void ToggleFlame()
    {
        if (flameParticles == null) return;
        
        isFlameActive = !isFlameActive;
        
        // Toggle particles
        if (isFlameActive) flameParticles.Play();
        else flameParticles.Stop();
        
        // Toggle damage collider
        if (flameCollider != null) flameCollider.enabled = isFlameActive;
        
        UpdateButtonAppearance();
    }

    void UpdateButtonAppearance()
    {
        if (buttonRenderer != null)
        {
            buttonRenderer.material = isFlameActive ? pressedMaterial : unpressedMaterial;
        }
    }

    public void Highlight()
    {
        if (buttonRenderer != null) buttonRenderer.material.color = Color.cyan;
    }

    public void Unhighlight()
    {
        UpdateButtonAppearance();
    }
}