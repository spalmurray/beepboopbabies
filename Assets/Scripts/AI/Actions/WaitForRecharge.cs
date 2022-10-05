using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class WaitForRecharge : ActionNode
{
    public float threshold = 90f;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.babyState.currentEnergy <= threshold)
        {
            return State.Running;
        }
        return State.Success;
    }
}
