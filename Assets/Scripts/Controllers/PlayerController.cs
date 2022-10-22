using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AgentState))]
[RequireComponent(typeof(KickTrajectoryRenderer))]
public class PlayerController : MonoBehaviour
{
    public const float PLAYER_VELOCITY_MULTIPLYIER = 10.0f;
    public const float KICK_UPWARD_ANGLE = 30;
    public Vector3 startPosition;
    public float maxKickSpeed = 15;
    public float startingKickSpeed = 7.5f;
    public float kickSpeedIncrease = 10;

    private CharacterController controller;
    private Vector2 inputDirection;
    private Vector3 playerVelocity;
    private AgentState state;

    private KickTrajectoryRenderer kickTrajectoryRenderer;
    private bool isKicking;
    private float kickSpeed;
    private BabyController kickingBaby;
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        state = GetComponent<AgentState>();
        kickTrajectoryRenderer = GetComponent<KickTrajectoryRenderer>();
    }

    private void Start()
    {
        controller.SetPosition(startPosition);
    }

    private void Update()
    {
        if (controller.isGrounded && playerVelocity.y < 0) playerVelocity.y = 0f;

        // Only allow moving while not kicking
        var move = new Vector3(inputDirection.x, 0, inputDirection.y);
        if (move != Vector3.zero)
        {
            transform.forward = move;
            if (isKicking)
            {
                // Cancel kicking if attempting to move while kicking
                ResetKick();                
            }
        }

        playerVelocity.x = move.x;
        playerVelocity.z = move.z;
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;

        if (isKicking)
        {
            if (!state.interactable || kickingBaby.gameObject != state.interactable.gameObject)
            {
                // Either baby went too far away, or another object entered
                ResetKick();
            }
            else
            {
                kickSpeed = Mathf.Min(kickSpeed + kickSpeedIncrease * Time.deltaTime, maxKickSpeed);
                
                var rigidBody = kickingBaby.gameObject.GetComponent<Rigidbody>();
                var kickVelocity = rigidBody.velocity + kickingBaby.CalculateKickVelocityChange(
                    gameObject,
                    kickSpeed,
                    KICK_UPWARD_ANGLE
                );
                
                kickTrajectoryRenderer.DrawTrajectory(rigidBody.position, kickVelocity);
            }
        }
        else
        {
            kickTrajectoryRenderer.ClearTrajectory();
        }
        
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>() * PLAYER_VELOCITY_MULTIPLYIER;
    }

    public void OnInteract()
    {
        //case 1: interact with object detected by collider
        if (state.interactable != null)
            state.interactable.Interact(gameObject);
        //case 2: interact with the object we picked up
        else if (state.pickedUpObject != null) state.pickedUpObject.Interact(gameObject);
    }

    public void OnKick(InputValue value)
    {
        if (value.Get<float>() != 0)
        {
            ResetKick();
            
            // Only start kicking if valid target in range
            var interactable = state.interactable;
            if (interactable == null) return;
            var obj = interactable.gameObject;
            // Check that we're indeed kicking a baby and not other innocent objects
            var babyController = obj.GetComponent<BabyController>();

            isKicking = true;
            kickingBaby = babyController;
        }
        else if (isKicking)
        {
            // Release if was previously still kicking
            // Origin position of the kick, i.e. players feet
            kickingBaby.KickBaby(gameObject, kickSpeed, KICK_UPWARD_ANGLE);
            ResetKick();
        }
    }

    private void ResetKick()
    {
        isKicking = false;
        kickingBaby = null;
        kickSpeed = startingKickSpeed;
    }

    public void OnPause()
    {
        FindObjectOfType<PauseMenu>().TogglePause();
    }
}