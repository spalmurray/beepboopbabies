using UnityEngine;

[RequireComponent(typeof(StationInteractable))]
public class DiaperChange : MonoBehaviour
{
    // increment amount for each tick
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
            station.baby.uiController.SetAlwaysActive(diaper: true);
        }
        else
        {
            station.baby.uiController.SetAlwaysActive(diaper: false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (station.baby)
        {
            station.baby.IncreaseDiaper(incrementAmount * Time.deltaTime);
        }
    }
}
