using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleStationInteractable : Interactable
{
    public GameObject oilBottle;
    public float spawnBottleCooldown = 1.5f;
    
    private bool canSpawnBottle = true;
    
    public override void Interact(GameObject playerObject)
    {
        if (!canSpawnBottle) return;
        
        var agent = playerObject.GetComponent<AgentState>();
        if (!agent) return;

        var bottleObj = Instantiate(oilBottle);
        bottleObj.GetComponent<PickUpInteractable>().PickUp(agent);

        canSpawnBottle = false;
        Invoke(nameof(CanSpawn), spawnBottleCooldown);
    }
    
    private void CanSpawn()
    {
        canSpawnBottle = true;
    }
}
