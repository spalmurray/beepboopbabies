using System;
using UnityEngine;

[RequireComponent(typeof(BottleStationInteractable))]
public class RefillBottle : MonoBehaviour
{
    // how long it will take to recharge to full
    public float incrementAmount = 5f;
    // Start is called before the first frame update
    private BottleStationInteractable station;

    private void Awake()
    {
        station = gameObject.GetComponent<BottleStationInteractable>();
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
            station.pickUpObject.uiController.EnableStatusBars();
        }
        else
        {
            station.pickUpObject.uiController.DisableStatusBars();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (station.pickUpObject)
        {
            station.pickUpObject.IncreaseAmount(incrementAmount * Time.deltaTime);
        }
    }
}