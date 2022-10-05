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
            if (player == null || !player.hasObject) return;
            var babyObject = other.GetComponentInChildren<ColliderTrigger>().interactable.gameObject;
            babyObject.transform.parent = gameObject.transform;
            babyObject.transform.position = center.position;
            baby = babyObject.GetComponent<BabyState>();
            baby.rechargeBaby = true;
        }
        else
        {
            var player = other.GetComponent<PlayerMovement>();
            // only interact with players not holding anything
            if (player == null || player.hasObject) return;
            var colliderTrigger = other.GetComponentInChildren<ColliderTrigger>();
            colliderTrigger.interactable = baby.GetComponent<BabyPickUpInteractable>();
            baby.transform.parent = player.pickUpPosition.transform;
            baby.transform.localPosition = Vector3.zero;
            baby.rechargeBaby = false;
            baby = null;
        }
    }
}
