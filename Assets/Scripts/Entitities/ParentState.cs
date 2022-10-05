using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class ParentState : MonoBehaviour
{
    public GameObject arrivePlane;
    public GameObject spawnPlane;
    public GameObject baby;
    public Vector3 targetLocation;
    public GameObject pickUpPosition;
    public bool babyRetrieved;

    public void SetRandomLocation(bool arrive)
    {
        GameObject plane = arrive ? arrivePlane : spawnPlane;
        Vector3 min = plane.GetComponent<MeshFilter>().mesh.bounds.min;
        Vector3 max = plane.GetComponent<MeshFilter>().mesh.bounds.max;
        Vector3 pos = plane.transform.position -  
                      new Vector3 ((Random.Range(min.x, max.x)), plane.transform.position.y, (Random.Range(min.z, max.z)));
        targetLocation = pos;
    }
    public void DropOffBaby()
    {
        baby.GetComponent<NavMeshAgent>().enabled = true;
        baby.GetComponent<BehaviourTreeRunner>().enabled = true;
        baby.GetComponent<BabyState>().uiController.EnableStatusBars();
        baby.transform.parent = null;
    }
    
    public void PickUpBaby(GameObject otherBaby)
    {
        otherBaby.GetComponent<NavMeshAgent>().enabled = false;
        otherBaby.GetComponent<BehaviourTreeRunner>().enabled = false;
        otherBaby.GetComponent<BabyState>().uiController.DisableStatusBars();
        otherBaby.transform.parent = gameObject.transform;
        otherBaby.transform.position = pickUpPosition.transform.position;
        babyRetrieved = true;
    }
}
