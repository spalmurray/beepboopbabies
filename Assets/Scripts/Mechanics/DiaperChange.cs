using UnityEngine;

public class DiaperChange : MonoBehaviour
{
    // increment amount for each tick
    public float incrementAmount = 5f;

    // Start is called before the first frame update
    private StationInteractable station;

    private void Start()
    {
        station = GetComponent<StationInteractable>();
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
