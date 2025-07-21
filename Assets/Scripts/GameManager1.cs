using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 instance;

    int currentScore = 0;

    [SerializeField]
    TextMeshProUGUI scoreText;

    void Awake()
    {
        // This is a LAZY singleton
        // Check if there is already an instance of GameManager1
        if (instance != null && instance != this)
        {
            // If it is not, destroy this object
            Destroy(gameObject);
        }
        else
        {
            // If there is no instance, set this object as the instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void ModifyScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Score: " + currentScore);
        scoreText.text = "Score: " + currentScore; // Update the score text
    }
    public void TestFunction()
    {
        Debug.Log("Test function called from GameManager");
    }

}

