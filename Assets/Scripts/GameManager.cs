using UnityEngine;
using TMPro; // Required for TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("UI Reference")]
    public GameObject collectablesPanel;
    public TMP_Text collectablesText;
    
    [Header("Game Settings")]
    public int totalCollectables = 20;
    private int collectedCount = 0;
    private Color defaultTextColor = Color.white;
    private Color completedTextColor = Color.green;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Auto-find TMP text if not assigned (optional)
        if (collectablesText == null)
        {
            collectablesText = GameObject.Find("CollectablesCounter").GetComponent<TMP_Text>();
            if (collectablesText == null)
            {
                Debug.LogError("Collectables TMP Text not found!");
            }
        }
        
        UpdateUI();
    }

    public void AddCollectable()
    {
        collectedCount++;
        UpdateUI();
        
        if (collectedCount >= totalCollectables)
        {
            Debug.Log("All collectables collected!");
        }
    }

    void UpdateUI()
    {
        if (collectablesText != null)
        {
            collectablesText.text = $"{collectedCount}/{totalCollectables}";
        }
        else
        {
            Debug.LogWarning("Collectables TMP Text not assigned!");
        }
    }
}