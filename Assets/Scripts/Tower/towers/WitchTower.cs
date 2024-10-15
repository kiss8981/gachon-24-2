using UnityEngine;

public class WitchTower : Tower
{
    [SerializeField]
    private float poisonDuration = 3.0f;

    [SerializeField]
    private float poisonDamagePerSecond = 5.0f;

    [SerializeField]
    private float lastAttackTime = 0f;

    [SerializeField]
    private float attackCooldown;

    private void Update()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Enemy nearestEnemy = base.FindNearestEnemy();
            if (nearestEnemy != null)
            {
                Attack(nearestEnemy);
                lastAttackTime = Time.time;
            }
        }
    }

    public override void Attack(Enemy enemy)
    {
        enemy.Poison(poisonDuration, poisonDamagePerSecond);
    }
}
