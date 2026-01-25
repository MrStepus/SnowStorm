using UnityEngine;
using System.Collections;
public class PlayerTc : BaseTc
{

    private int zoneCounter = 0;
    private int oldIndex;
    
    public bool payerNearGenerator = false;
    public bool chillsActive = true;

    private Coroutine activeChillsRoutine;
    private Coroutine corutinChillsActive;


    private void Start()
    {
        corutinChillsActive = StartCoroutine(Chills());
    }

    public override void TakeTc(int index)
    {
        switch (zoneCounter)
        {
            case 0:
                zoneCounter = 1;
                oldIndex = index;
                RestartCorutin(index);
                chillsActive = false;
                StopCoroutine(corutinChillsActive);
                break;

            case 1:
                zoneCounter = 2;
                RestartCorutin(index);
                break;
        }
    }

    public override void TakeStopCorutinTc(int index)
    {
        switch (zoneCounter)
        {
            case 1:
                zoneCounter = 0;
                RestartCorutin(index);
                chillsActive = true;
                corutinChillsActive = StartCoroutine(Chills());
                
                break;

            case 2:
                zoneCounter = 3;
                index = oldIndex;
                RestartCorutin(index);
                break;

            case 3:
                zoneCounter = 0;
                index = 0;
                RestartCorutin(index);
                chillsActive = true;
                corutinChillsActive = StartCoroutine(Chills());
                break;
        }
    }

    public IEnumerator PereodicPlusPlayerTC(int reductionOfChills, float tcRate, int minusСhills) 
    {
        while (PlayerInfo.playerСhills > reductionOfChills && payerNearGenerator == true)
        {
            yield return new WaitForSeconds(tcRate);
            PlayerInfo.playerСhills -= minusСhills;
            Debug.Log(PlayerInfo.playerСhills);
        }
        payerNearGenerator = false;
        activeChillsRoutine = null;

    }
    
    public IEnumerator Chills()
    {
        while (PlayerInfo.playerСhills < PlayerInfo.maxPlayerChills && chillsActive == true)
        {
            yield return new WaitForSeconds(20f);
            PlayerInfo.playerСhills++;
            Debug.Log(PlayerInfo.playerСhills);
        }
    }

    public void RestartCorutin(int index)
    {
        if (activeChillsRoutine != null) 
        {
            StopCoroutine(activeChillsRoutine);
            payerNearGenerator = false;
        }

        if (zoneCounter != 0)
        {
            payerNearGenerator = true;    
            var data = IndexListSetings.ZoneSeting[index];
            int reduction = (int)data[0];
            float rate = data[1];
            int minus = (int)data[2];
            activeChillsRoutine = StartCoroutine(PereodicPlusPlayerTC(reduction, rate, minus));
        }
    }
}

