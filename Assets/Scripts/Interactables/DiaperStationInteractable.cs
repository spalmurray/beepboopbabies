using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaperStationInteractable : StationInteractable
{
    public float diaperIncrement = 8f;

    private bool isChangingDiaper;
    
    public override void FixStationObject(bool isFixing)
    {
        isChangingDiaper = isFixing;
        GetComponent<BabyStationAudioPlayer>().HandleBabyPlaced(isFixing);
    }

    private void Update()
    {
        if (isChangingDiaper)
        {
            Baby.IncreaseDiaper(diaperIncrement * Time.deltaTime);
        }
    }
}
