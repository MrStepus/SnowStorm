using System.Collections.Generic;
using UnityEngine;
using Core.Composition;

public class Player : Character
{
    private void Awake()
    {
        AddComponent(new HealAbility(100, 100));
    }

    private void Start()
    {
        new DealDamageCommand(this, 20).Execute();
        Debug.Log("heal = " + GetComponent<HealAbility>().hp);
    }
}
