using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
// Code attributes

// ConditionBase
namespace BBUnity.Actions
{
    [Action("MyActions/CheckForPosition")]
    [Help("Check position at the front of queue")]
    public class CheckForPosition : GOAction
    {
        public ParentState state;

        public override void OnStart()
        {
            state = gameObject.GetComponent<ParentState>();
        }

        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {
            return state.frontOfQueue ? TaskStatus.COMPLETED : TaskStatus.FAILED;
        } // OnUpdate
    }
}