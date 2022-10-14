using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSpawnManager : MonoBehaviour
{
    public GameObject parent;
    public GameObject child;
    public Transform start;

    public List<Transform> leavePoints;

    public List<Transform> arrivePoints;

    // time between spawning each parent
    public float delayTime = 2f;
    private BehaviorExecutor behaviorExecutor;

    private void Start()
    {
        StartCoroutine(SpawnMultipleParents());
    }

    private IEnumerator SpawnMultipleParents()
    {
        for (var i = 0; i < leavePoints.Count; i++)
        {
            yield return new WaitForSeconds(delayTime);
            SpawnParent(arrivePoints[i].position, leavePoints[i].position);
        }
    }

    private void SpawnParent(Vector3 arrivePoint, Vector3 leavePoint)
    {
        Debug.Log("Spawn Parents");
        var parentInstance = Instantiate(parent, start.position, Quaternion.identity);
        var childInstance = Instantiate(child, Vector3.zero, Quaternion.identity);
        var interactable = childInstance.GetComponent<PickUpInteractable>();
        // Programmatically make the parent pick up the child
        interactable.Interact(parentInstance);
        behaviorExecutor = parentInstance.GetComponent<BehaviorExecutor>();
        if (behaviorExecutor != null)
        {
            var instanceId = childInstance.GetInstanceID();
            behaviorExecutor.SetBehaviorParam("InstanceId", instanceId);
            behaviorExecutor.SetBehaviorParam("LeavePoint", leavePoint);
            behaviorExecutor.SetBehaviorParam("ArrivePoint", arrivePoint);
        }
    }
}