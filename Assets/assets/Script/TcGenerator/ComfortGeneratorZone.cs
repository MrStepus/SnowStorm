using UnityEngine;

public class ComfortGeneratorZone : MonoBehaviour
{
    public float newTcRate;
    public float oldTcRate;
    public int newMaxTcZone;
    public int oldMaxTcZone;
    public GameObject homeGeneratorZone;

    private void OnTriggerEnter(Collider other)
    {
        var mainZone = homeGeneratorZone.GetComponent<TestGeneratorZone>();
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            if (mainZone != null) 
            {
                oldMaxTcZone = mainZone.maxTcZone;
                mainZone.maxTcZone = newMaxTcZone;
                oldTcRate = mainZone.tcRate;
                mainZone.tcRate = newTcRate;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        var mainZone = homeGeneratorZone.GetComponent<TestGeneratorZone>();
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            if (mainZone != null) 
            {
                mainZone.maxTcZone = oldMaxTcZone;
                mainZone.tcRate = newTcRate;
            }
        }

    }
}
