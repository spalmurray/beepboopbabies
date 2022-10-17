﻿using System;
using UnityEngine;

public class StationInteractable : Interactable
{
    public Transform center;
    public BabyController baby;

    public override void Interact(GameObject other)
    {
        var otherAgent = other.GetComponent<AgentState>();
        if (otherAgent == null) return;
        if (baby == null && otherAgent.pickedUpObject != null)
        {
            var otherGameObj = otherAgent.pickedUpObject.gameObject;
            // check if the pick up object is a baby (has a BabyController)
            var babyController = otherGameObj.GetComponent<BabyController>();
            if (babyController == null) return;
            var otherTransform = otherGameObj.transform;
            otherTransform.parent = center;
            otherTransform.localPosition = Vector3.zero;
            otherAgent.pickedUpObject = null;
            baby = babyController;
            baby.inStation = true;
        }
        else if (baby != null)
        {
            // Check if other agent does not have a pick up Object
            if (otherAgent.pickedUpObject != null) return;
            var babyTransform = baby.transform;
            babyTransform.parent = otherAgent.pickUpPoint;
            babyTransform.localPosition = Vector3.zero;
            otherAgent.pickedUpObject = baby.gameObject.GetComponent<PickUpInteractable>();
            baby.inStation = false;
            baby = null;
        }
    }
}