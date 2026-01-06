using UnityEngine;

public class TestAtackZone : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<BaseEnemyInfo>();

        if (health != null)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
               health.TakeDamage(damage);
                Debug.Log(PlayerStats.hp);
            }
        }
    }
}
