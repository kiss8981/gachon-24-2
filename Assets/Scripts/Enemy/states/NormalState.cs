using UnityEngine;

public class NormalState : IEnemyState
{
    private Transform[] wayPoints;
    private Rigidbody2D rb;

    public virtual void Initialize(Enemy enemy)
    {
        wayPoints = MapManager.Instance.wayPoints;
        rb = enemy.GetComponent<Rigidbody2D>();
    }

    public virtual void Execute(Enemy enemy)
    {
        if (wayPoints != null && wayPoints.Length > 0)
        {
            Vector2 targetPosition;

            if (enemy.target < wayPoints.Length)
            {
                targetPosition = wayPoints[enemy.target].position;
            }
            else
            {
                targetPosition = MapManager.Instance.endPoint.transform.position;
            }

            Vector2 newPosition = Vector2.MoveTowards(
                rb.position,
                targetPosition,
                enemy.speed * Time.fixedDeltaTime
            );
            rb.MovePosition(newPosition);

            if (Vector2.Distance(enemy.transform.position, targetPosition) < 0.1f)
            {
                enemy.target++;
            }
        }
    }
}
