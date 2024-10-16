using UnityEngine;

public class ElsaTower : Tower
{
    [SerializeField]
    private float freezeDuration = 3.0f;

    [SerializeField]
    private float lastAttackTime = 0f;
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
        enemy.Freeze(freezeDuration);
    }
}
