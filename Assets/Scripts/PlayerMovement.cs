using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public const float PLAYER_VELOCITY_MULTIPLYIER = 10.0f;

    private Rigidbody rb;
    private ColliderTrigger colliderTrigger;

    private Vector2 inputDirection;
    private Vector2 prevDirection;
    private CharacterController controller;

    private GameObject pickedUpObject;

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

        transform.eulerAngles = new Vector3(0, Mathf.Rad2Deg * Mathf.Atan2(prevDirection.x, prevDirection.y), 0);
        controller.Move(new Vector3(inputDirection.x, 0, inputDirection.y) * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>() * PLAYER_VELOCITY_MULTIPLYIER;
    }

    public void OnInteract()
    {
        colliderTrigger.interactable?.Interact(gameObject);
    }
}
