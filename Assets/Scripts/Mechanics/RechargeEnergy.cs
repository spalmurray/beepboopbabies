using System;
using UnityEngine;

[RequireComponent(typeof(StationInteractable))]
public class RechargeEnergy : MonoBehaviour
{
    // how long it will take to recharge to full
    public float incrementAmount = 5f;
    // Start is called before the first frame update
    private StationInteractable station;

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
            station.baby.uiController.SetAlwaysActive(energy: true);
        }
        else
        {
            station.baby.uiController.SetAlwaysActive(energy: false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (station.baby)
        {
            station.baby.IncreaseEnergy(incrementAmount * Time.deltaTime);
        }
    }
}