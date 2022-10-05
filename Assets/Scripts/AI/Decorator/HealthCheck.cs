using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class HealthCheck : DecoratorNode
{
    public float threshold = 50;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.babyState.health < threshold)
        {
            return child.Update();
        }
        return State.Success;
    }
}
