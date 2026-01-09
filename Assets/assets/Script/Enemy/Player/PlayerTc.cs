using UnityEngine;
using System.Collections;
public class PlayerTc : BaseTc
{

    public static int playerTc = 40;
	public int maxPlayerTc;
    public bool payerNearGenerator = false;

    public override void TakeTc(int maxTcZone, float tcRate )
    {
        payerNearGenerator = true;
        StartCoroutine(PereodicPlusPlayerTC(maxTcZone, tcRate));
    }

    public override void TakeStopCorutinTc(int maxTcZone, float tcRate)
    {
        payerNearGenerator = false;
        StopCoroutine(PereodicPlusPlayerTC(maxTcZone, tcRate));
    }

    public IEnumerator PereodicPlusPlayerTC(int maxTcZone, float tcRate) 
    {
        while (playerTc < maxTcZone && payerNearGenerator == true)
        {
            yield return new WaitForSeconds(tcRate);
            playerTc++;
            Debug.Log(playerTc);
        }
        
    }
}

