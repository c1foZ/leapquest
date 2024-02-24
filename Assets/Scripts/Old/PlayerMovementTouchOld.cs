using UnityEngine;

public class PlayerMovementTouchOld : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleTouchInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            HandleTouchMovement(touch);

            if (IsTouchInMiddleBottom(touch) && IsGrounded())
            {
                Jump();
            }

            HandleTouchEndState(touch);
        }
        else if (Input.GetMouseButton(0))
        {
            HandleMouseMovement();

            if (IsMouseInMiddleBottom() && IsGrounded())
            {
                Jump();
            }
        }
        else
        {
            StopMovement();
        }
    }

    void HandleTouchMovement(Touch touch)
    {
        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            Move(touch.position.x < Screen.width / 2 ? -1 : 1);
        }
    }

    void HandleTouchEndState(Touch touch)
    {
        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            StopMovement();
        }
    }

    bool IsTouchInMiddleBottom(Touch touch)
    {
        return touch.position.y < Screen.height / 4 && touch.position.x > Screen.width / 4 && touch.position.x < 3 * Screen.width / 4;
    }

    void HandleMouseMovement()
    {
        Move(Input.mousePosition.x < Screen.width / 2 ? -1 : 1);
    }

    bool IsMouseInMiddleBottom()
    {
        return Input.mousePosition.y < Screen.height / 4 && Input.mousePosition.x > Screen.width / 4 && Input.mousePosition.x < 3 * Screen.width / 4;
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void StopMovement()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void Move(float direction)
    {
        Vector2 movement = new Vector2(direction * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    bool IsGrounded()
    {
        float raycastLength = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength);

        return hit.collider != null;
    }
}
