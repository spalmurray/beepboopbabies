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
    public Animator anim;
    public AudioClip audioPickup;
    public AudioSource walkAudioSource;
    private CharacterController controller;
    private Vector2 inputDirection;
    private Vector3 playerVelocity;
    private AgentState state;

    private bool isAimingWithMouse;
    private Vector3 mouseAimingPosition = Vector2.zero;

    private bool IsKickingWithMouse => isAimingWithMouse && IsKicking;
    private bool IsKickingWithKeyboard => IsKicking && Keyboard.current != null && !isAimingWithMouse;
    
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
    private static readonly int Walk = Animator.StringToHash("Walk");

    private bool IsKicking => kickingObject != null;
    private bool IsMoving => !IsKicking && inputDirection != Vector2.zero;

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
        walkAudioSource = GetComponent<AudioSource>();
        walkAudioSource.loop = true;
    }

    private void Update()
    {
        if (controller.isGrounded && playerVelocity.y < 0) playerVelocity.y = 0f;

        // Only allow moving while not kicking
        var move = new Vector3(inputDirection.x, 0, inputDirection.y);

        if (move != Vector3.zero || IsKickingWithMouse)
        {
            if (move != Vector3.zero && IsKicking)
            {
                // Keyboard input received, use that instead of mouse
                isAimingWithMouse = false;
            }
            var targetDirection = IsKickingWithMouse ? MouseAimingDirection : move;
            var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            var rotationDegrees = (IsKickingWithKeyboard ? kickRotationSpeed : moveRotationSpeed) * Time.deltaTime;
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
            
            if (kickingObject != state.pickedUpObject)
            {
                // Either baby went too far away, or another object entered
                // Note: I think this case can happen if another player snatches your baby while you're kicking lol
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

        if (IsMoving && !ScoreManager.Instance.IsGameOver)
        {
            anim.SetBool(Walk, true);
            if (!walkAudioSource.isPlaying)
            {
                walkAudioSource.Play();
            }
        }
        else
        {
            anim.SetBool(Walk, false);
            if (walkAudioSource.isPlaying)
            {
                walkAudioSource.Stop();
            }
        }
        
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>() * PLAYER_VELOCITY_MULTIPLYIER;
    }

    public void OnInteract()
    {
        if (state.interactable != null) {
            //case 1: interact with object detected by collider
            state.interactable.Interact(gameObject);
            GetComponent<AudioSource>().PlayOneShot(audioPickup);// the audio for pickup
        } else if (state.pickedUpObject != null) { 
            //case 2: interact with the object we picked up
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
        if (IsKicking)
        {
            // Use mouse to aim when mouse moved while kicking
            isAimingWithMouse = true;
        }
        mouseAimingPosition = ray.GetPoint(distance);
    }

    public void OnMouseKick(InputValue value)
    {
        // When we kick using left click, default to aiming towards mouse location
        // even when mouse hasn't moved
        isAimingWithMouse = true;
        OnKick(value);
    }
    
    public void OnKick(InputValue value)
    {
        var buttonDown = value.Get<float>() != 0;
        
        if (buttonDown && !IsKicking)
        {
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
        else if (!buttonDown && IsKicking)
        {
            // Release if was previously still kicking
            // Drop picked up object before kicking
            state.pickedUpObject.Interact(gameObject);
            
            kickingObject.Kick(KickDirection * kickSpeed);
            ResetKick();
        }
    }

    private void ResetKick()
    {
        kickingObject = null;
        kickSpeed = startingKickSpeed;
        isAimingWithMouse = false;
    }

    public void OnPause()
    {
        FindObjectOfType<PauseMenu>().TogglePause();
    }
}