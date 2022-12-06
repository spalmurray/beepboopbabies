using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBCore.Actions
{
    /// <summary>
    ///     Implementation of the wait action using busy-waiting (spinning).
    /// </summary>
    [Action("Basic/WaitForSecondStates")]
    [Help("Action that success after a period of time.")]
    public class WaitForSecondState : BasePrimitiveAction
    {
        private float elapsedTime;

        ///<value>Input Amount of time to wait (in seconds) Parameter.</value>
        [InParam("state")]
        public ParentState state;

        /// <summary>Initialization Method of WaitForSeconds.</summary>
        /// <remarks>Initializes the elapsed time to 0.</remarks>
        public override void OnStart()
        {
            elapsedTime = 0;
            Debug.Log("Waiting for " + state.waitForSeconds);
        }

        /// <summary>Method of Update of WaitForSeconds.</summary>
        /// <remarks>Increase the elapsed time and check if you have exceeded the waiting time has ended.</remarks>
        public override TaskStatus OnUpdate()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= state.waitForSeconds)
                return TaskStatus.COMPLETED;
            return TaskStatus.RUNNING;
        }
    }
}