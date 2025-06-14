using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RainSoundController : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip rainSound;
    [Range(0f, 1f)] public float maxVolume = 0.7f;
    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxDistance = 20f;

    [Header("Playback Settings")]
    [SerializeField] [Range(0.5f, 3f)] private float playbackSpeed = 1.2f; // >1 = faster

    private AudioSource audioSource;
    private Transform playerTransform;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = playbackSpeed;
        audioSource.clip = rainSound;
        audioSource.spatialBlend = 1f; // Full 3D sound
        audioSource.loop = true;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;
        audioSource.Play();
    }

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform == null) return;
        
        // Optional: Adjust volume based on distance (alternative to built-in 3D sound)
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        float volume = Mathf.InverseLerp(maxDistance, minDistance, distance) * maxVolume;
        audioSource.volume = volume;
    }
}