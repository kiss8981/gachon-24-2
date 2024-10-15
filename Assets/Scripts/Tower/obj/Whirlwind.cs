using UnityEngine;
using System.Collections;

public class Whirlwind : MonoBehaviour
{
    public float damagePerSecond = 10f; // Damage dealt per second to enemies inside the whirlwind
    public float radius = 3f; // Radius of the whirlwind's effect
    private float duration; // How long the whirlwind lasts

    private void Start()
    {
        // Destroy the whirlwind after its duration ends
        StartCoroutine(DestroyAfterDuration());
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    private void Update()
    {
        // Continuously apply damage to enemies within the whirlwind's radius
        DealDamageToEnemies();
    }

    private void DealDamageToEnemies()
    {
        // Find all enemies within the whirlwind's radius
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in hitEnemies)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Deal damage per second to each enemy
                enemy.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    private IEnumerator DestroyAfterDuration()
    {
        // Wait for the duration of the whirlwind, then destroy it
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Optional: Draw the radius in the editor for visualization
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
