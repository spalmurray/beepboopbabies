using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
// Code attributes

// TaskStatus

[Action("MyActions/DropOrPickupObject")]
[Help("Causes an Agent to Drop off or Pick up an Object")]
public class DropOrPickupObject : GOAction
{
    [InParam("state")] public AgentState state;

    // Main class method, invoked by the execution engine.
    public override TaskStatus OnUpdate()
    {
        // if the agent is holding onto the object then drop it
        if (state.pickedUpObject != null)
        {
            state.pickedUpObject.Interact(gameObject);
            return TaskStatus.COMPLETED;
        }

        // if the agent is near a Interactable that can be pick up then pick it up
        if (state.interactable != null && state.interactable is PickUpInteractable)
        {
            state.interactable.Interact(gameObject);
            return TaskStatus.COMPLETED;
        }

        return TaskStatus.FAILED;
    } // OnUpdate
} // class ShootOnce