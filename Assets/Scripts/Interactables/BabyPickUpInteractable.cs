using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BehaviorExecutor))]
[RequireComponent(typeof(NavMeshAgent))]
public class BabyPickUpInteractable : KickableInteractable
{
    private BehaviorExecutor behaviorExecutor;
    private NavMeshAgent navMeshAgent;
    private bool locked = false;
    public void Start()
    {
        behaviorExecutor = GetComponent<BehaviorExecutor>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    public void DisableAI()
    {
        behaviorExecutor.enabled = false;
        navMeshAgent.enabled = false;
    }
    public void EnableAI()
    {
        if (!locked)
        {
            behaviorExecutor.enabled = true;
            navMeshAgent.enabled = true;
        }
    }
    public void LockBaby()
    {
        DisableAI();
        locked = true;
    }
    
    public void UnlockBaby()
    {
        HandleAI();
        locked = false;
    }

    private void HandleAI()
    {
        if (isPickedUp)
        {
            navMeshAgent.enabled = false;
            behaviorExecutor.enabled = false;
        }
        else
        {
            navMeshAgent.enabled = true;
            behaviorExecutor.enabled = true;
        }
    }

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
        
        if (!locked)
        {
            HandleAI();
        }
    }
}

