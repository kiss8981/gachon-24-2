using UnityEngine;

public class PoisonedState : IEnemyState
{
    private float poisonDuration;
    private int poisonDamage;

    public PoisonedState(float duration, int damage)
    {
        poisonDuration = duration;
        poisonDamage = damage;
    }

    public void Initialize(Enemy enemy) { }

    public void Execute(Enemy enemy)
    {
        // 중독 상태에서 지속적으로 체력을 감소시킴
        poisonDuration -= Time.deltaTime;
        enemy.TakeDamage(Mathf.CeilToInt(poisonDamage * Time.deltaTime));

        if (poisonDuration <= 0)
        {
            // 중독 시간이 끝나면 다시 정상 상태로 변경
            enemy.SetState(new NormalState());
        }
    }
}
