using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentState : AgentState
{
    public int childId;
    public bool isLeaving;
    public bool readyForPickUp;
    // list of points where parents wait in line
    public Vector3 currentTargetPoint;
    public Vector3 waitPoint;
    public bool frontOfQueue;
    public float waitForSeconds;
}
