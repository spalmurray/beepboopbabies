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
    public float moveRotationSpeed = 1000;
    public float kickRotationSpeed = 150;
    public AudioClip audioPickup;//Audio for Pickup
    private CharacterController controller;
    private Vector2 inputDirection;
    private Vector3 playerVelocity;
    private AgentState state;

    private bool mouseMoved;
    private Vector3 mouseAimingPosition = Vector2.zero;

    private bool IsAimingWithMouse => mouseMoved && IsKicking;
    private bool IsAimingWithKeyboard => IsKicking && Keyboard.current != null && !mouseMoved;
    
    private Vector3 MouseAimingDirection
    {
        get
        {
            var diff = mouseAimingPosition - transform.position;
            diff.y = 0;
            return diff.normalized;
        }
    }

    private KickTrajectoryRenderer kickTrajectoryRenderer;
    private float kickSpeed;
    private KickableInteractable kickingObject;

    private bool IsKicking => kickingObject != null;

    private bool IsKickingPickedUpBaby =>
        state.pickedUpObject && state.pickedUpObject.gameObject == kickingObject.gameObject;
    
    private Vector3 KickDirection
    {
        get
        {
            var forward = transform.forward;
            var x = forward.x;
            var z = forward.z;
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

        if (move != Vector3.zero || IsAimingWithMouse)
        {
            var targetDirection = IsAimingWithMouse ? MouseAimingDirection : move;
            var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            var rotationDegrees = (IsAimingWithKeyboard ? kickRotationSpeed : moveRotationSpeed) * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationDegrees);
        }

        playerVelocity.x = move.x;
        playerVelocity.z = move.z;
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;

        if (IsKicking)
        {
            // Prevent moving when kicking
            playerVelocity.x = 0;
            playerVelocity.z = 0;
            
            if (!IsKickingPickedUpBaby &&
                (!state.interactable || kickingObject.gameObject != state.interactable.gameObject))
            {
                // Either baby went too far away, or another object entered
                ResetKick();
            }
            else
            {
                kickSpeed = Mathf.Min(kickSpeed + kickSpeedIncrease * Time.deltaTime, maxKickSpeed);
                
                var rigidBody = kickingObject.gameObject.GetComponent<Rigidbody>();
                var kickVelocity = rigidBody.velocity + KickDirection * kickSpeed;
                
                kickTrajectoryRenderer.DrawTrajectory(rigidBody.position, kickVelocity);
            }
        }
        else
        {
            kickTrajectoryRenderer.ClearTrajectory();
        }
        
        controller.Move(playerVelocity * Time.deltaTime);

        mouseMoved = false;
    }

    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>() * PLAYER_VELOCITY_MULTIPLYIER;
    }

    public void OnInteract()
    {
        //case 1: interact with object detected by collider
        if (state.interactable != null) {
            state.interactable.Interact(gameObject);
            GetComponent<AudioSource>().PlayOneShot(audioPickup);// the audio for pickup
        } else if (state.pickedUpObject != null) {//case 2: interact with the object we picked up
            state.pickedUpObject.Interact(gameObject);
        }
    }

    public void OnMouseMove(InputValue value)
    {
        var position = value.Get<Vector2>();
        var ray = Camera.main.ScreenPointToRay(new Vector3(position.x, position.y, 0));
        
        // Check where the mouse pointer intersect with the plane at y=0
        var plane = new Plane(Vector3.up, Vector3.zero);
        if (!plane.Raycast(ray, out var distance)) return;
        mouseMoved = true;
        mouseAimingPosition = ray.GetPoint(distance);
    }

    public void OnKick(InputValue value)
    {
        if (value.Get<float>() != 0)
        {
            ResetKick();

            if (state.pickedUpObject)
            {
                // This may be null if the picked up object is not kickable
                kickingObject = state.pickedUpObject.GetComponent<KickableInteractable>();
            }
            else if (state.interactable)
            {
                // If interactable object is kickable, then pick it up before starting kick
                var kickObj = state.interactable.GetComponent<KickableInteractable>();
                if (!kickObj) return;
                state.interactable.Interact(gameObject);
                kickingObject = kickObj;
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
            kickingObject.Kick(KickDirection * kickSpeed);
            ResetKick();
        }
    }

    private void ResetKick()
    {
        kickingObject = null;
        kickSpeed = startingKickSpeed;
    }

    public void OnPause()
    {
        FindObjectOfType<PauseMenu>().TogglePause();
    }
}