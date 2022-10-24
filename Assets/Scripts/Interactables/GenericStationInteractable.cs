using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentState))]
public class GenericStationInteractable<T> : Interactable where T : PickUpInteractable
{
    public T pickedUpObject;
    public delegate void PlaceEvent(bool placeInStation);
    public event PlaceEvent HandlePlaceEvent;
    
    public void PickUpObject(T obj, AgentState otherAgent = null)
    {
        if (otherAgent)
        {
            obj.Drop(otherAgent);
        }
        obj.PickUp(GetComponent<AgentState>());
        pickedUpObject = obj;
        HandlePlaceEvent?.Invoke(true);
    }
    
    public override void Interact(GameObject other)
    {
        var otherAgent = other.GetComponent<AgentState>();
        if (otherAgent == null) return;

        if (otherAgent.pickedUpObject != null && otherAgent.pickedUpObject is not T) return;
        var newPickUpObject = otherAgent.pickedUpObject as T;

        var agent = GetComponent<AgentState>();
        var droppingObject = pickedUpObject;
        
        if (droppingObject)
        {
            // Drop current object in station
            droppingObject.Drop(agent);
            HandlePlaceEvent?.Invoke(false);
            pickedUpObject = null;
        }

        if (newPickUpObject)
        {
            // Put new object in station
            PickUpObject(newPickUpObject, otherAgent);
        }

        if (droppingObject)
        {
            // Let the other agent pick up the dropped object
            droppingObject.PickUp(otherAgent);
        }
    }
}
