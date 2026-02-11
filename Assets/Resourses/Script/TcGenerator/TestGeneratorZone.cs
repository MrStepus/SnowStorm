using System.Buffers.Text;
using System.Diagnostics;
using UnityEngine;

public class TestGeneratorZone : MonoBehaviour
{
    public int index; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IBaseTc>(out var baseTC))
        { 
            baseTC.TakeTc(index);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.TryGetComponent<IBaseTc>(out var baseTC))
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                baseTC.TakeStopCorutinTc(index);
            }
        }
    }
}
