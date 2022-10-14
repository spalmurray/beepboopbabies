using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework;
using UnityEngine; // ConditionBase
 
[Condition("MyConditions/CheckForObject")]
[Help("Checks whether Agent is interacting with an Object that matches a unique Id")]
public class CheckForObject : ConditionBase
{
    [InParam("InstanceId")] public int InstanceId;
    [InParam("state")] public AgentState state;
    
    public override bool Check()
    {
        if (state.interactable != null && state.interactable.GetInstanceID() != InstanceId)
        {
            Debug.Log("Instance Id mismatch expecting " + InstanceId + " Got " + state.interactable.gameObject.GetInstanceID());
        }
        return state.interactable != null && state.interactable.gameObject.GetInstanceID() == InstanceId;
    }
}