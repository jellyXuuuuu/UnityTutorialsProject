using UnityEngine;

public enum PlayerMovementState
{
    Normal,
    Sprinting,
    Crouching
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    private Rigidbody playerRigidbody;
    [SerializeField] private Grounder playerGrounder;

    [SerializeField] private float baseSpeed;
    private float xInput = 0, zInput = 0;

    [SerializeField] private KeyCode playerJumpKey;
    [SerializeField] private float jumpImpulse;
    private bool shouldJump = false;
    private bool isGrounded;

    [SerializeField] private KeyCode sprintKey;
    [SerializeField] private float sprintSpeed;
    private bool shouldSprint = false;

    [SerializeField] private KeyCode crouchKey;
    [SerializeField] private float crouchSpeed;
    private bool shouldCrouch = false;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerGrounder.TouchedGround += OnTouchedGround;
        playerGrounder.LeftGround += OnLeftGround;
    }

    private void OnDisable()
    {
        playerGrounder.TouchedGround -= OnTouchedGround;
        playerGrounder.LeftGround -= OnLeftGround;
    }

    private void OnDestroy()
    {
        playerGrounder.TouchedGround -= OnTouchedGround;
        playerGrounder.LeftGround -= OnLeftGround;
    }

    private void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(playerJumpKey))
        {
            shouldJump = true;
        }
        else if(Input.GetKeyUp(playerJumpKey))
        {
            shouldJump = false;
        }

        if (Input.GetKeyDown(sprintKey))
        {
            shouldSprint = true;
        }
        else if(Input.GetKeyUp(sprintKey))
        {
            shouldSprint = false;
        }

        if (Input.GetKeyDown(crouchKey))
        {
            shouldCrouch = true;
        }
        else if (Input.GetKeyUp(crouchKey))
        {
            shouldCrouch = false;
        }

    }

    private void FixedUpdate()
    {
        Vector3 relativePositionDelta = (xInput * transform.right) + (zInput * transform.forward);

        switch (State)
        {
            case PlayerMovementState.Crouching:
                relativePositionDelta *= crouchSpeed;
                break;
            case PlayerMovementState.Sprinting:
                relativePositionDelta *= sprintSpeed;
                break;
            default:
                relativePositionDelta *= baseSpeed;
                break;
        }

        relativePositionDelta *= Time.fixedDeltaTime;

        playerRigidbody.MovePosition(transform.position + relativePositionDelta);

        if (shouldJump && isGrounded)
        {
            playerRigidbody.AddForce(new Vector3(0, jumpImpulse, 0), ForceMode.Impulse);
            shouldJump = false;
        }

    }

    public  PlayerMovementState State
    {
        get
        {
            if (shouldSprint)
            {
                return PlayerMovementState.Sprinting;
            }
            if (shouldCrouch)
            {
                return PlayerMovementState.Crouching;
            }

            return PlayerMovementState.Normal;
        }
    }

    private void OnTouchedGround(Collider other)
    {
        isGrounded = true;
    }

    private void OnLeftGround(Collider other)
    {
        isGrounded = false;
    }

}
