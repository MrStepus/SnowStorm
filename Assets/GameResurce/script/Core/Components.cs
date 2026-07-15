using UnityEngine;

namespace Core.Composition
{
    public class Component { }

    public class HealAbility : Component
    {
        public HealAbility(int heal, int maxHeal)
        {
            this.hp = heal;
            this.maxHp = maxHeal;
        }
        
        public int hp;
        public int maxHp;
        
    }
}
