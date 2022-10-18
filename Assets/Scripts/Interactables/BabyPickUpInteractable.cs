using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPickUpInteractable : PickUpInteractable
{
    public override void Interact(GameObject other)
    {
        var controller = GetComponent<BabyController>();
        var agent = GetComponent<AgentState>();
        var otherAgent = other.GetComponent<AgentState>();
        if (otherAgent == null) return;
        if (otherAgent.pickedUpObject is BottleInteractable drinkBottle)
        {
            drinkBottle.Drop(otherAgent);
            drinkBottle.PickUp(agent);
            controller.uiController.SetAlwaysActive(oil: true);
        } 
        else if (agent.pickedUpObject is BottleInteractable dropBottle && otherAgent.pickedUpObject == null)
        {
            dropBottle.Drop(agent);
            dropBottle.PickUp(otherAgent);
            controller.uiController.SetAlwaysActive(oil: false);
        }
        else
        {
            base.Interact(other);
        }
    }
}

