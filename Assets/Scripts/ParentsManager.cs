using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TheKiwiCoder;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class ParentsManager : MonoBehaviour
{
    public GameObject parentPrefab;
    public GameObject spawnPlane;
    public GameObject arrivePlane;
    public float returnTime = 20f;
    // Start is called before the first frame update
    public int NumberOfParents = 3;
    
    void Start()
    {
        for (int i = 0; i < NumberOfParents; i++)
        {
            Vector3 min = spawnPlane.GetComponent<MeshFilter>().mesh.bounds.min;
            Vector3 max = spawnPlane.GetComponent<MeshFilter>().mesh.bounds.max;
            Vector3 pos = spawnPlane.transform.position -  
                          new Vector3 ((Random.Range(min.x, max.x)), spawnPlane.transform.position.y, (Random.Range(min.z, max.z)));
            var parent= Instantiate(parentPrefab, Vector3.zero, Quaternion.identity);
            var height = parent.GetComponent<Collider>().bounds.size.y;
            pos.y += height / 2;
            parent.transform.position = pos;
            var parentState = parent.GetComponent<ParentState>();
            parentState.arrivePlane = arrivePlane;
            parentState.spawnPlane = spawnPlane;
        }
    }
}
