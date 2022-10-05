using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpInteractable : Interactable
{
    public bool isPickedUp = false;

    public override void Interact(GameObject playerObject)
    {
        isPickedUp = !isPickedUp;

        if (isPickedUp)
        {
            PickUp(playerObject);
        }
        else
        {
            Drop(playerObject);
        }
    }

    protected virtual void PickUp(GameObject parentObject)
    {
        transform.parent = parentObject.transform;
        transform.localPosition = Vector3.zero;
        // disable for now baby gets really buggy if kinematic is set to true
        //GetComponent<Rigidbody>().isKinematic = true;
    }

    protected virtual void Drop(GameObject parentObject)
    {
        //GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
}
