using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBCore.Actions
{
    /// <summary>
    ///     Implementation of the wait action using busy-waiting (spinning).
    /// </summary>
    [Action("Basic/WaitForParents")]
    [Help("Action that success after a period of time.")]
    public class WaitForParents : BasePrimitiveAction
    {
        
        ///<value>Input Amount of time to wait (in seconds) Parameter.</value>
        [InParam("state")]
        public ParentState state;
        /// <summary>Method of Update of WaitForSeconds.</summary>
        /// <remarks>Increase the elapsed time and check if you have exceeded the waiting time has ended.</remarks>
        public override TaskStatus OnUpdate()
        {
            if (state.returnToKids)
                return TaskStatus.COMPLETED;
            return TaskStatus.RUNNING;
        }
    }
}