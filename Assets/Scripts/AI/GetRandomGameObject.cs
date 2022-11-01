using System.Collections.Generic;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using UnityEngine;

namespace BBUnity.Actions
{

    [Action("Vector3/GetRandomGameObject")]
    [Help("Gets a random gameobject")]
    public class GetRandomGameObject : GOAction
    {

        [InParam("state")]
        [Help("potential targets to choose from")]
        public BabyState baby { get; set; }
        
        [OutParam("random target")]
        [Help("Random target to choose")]
        public GameObject target { get; set; }
        
        public override void OnStart()
        {
            if (baby == null || baby.peers == null)
            {
                Debug.LogError("No targets to choose from");
                return;
            }
            // choose targets randomly
            if (baby.peers.Count > 0)
            {
                target = baby.peers[Random.Range(0, baby.peers.Count)];
            }
            else
            {
                Debug.LogWarning("No targets to choose from");
            }
        }

        /// <summary>Abort method of GetRandomInArea.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            if (target != null)
            {
                return TaskStatus.COMPLETED;
            }
            return TaskStatus.FAILED;
        }
    }
}