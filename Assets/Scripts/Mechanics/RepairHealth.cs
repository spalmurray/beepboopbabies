using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StationInteractable))]
[RequireComponent(typeof(BabyStationAudioPlayer))]
public class RepairHealth : MonoBehaviour
{
    private StationInteractable station;
    
    public float healthIncreasePerSecond = 5f;
    private static readonly int Repair = Animator.StringToHash("Repair");

    private void Awake()
    {
        station = GetComponent<StationInteractable>();
        
        var audioPlayer = GetComponent<BabyStationAudioPlayer>();
        audioPlayer.shouldStartAudio = baby => baby.currentHealth < baby.health - 1;
        audioPlayer.shouldEndAudio = baby => baby.currentHealth >= baby.health - 1;
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
            station.Baby.uiController.SetAlwaysActive(health: true);
            station.Baby.anim.SetBool(Repair, true);
        }
        else
        {
            station.Baby.uiController.SetAlwaysActive(health: false);
            station.Baby.anim.SetBool(Repair, false);
        }
    }
}
