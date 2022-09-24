using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public GameObject interactableObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable")){
            interactableObject = other.gameObject;
        }
        Debug.Log(other.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        if (Object.ReferenceEquals(other.gameObject, interactableObject))
        {
            interactableObject = null;
        }
    }
}
