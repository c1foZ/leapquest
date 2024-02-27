using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private Vector2 pointA;
    private Vector2 pointB;
    private readonly float moveSpeed = 0.5f;
    private Vector2 offset = new(3f, 0f);

    [SerializeField] private GameObject projectilePrefab;
    private readonly float fireHeightOffset = -0.2f;
    private readonly float fireInterval = 1f;
    private float nextFireTime;

    private Vector2 facingDirection;

    private void Start()
    {
        pointA = (Vector2)transform.position + offset;
        pointB = (Vector2)transform.position - offset;
        nextFireTime = Time.time + fireInterval;

        facingDirection = Vector2.right;
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
        Vector2 firePoint = new(transform.position.x, transform.position.y + fireHeightOffset);
        GameObject projectile = Instantiate(projectilePrefab, firePoint, Quaternion.identity);

        if (projectile.TryGetComponent<Projectile>(out var projectileScript))
        {
            projectileScript.SetDirection(facingDirection);
        }
    }
}
