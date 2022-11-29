using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStationInteractble : StationInteractable
{
    // Start is called before the first frame update
    public float healthIncrement = 5f;
    public override void Interact(GameObject other)
    {
        var state = other.GetComponent<AgentState>();
        if (state == null) return;
        var bodyPart = other.GetComponentInChildren<BodyPartInteractable>();
        if (bodyPart == null)
        {
            base.Interact(other);
        }
        else if (bodyPart != null && pickedUpObject != null)
        {
            bodyPart.Drop(state);
            // reassemble body part
            var controller = pickedUpObject.gameObject.GetComponent<BabyController>();
            var success = controller.SetBodyPart(bodyPart.bodyPartType, bodyPart.renderer.material);
            if (success)
            {
                Destroy(bodyPart.gameObject);
            } 
            else
            {
                bodyPart.PickUp(state);
            }
        }
    }

    public override void FixStationObject()
    {
        Baby.IncreaseHealth(healthIncrement);
    }
}
