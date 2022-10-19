using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
// Code attributes
// TaskStatus

// BasePrimitiveAction

namespace BBSamples.PQSG // Programmers Quick Start Guide
{
    /// <summary>
    ///     It is an action that inherits from a primitive base action that updates the status of the behavior to suspended, in
    ///     this case
    ///     is suspended by changing the brightness.
    /// </summary>
    [Action("Samples/ProgQuickStartGuide/SleepForever")]
    [Help("Low-cost infinite action that never ends. It does not consume CPU at all.")]
    public class DoneSleepForever : BasePrimitiveAction
    {
        // Main class method, invoked by the execution engine.
        /// <summary>Method of onUpdate of SleepForever.</summary>
        /// <remarks>Change the status of the task.</remarks>
        /// <return>Value of the status of the suspended task.</return>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.SUSPENDED;
        } // OnUpdate
    } // class DoneSleepForever
} // namespace