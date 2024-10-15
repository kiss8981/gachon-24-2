using UnityEngine;

public class XerathTower : Tower
{
    public float shockDuration = 3f; // 감전 지속 시간
    public float shockDamage = 5f; // 감전 데미지
    public GameObject shockEffectPrefab; // 전기 이펙트 프리팹

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
        enemy.TakeDamage(damage);

        ElectrifiedState electrifiedState = new ElectrifiedState(
            shockDuration,
            shockDamage,
            shockEffectPrefab
        );
        enemy.SetState(electrifiedState);
    }
}
