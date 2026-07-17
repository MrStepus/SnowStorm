using UnityEngine;

namespace Core.Composition
{
    public abstract class Component { }

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

    public class ObjectGame : Component
    {
        public ObjectGame(GameObject go)
        {
            _gameObject = go;
        }

        public GameObject _gameObject;
    } 
}
