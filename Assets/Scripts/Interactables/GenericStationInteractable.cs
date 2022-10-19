using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentState))]
public class GenericStationInteractable<T> : Interactable where T : PickUpInteractable
{
    public T pickUpObject;
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
        if (pickUpObject == null && otherAgent.pickedUpObject != null)
        {
            var otherGameObj = otherAgent.pickedUpObject.gameObject;
            var pickUpObj = otherGameObj.GetComponent<T>();
            pickUpObj.Drop(otherAgent);
            pickUpObj.PickUp(GetComponent<AgentState>());
            pickUpObject = pickUpObj;
            HandlePlaceEvent?.Invoke(true);
        }
        else if (pickUpObject != null)
        {
            // Check if other agent does not have a pick up Object
            if (otherAgent.pickedUpObject != null) return;
            pickUpObject.Drop(GetComponent<AgentState>());
            pickUpObject.PickUp(otherAgent);
            HandlePlaceEvent?.Invoke(false);
            pickUpObject = null;
        }
    }

}
