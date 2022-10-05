using UnityEngine;
using TheKiwiCoder;

/// <summary>
/// Get a random location from a plane
/// Note very tightly coupled with ParentState atm might
/// want to refactor in the future
/// </summary>
public class GetRandomLocationPlane : ActionNode
{
    public bool arrive = true;
    protected override void OnStart()
    {
        context.parentState.SetRandomLocation(arrive);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
