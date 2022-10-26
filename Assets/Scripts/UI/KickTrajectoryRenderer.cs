using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class KickTrajectoryRenderer : MonoBehaviour
{
    // Reference: https://www.patrykgalach.com/2020/03/23/drawing-ballistic-trajectory-in-unity/
    
    private LineRenderer line;

    // Step distance for the trajectory
    [SerializeField] private float trajectoryVertDist = 0.25f;
    // Max length of the trajectory
    [SerializeField] private float maxCurveLength = 20;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void DrawTrajectory(Vector3 startPosition, Vector3 startVelocity)
    {
        // Create a list of trajectory points
        var curvePoints = new List<Vector3>();
        curvePoints.Add(startPosition);
        // Initial values for trajectory
        var currentPosition = startPosition;
        var currentVelocity = startVelocity;
        // Init physics variables
        RaycastHit hit;
        Ray ray = new Ray(currentPosition, currentVelocity.normalized);
        // Loop until hit something or distance is too great
        while (!(Physics.Raycast(ray, out hit, trajectoryVertDist) && !hit.collider.isTrigger)
               && Vector3.Distance(startPosition, currentPosition) < maxCurveLength)
        {
            // Time to travel distance of trajectoryVertDist
            var t = trajectoryVertDist / currentVelocity.magnitude;
            
            // Update position and velocity
            currentPosition += t * currentVelocity;
            currentVelocity += t * Physics.gravity;
            // Add point to the trajectory
            curvePoints.Add(currentPosition);
            // Create new ray
            ray = new Ray(currentPosition, currentVelocity.normalized);
        }
        // If something was hit, add last point there
        if (hit.transform)
        {
            curvePoints.Add(hit.point);
        }
        // Display line with all points
        line.positionCount = curvePoints.Count;
        line.SetPositions(curvePoints.ToArray());
    }
    
    public void ClearTrajectory()
    {
        // Hide line
        line.positionCount = 0;
    }
}
