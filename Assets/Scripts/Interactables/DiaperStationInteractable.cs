using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaperStationInteractable : StationInteractable
{
    // Start is called before the first frame update
    public float diaperIncrement = 3f;
    // Start is called before the first frame update
    public override void FixStationObject()
    {
        Baby.IncreaseDiaper(diaperIncrement);
    }
}
