using TheKiwiCoder;

public class WaitForBabyPickUp : ActionNode
{
  
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (!context.parentState.babyRetrieved)
        {
            return State.Running;
        }
        return State.Success;
    }
}