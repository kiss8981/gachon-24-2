using UnityEngine;

public class ElsaTower : Tower
{
    [SerializeField]
    private float freezeDuration = 3.0f;
    [SerializeField]
    private float lastAttackTime = 0f;  // 마지막 공격 시간
    [SerializeField]
    private float attackCooldown;       // 공격 대기시간


    private void Update()
    {
        // 쿨타임이 끝났다면 공격 가능
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // 가장 가까운 적을 찾아서 공격
            Enemy nearestEnemy = base.FindNearestEnemy();
            if (nearestEnemy != null)
            {
                Attack(nearestEnemy);
                lastAttackTime = Time.time;  // 공격한 시간 기록
            }
        }
    }

    public override void Attack(Enemy enemy)
    {
        // 적을 얼리는 특수 로직
        enemy.Freeze(freezeDuration);
    }


}
