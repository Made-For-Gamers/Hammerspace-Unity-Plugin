using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AnimatorController))]

public class SP_AvatarController : MonoBehaviour
{
    
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private float rotatespeed = 7;
    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 1.1f;
    [Header("Falling")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private float fallSensitivity = -4f;
    public bool isGrounded;
    [Header("Climbing")]
    [SerializeField] private float stepHeight = 0.23f;
    [SerializeField] private float stepCheckOffset = 0.03f;

    private Transform groundCheck;
    private PlayerInputHandler playerInputHandler;
    private Rigidbody playerRidgitbody;
    private Camera cameraMain;
    private Vector3 moveDirection;
    private float moveAmount;
    private AnimatorController animatorController;
    private Vector3 moveVelocity;
    private bool isJumping;
    private float xMove;
    private float yMove;
    private Vector3 targetDirection;

    private void Start()
    {
        groundCheck = GetComponentsInChildren<Transform>()[1];
        animatorController = GetComponent<AnimatorController>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerRidgitbody = GetComponent<Rigidbody>();
        cameraMain = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        playerInputHandler.UpdateInputs();
    }

    private void FixedUpdate()
    {
        Ground();
    }

    private void Ground()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //check if the player grounded
        if (isGrounded)
        {
            if (playerInputHandler.jump && !isJumping) //jumping
            {
                Jump();
            }
            else //movement
            {
                Move();
            }
        }
        else //falling
        {
            Fall();
        }
    }

    private void Jump()
    {
        isJumping = true;
        moveVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        playerRidgitbody.velocity = moveVelocity;
        animatorController.JumpingAnimation(true);
    }

    public void Move()
    {
        isJumping = false;
        playerInputHandler.jump = false;
        if (animatorController.animator.GetBool("Jump"))
        {
            animatorController.JumpingAnimation(false);
        }
        if (animatorController.animator.GetBool("Fall"))
        {
            animatorController.FallingAnimation(false);
        }

        xMove = playerInputHandler.xInput;
        yMove = playerInputHandler.yInput;
        if (xMove < 0.1f && xMove > -0.1f && yMove < 0.1f && yMove > -0.1f)
        {
            if (animatorController.animator.GetFloat("yMove") > 0)
            {
                playerRidgitbody.velocity = Vector3.zero;
                animatorController.MovementAnimation(0, 0);

            }
        }
        else
        {
            if (yMove > 0.1)
            {
                moveDirection = transform.forward * yMove;
            }
            else if (yMove < -0.1)
            {
                moveDirection -= transform.forward * yMove;
            }
            moveDirection = moveDirection + cameraMain.transform.right * xMove;
            moveDirection.Normalize();
            if (playerInputHandler.run)
            {
                moveDirection = moveDirection * runSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkSpeed;
            }
            moveVelocity = moveDirection;
            playerRidgitbody.velocity = moveVelocity;
            moveAmount = Mathf.Clamp01(Mathf.Abs(xMove) + Mathf.Abs(yMove));           
            animatorController.MovementAnimation(0, moveAmount);
            Stairs();
            Rotation();

        }
    }

    private void Stairs()
    {
        //Check if players feet is in front of a possible step
        Vector3 startRayPosition = new Vector3(groundCheck.position.x, groundCheck.position.y, groundCheck.position.z);
        //startRayPosition.y /= 3f; 
        //Debug.DrawRay(startRayPosition, transform.forward, Color.green);
        if (Physics.Raycast(startRayPosition, transform.forward, 0.3f, groundMask))
        {
            //Check if there is an open space above the step height?
            startRayPosition.y = groundCheck.position.y + stepHeight - stepCheckOffset;
            //Debug.DrawRay(startRayPosition, transform.forward, Color.red, 1f);
            if (!Physics.Raycast(startRayPosition, transform.forward, 0.32f, groundMask))
            {
                //Debug.DrawRay(startRayPosition, transform.forward, Color.cyan, 1f);
                moveVelocity.y = Mathf.Sqrt(0.2f * -(stepHeight / 0.2f) * gravity);
                playerRidgitbody.velocity = moveVelocity;
            }
        }
    }

    private void Fall()
    {
        if (!playerInputHandler.jump && !animatorController.animator.GetBool("Fall") && playerRidgitbody.velocity.y < fallSensitivity)
        {
            animatorController.FallingAnimation(true);
        }
    }

    public void Rotation()
    {
        targetDirection = cameraMain.transform.forward * yMove;
        targetDirection = targetDirection + cameraMain.transform.right * xMove;
        targetDirection.Normalize();
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotatespeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }
}
