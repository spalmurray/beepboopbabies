using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Unity.VisualScripting;
using UnityEngine;
// Code attributes

namespace BBUnity.Actions
{
    [Action("MyActions/SetReadyForPickup")]
    [Help("Sets parent to pick up child")]
    public class SetReadyForPickup : GOAction
    {
        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {
            var state = gameObject.GetComponent<ParentState>();
            state.readyForPickUp = true;
            return TaskStatus.COMPLETED;
        } // OnUpdate
    }
}