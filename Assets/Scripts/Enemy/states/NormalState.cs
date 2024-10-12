using UnityEngine;

public class NormalState : IEnemyState
{
    private Transform[] wayPoints;
    private Rigidbody2D rb; // Rigidbody2D 변수 추가

    // 초기화 메서드
    public virtual void Initialize(Enemy enemy)
    {
        wayPoints = MapManager.Instance.wayPoints;
        rb = enemy.GetComponent<Rigidbody2D>(); // Rigidbody2D 가져오기
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
                targetPosition = MapManager.Instance.endPoint.transform.position; // 목표 지점으로 설정
            }

            Vector2 newPosition = Vector2.MoveTowards(
                rb.position,
                targetPosition,
                enemy.speed * Time.fixedDeltaTime
            );
            rb.MovePosition(newPosition);

            if (Vector2.Distance(enemy.transform.position, targetPosition) < 0.1f)
            {
                enemy.target++; // 다음 목표 지점으로 이동
            }
        }
    }
}
