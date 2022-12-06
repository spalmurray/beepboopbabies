using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("MyActions/KickGameObject")]
    [Help("Kicks a specified gameobject")]
    public class KickGameObject : GOAction
    {
        [InParam("target")]
        [Help("object we want to kick")]
        public GameObject kickObject { get; set; }
        
        [InParam("kickSpeed")]
        [Help("speed of the kick")]
        public float kickSpeed { get; set; }
        
        [InParam("kickAngle")]
        [Help("kick angle")]
        public float kickAngle { get; set; }
        
        public override TaskStatus OnUpdate()
        {
            var kick = kickObject.GetComponent<KickableInteractable>();
            if (kick != null)
            {
                kick.Kick(gameObject, kickSpeed, kickAngle);
                return TaskStatus.COMPLETED;
            }
            Debug.Log("No kickable object found");
            return TaskStatus.FAILED;
        }
    }
}