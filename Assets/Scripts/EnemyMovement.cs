using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private Vector2 offset = new Vector2(3f, 0f);

    private Vector2 pointA;
    private Vector2 pointB;

    [SerializeField] private GameObject projectilePrefab;
    private readonly float fireInterval = 1f; // Fire every second
    private float nextFireTime;

    private Vector2 facingDirection; // Store the facing direction

    private void Start()
    {
        pointA = (Vector2)transform.position + offset;
        pointB = (Vector2)transform.position - offset;
        nextFireTime = Time.time + fireInterval; // Initial fire time

        facingDirection = Vector2.right; // Initial facing direction
    }

    private void Update()
    {
        MoveBetweenPoints();

        // Check if it's time to fire
        if (Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireInterval; // Set next fire time
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

        // Update the facing direction based on the flip
        facingDirection = isFacingRight ? Vector2.right : Vector2.left;
    }

    private void FireProjectile()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set the direction of the projectile based on the enemy's facing direction
        if (projectile.TryGetComponent<Projectile>(out var projectileScript))
        {
            projectileScript.SetDirection(facingDirection); // Use the facing direction
        }
    }
}
