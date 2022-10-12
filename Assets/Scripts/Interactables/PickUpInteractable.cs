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
        var player = playerObject.GetComponent<PlayerMovement>();
        transform.parent = player.pickUpPosition.transform;
        transform.eulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    protected virtual void Drop(GameObject playerObject)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
}
