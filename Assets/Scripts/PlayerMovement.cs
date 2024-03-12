using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb;
    private PlayerInputActions playerInputActions;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float ladderClimbSpeed = 3f;
    [SerializeField] private bool isInLadder;

    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask groundLayer;
    private const string GroundLayerName = "Ground";
    [SerializeField] private float rayDistance = 0.47f;
    [SerializeField] private float groundThreshold = -5f;
    private bool isGrounded;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 6f;
    private bool isJumping;

    [Header("Facing Direction")]
    private bool isFacingRight;
    private Transform originalParent;

    [Header("Animations")]
    [SerializeField] private Animator animator;
    private void Awake()
    {
        InitializeComponents();
        SetupInputActions();
    }

    private void InitializeComponents()
    {
        gameManager = GameManager.instance;
        rb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask(GroundLayerName);
        originalParent = transform.parent;
    }

    private void SetupInputActions()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Jump.started += OnJumpStart;
        playerInputActions.Player.Jump.canceled += OnJumpEnd;
    }

    private void FixedUpdate()
    {
        if (isInLadder)
        {
            ClimbLadder();
        }
        else
        {
            MovePlayer();
            CheckGround();
            HandleJump();
            RestartIfBelowGround();
        }
    }

    public void SetParent(Transform newParent)
    {
        originalParent = transform.parent;
        transform.parent = newParent;
    }

    public void ResetParent()
    {
        transform.parent = originalParent;
    }

    private void ClimbLadder()
    {
        Vector2 inputMove = playerInputActions.Player.Movement.ReadValue<Vector2>();
        rb.velocity = new Vector2(inputMove.x * moveSpeed, inputMove.y * ladderClimbSpeed);

        if (inputMove.y == 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    public void EnterLadder()
    {
        isInLadder = true;
        rb.gravityScale = 0f;
    }

    public void ExitLadder()
    {
        isInLadder = false;
        rb.gravityScale = 1f;
    }

    private void MovePlayer()
    {
        Vector2 inputMove = playerInputActions.Player.Movement.ReadValue<Vector2>();
        rb.velocity = new Vector2(inputMove.x * moveSpeed, rb.velocity.y);
        FlipCharacter(inputMove.x);
        animator.SetFloat("Speed", Math.Abs(inputMove.x));
    }

    private void HandleJump()
    {
        if (isJumping && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);
        animator.SetBool("isJumping", !isGrounded);
    }

    private void RestartIfBelowGround()
    {
        if (transform.position.y < groundThreshold && gameManager != null)
        {
            Debug.Log("Restarting game...");
            gameManager.RestartGame();
        }
    }

    private void OnJumpStart(InputAction.CallbackContext context)
    {
        isJumping = true;
    }

    private void OnJumpEnd(InputAction.CallbackContext context)
    {
        isJumping = false;
    }

    private void FlipCharacter(float horizontalInput)
    {
        if ((horizontalInput < 0 && !isFacingRight) || (horizontalInput > 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
