using System.Runtime.CompilerServices;
using UnityEngine;

public class StationInteractable : Interactable
{
    public Transform center;
    public BabyState baby;
    public override void Interact(GameObject other)
    {
        if (baby == null)
        {
            // check if player has a baby
            var player = other.GetComponent<PlayerMovement>();
            if (player == null) return;
            var babyObject = other.GetComponentInChildren<ColliderTrigger>().interactable;
            if (babyObject == null) return;
            var babyGameObj = babyObject.gameObject;
            babyGameObj.transform.parent = center;
            babyGameObj.transform.localPosition = Vector3.zero;
            baby = babyGameObj.GetComponent<BabyState>();
            baby.rechargeBaby = true;
        }
        else
        {
            var player = other.GetComponent<PlayerMovement>();
            // only interact with players not holding anything
            if (player == null) return;
            var colliderTrigger = other.GetComponentInChildren<ColliderTrigger>();
            colliderTrigger.interactable = baby.gameObject.GetComponent<BabyPickUpInteractable>();
            baby.transform.parent = player.pickUpPosition.transform;
            baby.transform.localPosition = Vector3.zero;
            baby.rechargeBaby = false;
            baby = null;
        }
    }
}
