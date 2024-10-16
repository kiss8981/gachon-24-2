using UnityEngine;
using System.Collections;

public class Whirlwind : MonoBehaviour
{
    public float damagePerSecond = 10f;
    public float radius = 3f;
    private float duration;

    private void Start()
    {
        StartCoroutine(DestroyAfterDuration());
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    private void Update()
    {
        DealDamageToEnemies();
    }

    private void DealDamageToEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in hitEnemies)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    private IEnumerator DestroyAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
