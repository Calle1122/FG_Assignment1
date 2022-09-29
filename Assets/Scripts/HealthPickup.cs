
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float health;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerManager>().Heal(health);
            
            Destroy(this.gameObject);
        }
    }
    
    private void Update()
    {
        transform.Rotate(Vector3.up, 100f * Time.deltaTime);
    }
}
