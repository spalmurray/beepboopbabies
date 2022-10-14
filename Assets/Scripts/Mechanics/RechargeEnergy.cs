using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeEnergy : MonoBehaviour
{
    // how long it will take to recharge to full
    public float rechargeTime = 5f;
    // Start is called before the first frame update
    private StationInteractable station;

    void Start()
    {
        station = GetComponent<StationInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        var state = station.baby;
        if (state != null)
        {
            state.IncreaseEnergy(rechargeTime);
        }
    }
}
