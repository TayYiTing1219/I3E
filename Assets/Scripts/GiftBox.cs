using UnityEngine;

public class GiftBox : MonoBehaviour
{
    // This script handles the behavior of a gift box that can be destroyed by bullets
    // and spawns a coin when destroyed.
    [SerializeField]
    GameObject coinPrefab; // Prefab for the coin to be spawned when the gift box is destroyed

    [SerializeField]
    int numberOfCoins = 3; // Number of coins to spawn when the gift box is destroyed

    [SerializeField]
    private Vector3 coinRotation = new Vector3(90, 0, 0); // Adjust as needed for vertical orientation

    
    // This method is called when the gift box collides with another object
    // It checks if the object is a bullet and destroys the gift box if it is.
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("GiftBox collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Gift box hit by bullet!");
            // Destroy the gift box
            Destroy(gameObject);

            if (coinPrefab != null)
            {
                for (int i = 0; i < numberOfCoins; i++)
                {
                    Vector2 randomCircle = Random.insideUnitCircle * 0.5f; // Random point within a circle of radius 0.5
                    Vector3 coinSpawnPosition = transform.position + new Vector3(randomCircle.x, 0.5f, randomCircle.y);
                    Quaternion rotation = Quaternion.Euler(coinRotation);
                    Instantiate(coinPrefab, coinSpawnPosition, rotation);
                }
                Debug.Log($"{numberOfCoins} coin(s) spawned!");
            }
        }
    }
}
