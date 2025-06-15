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
                // Apply damage - this will now trigger respawn if health reaches 0
                player.ModifyHealth(-damageAmount);
                
                // Push player away
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                other.GetComponent<Rigidbody>().AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
}