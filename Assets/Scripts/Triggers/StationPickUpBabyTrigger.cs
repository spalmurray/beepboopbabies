using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentState))]
[RequireComponent(typeof(StationInteractable))]
public class StationPickUpBabyTrigger : MonoBehaviour
{
    private AgentState state;
    private StationInteractable station;
    
    void Start()
    {
        state = GetComponent<AgentState>();
        station = GetComponent<StationInteractable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var babyController = collision.gameObject.GetComponent<BabyController>();
        var babyInteractable = collision.gameObject.GetComponent<PickUpInteractable>();
        
        if (station.baby == null && babyController && !babyInteractable.isPickedUp)
        {
            babyInteractable.PickUp(state);
            babyController.inStation = true;
            station.baby = babyController;
            station.InvokePlaceEvent();
        }
    }
}
