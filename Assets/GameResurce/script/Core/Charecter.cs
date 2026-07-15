using System.Collections.Generic;
using UnityEngine;

namespace Core.Composition
{
    public class Character :  MonoBehaviour
    {
        
        public List<Component> Components = new List<Component>();
        
        public void AddComponent(params Component[] abilities)
        {
            foreach (var a in abilities)
            {
                Components.Add(a);
            }
        }
    
        public T GetComponent<T>() where T : Component
        {
            foreach (var a in Components)
            {
                if (a is T targetAbility)
                {
                    return targetAbility;
                }
            }
            return null;
        }
        
    }
}