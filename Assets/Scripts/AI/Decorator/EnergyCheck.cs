using TheKiwiCoder;
using UnityEngine;

/// <summary>
/// Checks if energy is below threshold only 
/// </summary>
public class EnergyCheck : DecoratorNode
{
    public float threshold = 90;
    protected override void OnStart() {
    }

    protected override void OnStop() {
        
    }

    protected override State OnUpdate() {
        if (context.babyState.currentEnergy <= threshold)
        {
            Debug.Log("baby energy below threshold");
            return child.Update();
        }
        return State.Success;
    }
}
