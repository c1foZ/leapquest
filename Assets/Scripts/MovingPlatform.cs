using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 offsetA = new(-3f, 0f);
    [SerializeField] private Vector2 offsetB = new(3f, 0f);
    [SerializeField] private float moveSpeed = 3f;

    private Vector2 pointA;
    private Vector2 pointB;

    private bool isMoving;

    private void Start()
    {
        pointA = (Vector2)transform.position + offsetA;
        pointB = (Vector2)transform.position + offsetB;
    }

    private void Update()
    {
        if (isMoving)
        {
            StartCoroutine(MovePlatform());
            isMoving = false;
        }
    }

    public void StartMoving()
    {
        if (!isMoving)
        {
            StartCoroutine(MovePlatform());
        }
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {
            yield return MoveToPoint(pointB);
            yield return MoveToPoint(pointA);
        }
    }

    private IEnumerator MoveToPoint(Vector2 targetPoint)
    {
        while (Vector2.Distance(transform.position, targetPoint) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent<PlayerMovement>(out var platformMovement))
        {
            platformMovement.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.TryGetComponent<PlayerMovement>(out var platformMovement))
        {
            platformMovement.ResetParent();
        }
    }
}