using UnityEngine;

public class CharmanderTower : Tower
{
    [SerializeField]
    private float burnDuration = 3.0f;

    [SerializeField]
    private float burnDamagePerSecond = 5.0f;

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
        enemy.Burn(burnDuration, burnDamagePerSecond);
    }
}
