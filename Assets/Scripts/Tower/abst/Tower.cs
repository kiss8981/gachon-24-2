using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public float attackSpeed;
    public int damage;
    public float range;

    public enum TargetingMode
    {
        Nearest,
        Farthest,
        Strongest,
        Weakest
    }

    public TargetingMode targetingMode = TargetingMode.Nearest;

    public abstract void Attack(Enemy enemy);

    public float GetCooldownTime()
    {
        return 1f / attackSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public virtual Enemy FindNearestEnemy()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        Enemy nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();

            if (enemy != null && enemy.state == EnemyState.NormalState)
            {
                float distanceToEnemy = Vector2.Distance(
                    transform.position,
                    enemy.transform.position
                );

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }
}
