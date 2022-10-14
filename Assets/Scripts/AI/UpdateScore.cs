using UnityEngine;
using Pada1.BBCore; // Code attributes
using Pada1.BBCore.Tasks; // TaskStatus

[Action("MyActions/UpdateScore")]
[Help("Updates the Score by specified amount")]
public class UpdateScore : BBUnity.Actions.GOAction
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
} // class ShootOnce