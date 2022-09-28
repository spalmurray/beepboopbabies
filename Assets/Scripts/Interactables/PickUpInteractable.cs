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

    protected virtual void PickUp(GameObject playerObject)
    {
        transform.parent = playerObject.transform;
        var localPosition = transform.localPosition;
        localPosition.y = 0.25f;
        transform.localPosition = localPosition;

        GetComponent<Rigidbody>().isKinematic = true;
    }

    protected virtual void Drop(GameObject playerObject)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
}
