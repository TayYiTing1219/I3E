using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Load the scene with index 1 when the player enters the trigger collision
            SceneManager.LoadScene(1);
        }
    }
}

