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
    public float maxKickDirectionChange = 45;
    public float kickDirectionChangeSpeed = 120;

    private CharacterController controller;
    private Vector2 inputDirection;
    private Vector3 playerVelocity;
    private AgentState state;

    private KickTrajectoryRenderer kickTrajectoryRenderer;
    private float kickSpeed;
    private float kickDirectionChange;
    private BabyController kickingBaby;

    private bool IsKicking => kickingBaby != null;

    private bool IsKickingPickedUpBaby =>
        state.pickedUpObject && state.pickedUpObject.gameObject == kickingBaby.gameObject;
    
    private Vector3 KickDirection
    {
        get
        {
            var forward = transform.forward;
            var originalDirection = Mathf.Atan2(forward.z, forward.x);
            var currentDirection = StandardizeAngle(originalDirection + kickDirectionChange);
            var x = Mathf.Cos(currentDirection);
            var z = Mathf.Sin(currentDirection);
            var y = Mathf.Sqrt(x * x + z * z) * Mathf.Tan(Mathf.Deg2Rad * KICK_UPWARD_ANGLE);
            return new Vector3(x, y, z).normalized;
        }
    }

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

        if (move != Vector3.zero && !IsKicking)
        {
            transform.forward = move;
        }

        playerVelocity.x = move.x;
        playerVelocity.z = move.z;
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;

        if (IsKicking)
        {
            // Adjust kick angle
            if (move != Vector3.zero)
            {
                // Adjust kick direction
                var forward = transform.forward;
                var targetDirection = Mathf.Atan2(move.z, move.x);
                var originalDirection = Mathf.Atan2(forward.z, forward.x);
                var currentDirection = StandardizeAngle(originalDirection + kickDirectionChange);

                // Check whether to increase angle or decrease
                var changeMultiplier = targetDirection > currentDirection ? 1 : -1;
                if (Mathf.Abs(targetDirection - currentDirection) > Mathf.PI) changeMultiplier *= -1;
                
                var directionChange = changeMultiplier * kickDirectionChangeSpeed * Mathf.Deg2Rad * Time.deltaTime;

                // Ensure direction is within max range
                kickDirectionChange = Mathf.Clamp(
                    kickDirectionChange + directionChange,
                    -maxKickDirectionChange * Mathf.Deg2Rad,
                    maxKickDirectionChange * Mathf.Deg2Rad
                );

                // Prevent moving when kicking
                playerVelocity.x = 0;
                playerVelocity.z = 0;
            }
            
            if (!IsKickingPickedUpBaby &&
                (!state.interactable || kickingBaby.gameObject != state.interactable.gameObject))
            {
                // Either baby went too far away, or another object entered
                ResetKick();
            }
            else
            {
                kickSpeed = Mathf.Min(kickSpeed + kickSpeedIncrease * Time.deltaTime, maxKickSpeed);
                
                var rigidBody = kickingBaby.gameObject.GetComponent<Rigidbody>();
                var kickVelocity = rigidBody.velocity + KickDirection * kickSpeed;
                
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

            // Check if we have a picked up baby to kick
            if (state.pickedUpObject)
            {
                kickingBaby = state.pickedUpObject.GetComponent<BabyController>();
            }
            
            // If no picked up baby, check if there's a baby in interact range
            if (!kickingBaby && state.interactable)
            {
                kickingBaby = state.interactable.GetComponent<BabyController>();
            }
        }
        else if (IsKicking)
        {
            // Release if was previously still kicking
            // Origin position of the kick, i.e. players feet
            if (IsKickingPickedUpBaby)
            {
                // Drop picked up baby before kicking
                state.pickedUpObject.Interact(gameObject);
            }
            kickingBaby.KickBaby(KickDirection * kickSpeed);
            ResetKick();
        }
    }

    private void ResetKick()
    {
        kickingBaby = null;
        kickSpeed = startingKickSpeed;
        kickDirectionChange = 0;
    }

    public void OnPause()
    {
        FindObjectOfType<PauseMenu>().TogglePause();
    }

    // Adjust angle in radians to be in (-pi, pi]
    private static float StandardizeAngle(float angle) => Mathf.Repeat(angle + Mathf.PI, 2 * Mathf.PI) - Mathf.PI;
}