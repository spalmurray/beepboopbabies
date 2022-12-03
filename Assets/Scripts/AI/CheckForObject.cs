using Pada1.BBCore;
using Pada1.BBCore.Framework;

// ConditionBase
namespace BBUnity.Conditions
{
    [Condition("MyConditions/CheckForObject")]
    [Help("Checks whether Agent is interacting with an Object that matches a unique Id")]
    public class CheckForObject : ConditionBase
    {
        [InParam("state")] public AgentState state;

        public static bool firstTimeChecked = false;

        public override bool Check()
        {
            firstTimeChecked = true;
            return state.pickedUpObject != null;
        }
    }
}