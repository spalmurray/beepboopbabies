using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementAlongPath : MonoBehaviour
{
    // Utility coroutine to move the given game object along the path
    public static IEnumerator Move(GameObject gameObject, Vector3[] path, float speed = 1.0f)
    {
        var movement = gameObject.AddComponent<MovementAlongPath>();
        movement.positions = path;
        movement.speed = speed;

        var completed = false;
        movement.MoveCompleted += () => completed = true;
        
        yield return new WaitUntil(() => completed);

        Destroy(movement);
    }

    public delegate void MovementCompleteHandler();
    
    // Event to indicate that movement has completed
    public event MovementCompleteHandler MoveCompleted;

    // If absolute distance between position and target is lower than this threshold, counts as we arrived
    public float distanceThreshold = 0.1f;

    public float speed = 1.0f;

    public Vector3[] positions;
    public int currentPositionIndex = 0;

    private CharacterController characterController;

    void Start()
    {
        currentPositionIndex = 0;
        
        characterController = gameObject.GetComponent<CharacterController>();
        if (positions.Length > 0)
        {
            characterController.enabled = false;
            characterController.transform.position = positions[0];
            characterController.enabled = true;
        }
    }

    void Update()
    {
        if (currentPositionIndex >= positions.Length - 1) return;

        var target = positions[currentPositionIndex + 1];

        var distance = speed * Time.deltaTime;
        // Motion is displacement from position to target if close enough
        // otherwise move distance units towards that direction
        var motion = Vector3.Distance(transform.position, target) > distance
            ? (target - transform.position).normalized * speed * Time.deltaTime
            : target - transform.position;
        characterController.Move(motion);

        // If close enough, stop and start moving towards next target
        if (Vector3.Distance(transform.position, target) < distanceThreshold)
        {
            currentPositionIndex++;

            if (currentPositionIndex >= positions.Length - 1)
            {
                MoveCompleted?.Invoke();
            }
        }
    }
}
