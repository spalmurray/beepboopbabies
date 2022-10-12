using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public PickUpInteractable interactable;
    public StationInteractable station;

    private void OnTriggerEnter(Collider other)
    {
        var otherObject = other.gameObject;
        Debug.Log("colliding with " + other.gameObject.name);
        var interactableObj = otherObject.GetComponent<PickUpInteractable>();
        if (interactable == null && interactableObj != null){
            interactable = interactableObj;
        }
        var stationObj = otherObject.GetComponent<StationInteractable>();
        if (station == null && stationObj != null){
            station = stationObj;
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
    }
}
