using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParentSpawnManager : MonoBehaviour
{
    public GameObject[] parent;
    public GameObject[] child;
    public Transform start;
    // random materials to assigned to parent and child objects
    public List<Material> materials;
    public List<Transform> leavePoints;

    public List<Transform> arrivePoints;

    // time between spawning each parent
    public float delayTime = 2f;

    public List<string> childNames;

    private BehaviorExecutor behaviorExecutor;

    public int NumberOfParents => leavePoints.Count;
    // both parent and child will have 4 unique materials assigned to them
    private int MaterialCount = 4;

    private void Start()
    {
        childNames = new List<string>() { "Bob", "Anna", "Gaston", "Lemmy" };
        StartCoroutine(SpawnMultipleParents());
    }

    private IEnumerator SpawnMultipleParents()
    {
        for (var i = 0; i < NumberOfParents; i++)
        {
            float delayRandom = Random.Range(1f, delayTime);//You can change parents spawn time here
            yield return new WaitForSeconds(delayRandom);
            SpawnParent(arrivePoints[i].position, leavePoints[i].position, childNames[i]);
        }
    }

    private void SpawnParent(Vector3 arrivePoint, Vector3 leavePoint, string childName)
    {
        int indexP = Random.Range(0, parent.Length);
        int indexC = Random.Range(0, child.Length);
        //randomize the color, parent and child will have same color
        var parentInstance = Instantiate(parent[indexP], start.position, Quaternion.identity);
        var childInstance = Instantiate(child[indexC], Vector3.zero, Quaternion.identity);
        var childState = childInstance.GetComponent<BabyState>();
        var interactable = childInstance.GetComponent<PickUpInteractable>();
        var mat = materials.PickRandom(MaterialCount).ToArray();
        // assign parent and child same materials
        var parentRenderer = parentInstance.GetComponentInChildren<Renderer>();
        var childRenderer = childInstance.GetComponentInChildren<Renderer>();
        var parentMat = parentRenderer.sharedMaterials;
        var childMat = childRenderer.sharedMaterials;
        for (int i = 0; i < MaterialCount; i++)
        {
            parentMat[i] = mat[i];
            childMat[i] = mat[i];
        }
        parentRenderer.sharedMaterials = parentMat;
        childRenderer.sharedMaterials = childMat;
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