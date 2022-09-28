using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPickUpInteractable : PickUpInteractable
{
    private BabyState state;

    private bool hasDroppedOff;

    private GameObject touchingDropOffObject;

    private void Start()
    {
        state = GetComponent<BabyState>();
    }

    protected override void Drop(GameObject playerObject)
    {
        base.Drop(playerObject);

        if (!hasDroppedOff && touchingDropOffObject != null)
        {
            DropOffBaby();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasDroppedOff || touchingDropOffObject != null || !IsValidDropOffObject(collision.gameObject))
        {
            return;
        }

        touchingDropOffObject = collision.gameObject;

        if (!isPickedUp)
        {
            DropOffBaby();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (ReferenceEquals(collision.gameObject, touchingDropOffObject))
        {
            touchingDropOffObject = null;
        }
    }

    private bool IsValidDropOffObject(GameObject gameObject)
    {
        var dropOff = gameObject.gameObject.GetComponent<BabyDropOff>();
        return dropOff != null && dropOff.babyId == state.id;
    }

    private void DropOffBaby()
    {
        hasDroppedOff = true;
        ScoreManager.Instance.UpdateScore(1);
    }
}
