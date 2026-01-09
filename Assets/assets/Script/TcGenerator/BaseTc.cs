using UnityEngine;

public abstract class BaseTc : MonoBehaviour
{
    public abstract void TakeTc(int maxTcZone, float tcRate);
    public abstract void TakeStopCorutinTc(int maxTcZone, float tcRate);
}
