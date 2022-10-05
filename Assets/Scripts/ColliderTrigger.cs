using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public Interactable interactable;

    private void OnTriggerEnter(Collider other)
    {
        var interactableObj = other.gameObject.GetComponent<Interactable>();
        if (interactable == null && interactableObj != null){
            interactable = interactableObj;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactableObj = other.gameObject.GetComponent<Interactable>();
        if (Object.ReferenceEquals(interactableObj, interactable))
        {
            interactable = null;
        }
    }
}
