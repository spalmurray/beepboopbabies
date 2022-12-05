using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
// Code attributes

// ConditionBase
namespace BBUnity.Actions
{
    [Action("MyActions/CheckForObject")]
    [Help("Checks if the picked up object is gone")]
    public class CheckForObject : GOAction
    {
        [InParam("state")] public ParentState state;

        public static bool firstTimeChecked = false;

        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {
            firstTimeChecked = true;
            return state.pickedUpObject != null ? TaskStatus.COMPLETED : TaskStatus.FAILED;
        } // OnUpdate
    }
}