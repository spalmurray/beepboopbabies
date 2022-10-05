using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public PickUpInteractable interactable;
    public StationInteractable station;
    public ParentState parentState;

    private void OnTriggerEnter(Collider other)
    {
        var otherObject = other.gameObject;
        var interactableObj = otherObject.GetComponent<PickUpInteractable>();
        if (interactable == null && interactableObj != null){
            interactable = interactableObj;
        }
        var stationObj = otherObject.GetComponent<StationInteractable>();
        if (station == null && stationObj != null){
            station = stationObj;
        }
        var parentObj = otherObject.GetComponent<ParentState>();
        if (station == null && parentObj != null){
            parentState = parentObj;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        var interactableObj = other.gameObject.GetComponent<Interactable>();
        if (ReferenceEquals(interactableObj, interactable))
        {
            interactable = null;
        }
        var stationObj = other.GetComponent<StationInteractable>();
        if (ReferenceEquals(stationObj, station))
        {
            station = null;
        }
        var parentObj = other.GetComponent<ParentState>();
        if (ReferenceEquals(parentObj, parentState))
        {
            parentState = null;
        }
    }
}
