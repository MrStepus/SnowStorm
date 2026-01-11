using System.Buffers.Text;
using System.Diagnostics;
using UnityEngine;

public class TestGeneratorZone : MonoBehaviour
{
    public int index; 
    private void OnTriggerEnter(Collider other)
    {
        var baseTC = other.GetComponent<BaseTc>();

        if (baseTC != null)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                baseTC.TakeTc(index);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var baseTC = other.GetComponent<BaseTc>();

        if (baseTC != null)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                baseTC.TakeStopCorutinTc(index);
            }
        }
    }
}
