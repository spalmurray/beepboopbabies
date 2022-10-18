using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AgentState))]
public class PlayerController : MonoBehaviour
{
    public const float PLAYER_VELOCITY_MULTIPLYIER = 10.0f;
    public const float KICK_SPEED = 15;
    public const float KICK_UPWARD_ANGLE = 30;
    public Vector3 startPosition;
    private CharacterController controller;
    private Vector2 inputDirection;
    private Vector3 playerVelocity;
    private AgentState state;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        state = GetComponent<AgentState>();
    }

    private void Start()
    {
        controller.SetPosition(startPosition);
    }

    private void Update()
    {
        if (controller.isGrounded && playerVelocity.y < 0) playerVelocity.y = 0f;

        var move = new Vector3(inputDirection.x, 0, inputDirection.y);
        if (move != Vector3.zero) transform.forward = move;

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
        //case 1: interact with object detected by collider
        if (state.interactable != null)
            state.interactable.Interact(gameObject);
        //case 2: interact with the object we picked up
        else if (state.pickedUpObject != null) state.pickedUpObject.Interact(gameObject);
    }

    public void OnKick()
    {
        var interactable = state.interactable;
        if (interactable == null) return;
        var obj = interactable.gameObject;

        // Check that we're indeed kicking a baby and not other innocent objects
        var babyController = obj.GetComponent<BabyController>();
        if (babyController != null)
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

            babyController.HandleKicked();
        }
    }

    public void OnPause()
    {
        FindObjectOfType<PauseMenu>().TogglePause();
    }
}