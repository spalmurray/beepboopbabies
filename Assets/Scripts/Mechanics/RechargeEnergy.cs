using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeEnergy : MonoBehaviour
{
    public float rechargeTime = 1f;
    // Start is called before the first frame update
    private StationInteractable station;
    private float yVelocity;
    void Start()
    {
        station = GetComponent<StationInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        var state = station.baby;
        if (state != null)
        {
            state.IncreaseEnergy(Mathf.SmoothDamp(state.currentEnergy, state.energy, ref yVelocity, rechargeTime));
        }
    }
}
