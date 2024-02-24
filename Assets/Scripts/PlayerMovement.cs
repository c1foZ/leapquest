using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;
    private readonly float moveSpeed = 4f;
    private readonly float groundThreshold = -5f;
    private LayerMask groundLayer;
    private const string GroundLayerName = "Ground";
    private readonly float rayDistance = 0.47f;
    private readonly float jumpForce = 6f;

    private Rigidbody2D rb;
    private PlayerInputActions playerInputActions;
    private bool isGrounded;
    private bool isJumping;
    private bool isFacingRight;

    private void Awake()
    {
        gameManager = GameManager.instance;
        rb = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        groundLayer = LayerMask.GetMask(GroundLayerName);

        playerInputActions.Enable();
        playerInputActions.Player.Jump.started += OnJumpStart;
        playerInputActions.Player.Jump.canceled += OnJumpEnd;
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager not found in the scene.");
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        CheckGround();
        HandleJump();
        RestartIfBelowGround();
    }

    private void MovePlayer()
    {
        Vector2 inputMove = playerInputActions.Player.Movement.ReadValue<Vector2>();
        rb.velocity = new Vector2(inputMove.x * moveSpeed, rb.velocity.y);
        FlipCharacter(inputMove.x);
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
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);
    }

    private void RestartIfBelowGround()
    {
        if (transform.position.y < groundThreshold && gameManager != null)
        {
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
