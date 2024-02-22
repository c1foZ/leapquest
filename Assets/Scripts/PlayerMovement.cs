using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;

        if (isGrounded && Input.GetKey(KeyCode.W))
        {
            Jump();
        }
        
         if (transform.position.y < -5f)
        {
            gameManager.RestartGame();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
}
