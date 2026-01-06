using UnityEngine;

public class PlayerStats : BaseEnemyInfo
{
    public int maxHp;
    public static int hp;

    private void Start()
    {
        hp = maxHp;
    }

    public override void TakeDamage(int damage)
    {
        hp -= damage;
    }

}
