using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int damageAmount = 100;
    [SerializeField] private float pushForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                // Apply damage
                player.ModifyHealth(-damageAmount);
                
                // Push player away
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                other.GetComponent<Rigidbody>().AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
}