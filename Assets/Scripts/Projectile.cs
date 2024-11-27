using UnityEngine;

public class Projectile : MonoBehaviour
{
    private readonly float speed = 5f;
    private Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        rb.velocity = direction * speed;
    }

    private void Update()
    {
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyPlayer(collision.gameObject);
            Destroy(gameObject);
            GameManager.instance.RestartGame();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); 
        }
    }

    private void DestroyPlayer(GameObject player)
    {
        Destroy(player);
    }
}
