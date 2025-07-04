/*
* Author: Tay Yi Ting
* Date: 2025-06-15
* Description: Hazard that applies damage and push effect to the player on collision.
*/

using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int damageAmount = 100;
    [SerializeField] private float pushForce = 10f;

    public int GetDamageAmount()
    {
        return damageAmount;
    }

    private void ApplyDamage(GameObject playerObject)
    {
        PlayerBehaviour player = playerObject.GetComponent<PlayerBehaviour>();
        if (player != null)
        {
            Debug.Log($"Applying {damageAmount} damage to player");
            player.ModifyHealth(-damageAmount);

            // Push effect
            Rigidbody rb = playerObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDirection = (playerObject.transform.position - transform.position).normalized;
                rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hazard collided with player!");
            ApplyDamage(collision.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hazard triggered by player!");
            ApplyDamage(other.gameObject);
        }
    }
}