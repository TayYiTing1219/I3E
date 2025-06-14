using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    bool isOpen = false;
    float initialRotationY; // Store only the initial closed rotation

    [Header("Sound Effects")]
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        initialRotationY = transform.eulerAngles.y;
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f; // Make it 3D sound
        }
    }

    public void Interact()
    {
        // Toggle state first
        isOpen = !isOpen;

        // Apply rotation based on current state (works repeatedly)
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y = isOpen ? initialRotationY + 90f : initialRotationY;
        transform.eulerAngles = newRotation;
        
        if (audioSource != null)
        {
            if (isOpen && openSound != null)
            {
                audioSource.PlayOneShot(openSound);
            }
            else if (!isOpen && closeSound != null)
            {
                audioSource.PlayOneShot(closeSound);
            }
        }
    }
}