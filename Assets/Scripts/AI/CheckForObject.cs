using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;
// Code attributes

// ConditionBase

[Condition("MyConditions/CheckForObject")]
[Help("Checks whether Agent is interacting with an Object that matches a unique Id")]
public class CheckForObject : ConditionBase
{
    [InParam("state")] public AgentState state;

    public override bool Check()
    {
        return state.pickedUpObject != null;
    }
}