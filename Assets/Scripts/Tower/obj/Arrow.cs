using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Enemy target;
    private float speed;
    public float damage = 10f; // Arrow damage

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
            Destroy(gameObject); // Destroy the arrow if the target is lost
            return;
        }

        // Calculate the direction to the target
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float distanceThisFrame = speed * Time.deltaTime;

        // Rotate the arrow to face the target
        RotateTowardsTarget(direction);

        // Check if the arrow reaches the target
        if (Vector3.Distance(transform.position, target.transform.position) <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Move the arrow in the direction of the target
        transform.Translate(direction * distanceThisFrame, Space.World);
    }

    private void RotateTowardsTarget(Vector3 direction)
    {
        // Calculate the angle in radians and convert it to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the arrow
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void HitTarget()
    {
        target.TakeDamage(damage);
        Destroy(gameObject); // Destroy the arrow after hitting the target
    }
}
