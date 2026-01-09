using System.Buffers.Text;
using System.Diagnostics;
using UnityEngine;

public class TestGeneratorZone : MonoBehaviour
{
    public int maxTcZone;
    public float tcRate;
    private void OnTriggerEnter(Collider other)
    {
        var baseTC = other.GetComponent<BaseTc>();

        if (baseTC != null)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                baseTC.TakeTc(maxTcZone, tcRate);
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
                baseTC.TakeStopCorutinTc(maxTcZone, tcRate);
            }
        }
    }
}
