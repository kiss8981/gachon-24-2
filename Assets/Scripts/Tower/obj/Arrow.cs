using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Enemy target;
    private float speed;
    public float damage = 10f;

    public void SetTarget(Enemy enemyTarget)
    {
        target = enemyTarget;
    }

    public void SetSpeed(float projectileSpeed)
    {
        speed = projectileSpeed;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.transform.position - transform.position).normalized;
        float distanceThisFrame = speed * Time.deltaTime;

        RotateTowardsTarget(direction);

        if (Vector3.Distance(transform.position, target.transform.position) <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction * distanceThisFrame, Space.World);
    }

    private void RotateTowardsTarget(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void HitTarget()
    {
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
