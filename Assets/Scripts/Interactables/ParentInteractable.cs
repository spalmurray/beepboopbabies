using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentInteractable : StationInteractable
{
    private ParentState state;
    private float delayTime = 100f;
    public const float KICK_SPEED = 35;
    // how many seconds before the end of game that parents will return 
    public const float KICK_UPWARD_ANGLE = 45;
    private void Awake()
    {
        state = GetComponent<ParentState>();
    }
    public override void Interact(GameObject other)
    {
        // can only interact with parent at the front of queue
        if (!state.frontOfQueue)
        {
            return;
        }
        if (!state.isLeaving)
        {
            base.Interact(other);
            return;
        }
        var otherAgent = other.GetComponent<AgentState>();
        if (otherAgent.pickedUpObject == null) return;
        var pickedUpObject = otherAgent.pickedUpObject.gameObject;
        // check if the pick up object is a baby (has a BabyController)
        var babyController = pickedUpObject.GetComponent<BabyController>();
        if (babyController == null) return;
        // check if baby belongs to parent
        if (pickedUpObject.GetInstanceID() == state.childId)
        {
            Debug.Log("Baby belongs to parent gameobj id:" + pickedUpObject.GetInstanceID() + " child id: " + state.childId);
            base.Interact(other);
        }
        else
        {
            // kick the baby if its not ours
            Debug.Log("This is not my kid expecting: " + state.childId + " got: " + other.gameObject.GetInstanceID());
            var interactable = pickedUpObject.GetComponent<BabyPickUpInteractable>();
            interactable.Drop(other.GetComponent<AgentState>());
            interactable.Kick(gameObject, KICK_SPEED, KICK_UPWARD_ANGLE);
        }

    }
}
