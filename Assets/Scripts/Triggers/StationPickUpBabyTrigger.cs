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
        if (station.pickedUpObject) return;
        
        var babyInteractable = collision.gameObject.GetComponent<BabyPickUpInteractable>();
        
        if (babyInteractable && !babyInteractable.isPickedUp)
        {
            station.PickUpObject(babyInteractable);
        }
    }
}
