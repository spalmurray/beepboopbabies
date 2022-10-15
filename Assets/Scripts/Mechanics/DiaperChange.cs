using UnityEngine;

public class DiaperChange : MonoBehaviour
{
    // increment amount for each tick
    public float incrementAmount = 5f;

    // Start is called before the first frame update
    private StationInteractable station;
    private bool inStation;
    private BabyController baby;

    private void Start()
    {
        station = GetComponent<StationInteractable>();
    }

    // Update is called once per frame
    private void Update()
    {
        //TODO: this is a really bad hack ideally you want to use events to inform when a station has a baby or not
        var newInStation = station.baby != null;
        if (inStation && !newInStation)
        {
            baby.rediaperBaby = false;
            baby = null;
        } else if (!inStation && newInStation)
        {
            baby = station.baby;
            baby.rediaperBaby = true;
        }
        inStation = newInStation;
        if (inStation && baby != null)
        {
            baby.IncreaseDiaper(incrementAmount * Time.deltaTime);
        }
    }
}
