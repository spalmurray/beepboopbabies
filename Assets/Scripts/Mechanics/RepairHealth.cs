using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StationInteractable))]
public class RepairHealth : MonoBehaviour
{
    private StationInteractable station;
    
    public float healthIncreasePerSecond = 5f;

    private void Awake()
    {
        station = GetComponent<StationInteractable>();
    }
    
    private void OnEnable()
    {
        station.HandlePlaceEvent += onPlaceEvent;
    }

    private void OnDisable()
    {
        station.HandlePlaceEvent -= onPlaceEvent;
    }

    private void onPlaceEvent(bool placeInStation)
    {
        if (placeInStation)
        {
            station.baby.uiController.SetAlwaysActive(health: true);
        }
        else
        {
            station.baby.uiController.SetAlwaysActive(health: false);
        }
    }

    private void Update()
    {
        if (station.baby)
        {
            station.baby.IncreaseHealth(healthIncreasePerSecond * Time.deltaTime);
        }
    }
}
