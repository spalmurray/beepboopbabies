using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public const float PLAYER_VELOCITY_MULTIPLYIER = 10.0f;
    public bool hasObject;
    public GameObject pickUpPosition;
    private Rigidbody rb;
    private ColliderTrigger colliderTrigger;
    private float verticalSpeed = 0;
    private float gravity = 9.87f;
    private Vector2 inputDirection;
    private Vector2 prevDirection;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        colliderTrigger = GetComponentInChildren<ColliderTrigger>();
    }

    void Update()
    {
        if (inputDirection.x != 0 || inputDirection.y != 0)
        {
            prevDirection = inputDirection;
        }
        if (controller.isGrounded) verticalSpeed = 0;
        else verticalSpeed -= gravity * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, Mathf.Rad2Deg * Mathf.Atan2(prevDirection.x, prevDirection.y), 0);
        controller.Move(new Vector3(inputDirection.x, verticalSpeed, inputDirection.y) * Time.deltaTime);
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
            hasObject = !hasObject;
        }
        else if (hasObject && colliderTrigger.parentState != null)
        {
            colliderTrigger.parentState.PickUpBaby(colliderTrigger.interactable.gameObject);
            hasObject = !hasObject;
        }
        else if (colliderTrigger.interactable != null)
        {
            colliderTrigger.interactable.Interact(pickUpPosition);
            hasObject = !hasObject;
        }
    }
}
