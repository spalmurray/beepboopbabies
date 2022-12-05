using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Unity.VisualScripting;
using UnityEngine;
// Code attributes

namespace BBUnity.Actions
{
    [Action("MyActions/SetIsLeaving")]
    [Help("Updates the Score by specified amount")]
    public class SetIsLeaving : GOAction
    {
        [InParam("state")] public ParentState state;

        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {
            state.isLeaving = true;
            return TaskStatus.COMPLETED;
        } // OnUpdate
    }
}