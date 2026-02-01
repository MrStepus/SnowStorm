using UnityEngine;

public class TestAtackZone : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<IBaseEnemyInfo>(out var baseEnemyInfo))
        {
            baseEnemyInfo.TakeDamage(damage);
            Debug.Log(PlayerStats.hp);
        }
    }
}
