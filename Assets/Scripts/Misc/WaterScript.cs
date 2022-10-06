
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(500);
        }
    }
}
