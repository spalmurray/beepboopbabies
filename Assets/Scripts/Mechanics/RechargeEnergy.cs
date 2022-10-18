using UnityEngine;

public class RechargeEnergy : MonoBehaviour
{
    // how long it will take to recharge to full
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
            station.baby.IncreaseEnergy(incrementAmount * Time.deltaTime);
        }
    }
}