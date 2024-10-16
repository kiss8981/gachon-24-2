using UnityEngine;

public class MalphiteTower : Tower
{
    public float effectDuration = 0.5f;
    public GameObject groundEffectPrefab;
    public float attackCooldown;

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
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in allEnemies)
        {
            enemy.TakeDamage(damage);
        }

        SpawnGroundEffect();
    }

    private void SpawnGroundEffect()
    {
        GameObject effectInstance = Instantiate(
            groundEffectPrefab,
            transform.position,
            Quaternion.identity
        );

        Destroy(effectInstance, effectDuration);
    }

    public override void Attack(Enemy enemy) { }
}
