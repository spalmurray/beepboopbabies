using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    void Start ()
    {
        StartCoroutine(SpawnMultipleParents());
    }
    
    IEnumerator SpawnMultipleParents()
    {
        for(int i = 0; i < leavePoints.Count; i++)
        { 
            yield return new WaitForSeconds(delayTime);
            SpawnParent(arrivePoints[i].position, leavePoints[i].position);
        }
    }

    void SpawnParent(Vector3 arrivePoint, Vector3 leavePoint)
    {
        Debug.Log("Spawn Parents");
        GameObject parentInstance = Instantiate(parent, start.position, Quaternion.identity);
        GameObject childInstance = Instantiate(child, Vector3.zero, Quaternion.identity);
        var interactable = childInstance.GetComponent<PickUpInteractable>();
        // Programmatically make the parent pick up the child
        interactable.Interact(parentInstance);
        behaviorExecutor = parentInstance.GetComponent<BehaviorExecutor>();
        if (behaviorExecutor != null)
        {
            int instanceId = childInstance.GetInstanceID();
            behaviorExecutor.SetBehaviorParam("InstanceId", instanceId);
            behaviorExecutor.SetBehaviorParam("LeavePoint", leavePoint);
            behaviorExecutor.SetBehaviorParam("ArrivePoint", arrivePoint);
        }
    }
    
    
    
}
