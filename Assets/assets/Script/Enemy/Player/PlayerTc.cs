using UnityEngine;
using System.Collections;
public class PlayerTc : BaseTc
{

    public static int playerTc = 10;
	public int maxPlayerTc;
    public static float TcRate = 2f;

    public override void TakeTc(int maxTcZone )
    {
        StartCoroutine(PereodicPlusPlayerTC(TcRate, maxTcZone));
    }

    private IEnumerator PereodicPlusPlayerTC(float TcRate, int maxTcZone) 
    {
        while (playerTc < maxTcZone)
        {
            yield return new WaitForSeconds(TcRate);
            playerTc++;
            Debug.Log(playerTc);
        }
        
    }
}

