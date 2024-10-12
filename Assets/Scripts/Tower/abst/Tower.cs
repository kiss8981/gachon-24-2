using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    // 모든 타워의 공통 속성
    public float attackSpeed;  // 초당 공격 횟수
    public int damage;         // 공격력
    public float range;        // 공격 범위

    // 타워가 공격할 적을 선택하는 방식에 대한 설정
    public enum TargetingMode { Nearest, Farthest, Strongest, Weakest }
    public TargetingMode targetingMode = TargetingMode.Nearest; // 기본값: 가장 가까운 적

    // 타워가 적을 공격하는 추상 메서드 (구체적 로직은 자식 클래스에서 구현)
    public abstract void Attack(Enemy enemy);

    // 타워의 공격 대기시간을 계산하는 함수
    public float GetCooldownTime()
    {
        return 1f / attackSpeed;  // 공격 속도에 따른 쿨다운 계산
    }

    // 타워의 공격 범위를 시각적으로 표시 (디버깅용)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public virtual Enemy FindNearestEnemy()
    {
        // 타워의 2D 공격 범위 내에 있는 모든 적을 찾음
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        Enemy nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;  // 무한대로 초기화

        // 범위 내 콜라이더를 순회하며 적을 찾음
        foreach (Collider2D hitCollider in hitColliders)
        {
            // 적 오브젝트에 연결된 Enemy 컴포넌트를 가져옴
            Enemy enemy = hitCollider.GetComponent<Enemy>();

            // 적 오브젝트가 맞는지 확인
            if (enemy != null)
            {
                // 2D에서 타워와 적 사이의 거리를 계산 (Vector2 사용)
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                // 가장 가까운 적을 찾음
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;  // 가장 가까운 적 반환 (없으면 null)
    }
}
