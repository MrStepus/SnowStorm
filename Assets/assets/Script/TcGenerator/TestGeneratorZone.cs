using UnityEngine;

public class TestGeneratorZone : MonoBehaviour
{
    public int maxTcZone;
    private void OnTriggerEnter(Collider other)
    {
        var playerTC = other.GetComponent<BaseTc>();

        if (playerTC != null)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                playerTC.TakeTc(maxTcZone);
            }
        }
    }
}
