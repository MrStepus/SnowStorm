using UnityEngine;

public class PlayerStats : MonoBehaviour, IBaseEnemyInfo
{
    public int maxHp;
    public static int hp;

    private void Start()
    {
        hp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }

}
