using TheKiwiCoder;

public class DropOffBaby : ActionNode
{

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.parentState.DropOffBaby();
        return State.Success;
    }
}