using UnityEngine;

public class MalphiteTower : Tower
{
    public float effectDuration = 0.5f; // Duration of the ground effect
    public GameObject groundEffectPrefab; // Prefab for the ground effect
    public float attackCooldown; // Cooldown for the tower's attacks

    [SerializeField]
    private float lastAttackTime = 0f;

    private void Update()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            AttackAllEnemies();
            lastAttackTime = Time.time;
        }
    }

    private void AttackAllEnemies()
    {
        // Find all enemies
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        // Deal damage to all enemies
        foreach (Enemy enemy in allEnemies)
        {
            enemy.TakeDamage(damage);
        }

        // Spawn ground effect at the tower's position
        SpawnGroundEffect();
    }

    private void SpawnGroundEffect()
    {
        // Instantiate the ground effect at the tower's position
        GameObject effectInstance = Instantiate(
            groundEffectPrefab,
            transform.position,
            Quaternion.identity
        );

        // Optionally, destroy the effect after a set duration to avoid clutter
        Destroy(effectInstance, effectDuration);
    }

    public override void Attack(Enemy enemy) { }
}
