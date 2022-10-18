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

    public List<string> childNames;
    
    private BehaviorExecutor behaviorExecutor;

    public int NumberOfParents => leavePoints.Count;

    private void Start()
    {
        childNames = new(){ "Bob", "Anna", "Gaston", "Lemmy"};
        StartCoroutine(SpawnMultipleParents());
    }

    private IEnumerator SpawnMultipleParents()
    {
        for (var i = 0; i < NumberOfParents; i++)
        {
            yield return new WaitForSeconds(delayTime);
            SpawnParent(arrivePoints[i].position, leavePoints[i].position, childNames[i]);
        }
    }

    private void SpawnParent(Vector3 arrivePoint, Vector3 leavePoint, string childName)
    {
        var parentInstance = Instantiate(parent, start.position, Quaternion.identity);
        var childInstance = Instantiate(child, Vector3.zero, Quaternion.identity);
        var childState = childInstance.GetComponent<BabyState>();
        var interactable = childInstance.GetComponent<PickUpInteractable>();
        // Programmatically make the parent pick up the child
        interactable.PickUp(parentInstance.GetComponent<AgentState>());
        parentInstance.GetComponent<ParentState>().childId = childInstance.GetInstanceID();
        childState.name = childName;
        behaviorExecutor = parentInstance.GetComponent<BehaviorExecutor>();
        if (behaviorExecutor != null)
        {
            behaviorExecutor.SetBehaviorParam("LeavePoint", leavePoint);
            behaviorExecutor.SetBehaviorParam("ArrivePoint", arrivePoint);
        }
    }
}