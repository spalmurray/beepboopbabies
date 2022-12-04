using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
// Code attributes

// ConditionBase
namespace BBUnity.Actions
{
    [Action("MyActions/CheckForObjectGone")]
    [Help("Checks if the picked up object is gone")]
    public class CheckForObjectGone : GOAction
    {
        [InParam("state")] public ParentState state;

        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {
            return state.pickedUpObject == null ? TaskStatus.COMPLETED : TaskStatus.FAILED;
        } // OnUpdate
    }
}