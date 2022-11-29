using UnityEngine;

[RequireComponent(typeof(StationInteractable))]
[RequireComponent(typeof(BabyStationAudioPlayer))]
public class DiaperChange : MonoBehaviour
{
    // increment amount for each tick
    public float incrementAmount = 5f;

    // Start is called before the first frame update
    private StationInteractable station;

    private void Awake()
    {
        station = GetComponent<StationInteractable>();
        
        var audioPlayer = GetComponent<BabyStationAudioPlayer>();
        audioPlayer.shouldStartAudio = baby => baby.currentDiaper < baby.diaper - 1;
        audioPlayer.shouldEndAudio = baby => baby.currentDiaper >= baby.diaper - 1;
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
            station.Baby.uiController.SetAlwaysActive(diaper: true);
        }
        else
        {
            station.Baby.uiController.SetAlwaysActive(diaper: false);
        }
    }
}
