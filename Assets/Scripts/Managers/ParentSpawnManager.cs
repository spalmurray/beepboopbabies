using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSpawnManager : MonoBehaviour
{
    public GameObject[] parent;
    public GameObject[] child;
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
        childNames = new List<string>() { "Bob", "Anna", "Gaston", "Lemmy" };
        StartCoroutine(SpawnMultipleParents());
    }

    private IEnumerator SpawnMultipleParents()
    {
        for (var i = 0; i < NumberOfParents; i++)
        {
            float delayRandom = Random.Range(2f, 9f);//You can change parents spawn time here
            //yield return new WaitForSeconds(delayTime);
            yield return new WaitForSeconds(delayRandom);
            SpawnParent(arrivePoints[i].position, leavePoints[i].position, childNames[i]);
        }
    }

    private void SpawnParent(Vector3 arrivePoint, Vector3 leavePoint, string childName)
    {
        int index = Random.Range(0, 8);//randomize the color, parent and child will have same color
        var parentInstance = Instantiate(parent[index], start.position, Quaternion.identity);
        var childInstance = Instantiate(child[index], Vector3.zero, Quaternion.identity);
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