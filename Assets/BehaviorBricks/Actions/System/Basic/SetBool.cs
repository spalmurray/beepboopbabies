using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;

namespace BBUnity.Actions
{
    /// <summary>
    ///     It is a basic action to associate a Boolean to a variable.
    /// </summary>
    [Action("Basic/SetBool")]
    [Help("Sets a value to a boolean variable")]
    public class SetBool : BasePrimitiveAction
    {
        ///<value>Input Boolean Parameter.</value>
        [InParam("value")] [Help("Value")] public bool value;


        ///<value>OutPut Boolean Parameter.</value>
        [OutParam("var")] [Help("output variable")]
        public bool var;

        /// <summary>Initialization Method of SetBool.</summary>
        /// <remarks>Initializes the Boolean value.</remarks>
        public override void OnStart()
        {
            var = value;
        }

        /// <summary>Method of Update of SetBool.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}