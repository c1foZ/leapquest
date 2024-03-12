using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    private Vector2 pointA;
    private Vector2 pointB;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private Vector2 offset = new(3f, 0f);

    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireHeightOffset = -0.2f;
    [SerializeField] private float fireInterval = 1f;
    private float nextFireTime;

    private Vector2 facingDirection;
    private GameManager gameManager;

    [SerializeField] private Animator animator;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager not found in the scene.");
        }

        SetPoints();
        nextFireTime = Time.time + fireInterval;
        facingDirection = Vector2.right;
    }

    private void SetPoints()
    {
        Vector2 currentPosition = (Vector2)transform.position;
        pointA = currentPosition + offset;
        pointB = currentPosition - offset;
    }

    private void Update()
    {
        MoveBetweenPoints();

        if (Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireInterval;
        }
    }

    private void MoveBetweenPoints()
    {
        float pingPongValue = Mathf.PingPong(Time.time * moveSpeed, 1f);
        Vector2 newPosition = Vector2.Lerp(pointA, pointB, pingPongValue);
        transform.position = newPosition;

        if (pingPongValue < 0.01f)
        {
            FlipCharacter(false);
        }
        else if (pingPongValue > 0.99f)
        {
            FlipCharacter(true);
        }
    }

    private void FlipCharacter(bool isFacingRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = isFacingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;

        facingDirection = isFacingRight ? Vector2.right : Vector2.left;
    }

    private void FireProjectile()
    {
        Vector2 firePoint = new Vector2(transform.position.x, transform.position.y + fireHeightOffset);
        GameObject projectile = Instantiate(projectilePrefab, firePoint, Quaternion.identity);

        if (projectile.TryGetComponent<Projectile>(out var projectileScript))
        {
            projectileScript.SetDirection(facingDirection);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerCollision(collision);
        }
    }

    private void HandlePlayerCollision(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.GetContact(0).point.y > transform.position.y &&
            collision.relativeVelocity.y < 0)
        {
            rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
            KillEnemy();
            gameManager.IncreaseScore(3);
        }
    }

    private void KillEnemy()
    {
        animator.SetBool("isKilled", true);
        Destroy(gameObject, 0.4f);
    }
}
