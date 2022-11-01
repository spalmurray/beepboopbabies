using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

// ConditionBase
namespace BBUnity.Conditions
{
    [Condition("MyConditions/CheckSadness")]
    [Help("Checks if the agent is sad")]
    public class CheckForSadness: ConditionBase
    {
        [InParam("state")] public BabyState state;

        public override bool Check()
        {
            return state.isSad;
        }
    }
}