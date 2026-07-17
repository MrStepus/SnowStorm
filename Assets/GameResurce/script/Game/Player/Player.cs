using System.Collections.Generic;
using UnityEngine;
using Core.Composition;
using GameObject = UnityEngine.GameObject;

public class Player : Character
{
    
    public GameObject _gameObject;
    
    private void Awake()
    {
        AddComponent(new HealAbility(100, 100));
        AddComponent(new ObjectGame(_gameObject));
    }

    private void Start()
    {
        Debug.Log("heal = " + GetComponent<HealAbility>().maxHp);
        new DealDamageCommand(this, 20).Execute();
        Debug.Log("heal = " + GetComponent<HealAbility>().hp);
        Debug.Log(GetComponent<ObjectGame>()._gameObject.name);
    }
}
