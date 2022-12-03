using System;
using UnityEngine;

[RequireComponent(typeof(StationInteractable))]
[RequireComponent(typeof(BabyStationAudioPlayer))]
public class RechargeEnergy : MonoBehaviour
{
    // how long it will take to recharge to full
    public float incrementAmount = 5f;
    // Start is called before the first frame update
    private StationInteractable station;

    private void Awake()
    {
        station = GetComponent<StationInteractable>();
        
        var audioPlayer = GetComponent<BabyStationAudioPlayer>();
        station.HandlePlaceEvent += audioPlayer.HandleBabyPlaced;
        audioPlayer.shouldStartAudio = baby => baby.currentEnergy < baby.energy - 1;
        audioPlayer.shouldEndAudio = baby => baby.currentEnergy >= baby.energy - 1;
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
            station.Baby.uiController.SetAlwaysActive(energy: true);
        }
        else
        {
            station.Baby.uiController.SetAlwaysActive(energy: false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (station.Baby)
        {
            station.Baby.IncreaseEnergy(incrementAmount * Time.deltaTime);
        }
    }
}