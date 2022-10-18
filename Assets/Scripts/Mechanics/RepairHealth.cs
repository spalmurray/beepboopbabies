using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StationInteractable))]
public class RepairHealth : MonoBehaviour
{
    private StationInteractable station;
    
    public float healthIncreasePerSecond = 5f;

    private void Start()
    {
        station = GetComponent<StationInteractable>();
    }

    private void Update()
    {
        if (station.baby)
        {
            station.baby.IncreaseHealth(healthIncreasePerSecond * Time.deltaTime);
        }
    }
}
