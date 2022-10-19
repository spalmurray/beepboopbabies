using System;
using UnityEngine;

[RequireComponent(typeof(AgentState))]
public class StationInteractable : Interactable
{
    public BabyController baby;
    public delegate void PlaceEvent(bool placeInStation);
    public event PlaceEvent HandlePlaceEvent;
    
    public void InvokePlaceEvent()
    {
        HandlePlaceEvent?.Invoke(true);
    }
    public override void Interact(GameObject other)
    {
        var otherAgent = other.GetComponent<AgentState>();
        if (otherAgent == null) return;
        if (baby == null && otherAgent.pickedUpObject != null)
        {
            var otherGameObj = otherAgent.pickedUpObject.gameObject;
            // check if the pick up object is a baby (has a BabyController)
            var babyState = otherGameObj.GetComponent<BabyController>();
            if (babyState == null) return;
            var babyInteractable = otherGameObj.GetComponent<PickUpInteractable>();
            babyInteractable.Drop(otherAgent);
            babyInteractable.PickUp(GetComponent<AgentState>());
            baby = babyState;
            HandlePlaceEvent?.Invoke(true);
            baby.inStation = true;
        }
        else if (baby != null)
        {
            // Check if other agent does not have a pick up Object
            if (otherAgent.pickedUpObject != null) return;
            var babyInteractable = baby.gameObject.GetComponent<PickUpInteractable>();
            babyInteractable.Drop(GetComponent<AgentState>());
            babyInteractable.PickUp(otherAgent);
            baby.inStation = false;
            HandlePlaceEvent?.Invoke(false);
            baby = null;
        }
    }
}