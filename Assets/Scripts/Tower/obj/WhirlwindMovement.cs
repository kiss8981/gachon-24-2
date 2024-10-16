using UnityEngine;

public class WhirlwindMovement : MonoBehaviour
{
    private Enemy targetEnemy;
    private float speed;
    private float duration;

    public void Initialize(Enemy enemy, float moveSpeed, float lifetime)
    {
        targetEnemy = enemy;
        speed = moveSpeed;
        duration = lifetime;
    }

    private void Update()
    {
        if (targetEnemy == null)
            return;

        Vector2 direction = (targetEnemy.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Floating(duration);

            Destroy(gameObject);
        }
    }
}
