using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public const float PLAYER_VELOCITY_MULTIPLYIER = 10.0f;

    public const float KICK_SPEED = 15;
    public const float KICK_UPWARD_ANGLE = 30;
    public Transform pickUpPosition;

    public Vector3 startPosition;

    private Rigidbody rb;
    private ColliderTrigger colliderTrigger;
    private Vector3 playerVelocity;
    private Vector2 inputDirection;
    private CharacterController controller;

    private GameObject pickedUpObject;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        colliderTrigger = GetComponentInChildren<ColliderTrigger>();
    }

    private void Start()
    {
        controller.SetPosition(startPosition);
    }

    void Update()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        var move = new Vector3(inputDirection.x, 0, inputDirection.y);
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        playerVelocity.x = move.x;
        playerVelocity.z = move.z;
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>() * PLAYER_VELOCITY_MULTIPLYIER;
    }

    public void OnInteract()
    {
        if (colliderTrigger.station != null)
        {
            colliderTrigger.station.Interact(gameObject);
        }
        else if (colliderTrigger.interactable != null)
        {
            colliderTrigger.interactable.Interact(gameObject);
        }
    }

    public void OnKick()
    {
        var interactable = colliderTrigger.interactable;
        if (interactable == null) return;

        // Ensure we're not kicking a picked up baby
        if ((interactable is PickUpInteractable && (interactable as PickUpInteractable).isPickedUp)) return;

        var obj = interactable.gameObject;

        // Check that we're indeed kicking a baby and not other innocent objects
        if (obj.GetComponent<BabyState>() != null)
        {
            // Origin position of the kick, i.e. players feet
            var kickOrigin = gameObject.transform.position;
            var lowY = gameObject.GetComponent<Collider>().bounds.min.y;
            kickOrigin.y = lowY;

            // Get direction of kick, rotate upward by KICK_UPWARD_ANGLE degrees
            var kickOriginDirection = obj.transform.position - kickOrigin;
            var x = kickOriginDirection.x;
            var z = kickOriginDirection.z;
            var y = Mathf.Sqrt(x * x + z * z) * Mathf.Tan(Mathf.Deg2Rad * KICK_UPWARD_ANGLE);
            var forceDirection = new Vector3(x, y, z);
            forceDirection.Normalize();

            // Apply kick force
            var babyRigidbody = obj.GetComponent<Rigidbody>();
            babyRigidbody?.AddForceAtPosition(forceDirection * KICK_SPEED, kickOrigin, ForceMode.VelocityChange);
        }
    }
}