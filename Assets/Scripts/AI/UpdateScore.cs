using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
// Code attributes

// TaskStatus
namespace BBUnity.Actions
{
    [Action("MyActions/UpdateScore")]
    [Help("Updates the Score by specified amount")]
    public class UpdateScore : GOAction
    {
        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {
            var hud = GameObject.Find("HUD");
            var scoreManager = hud.GetComponent<ScoreManager>();
            var babyState = gameObject.GetComponent<ParentState>().pickedUpObject.GetComponent<BabyState>();
            scoreManager.RegisterPickedUpBaby(babyState);
            return TaskStatus.COMPLETED;
        } // OnUpdate
    }
}