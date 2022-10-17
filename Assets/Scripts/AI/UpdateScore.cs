using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
// Code attributes

// TaskStatus

[Action("MyActions/UpdateScore")]
[Help("Updates the Score by specified amount")]
public class UpdateScore : GOAction
{
    [InParam("score")] public int score;

    // Main class method, invoked by the execution engine.
    public override TaskStatus OnUpdate()
    {
        var hud = GameObject.Find("HUD");
        var scoreManager = hud.GetComponent<ScoreManager>();
        scoreManager.UpdateScore(score);
        return TaskStatus.COMPLETED;
    } // OnUpdate
}