using UnityEngine;

public class PoisonedState : NormalState
{
    private float poisonDuration; // 지속 시간
    private float poisonDamage; // 독 데미지
    private GameObject poisonEffectInstance; // 독 효과 오브젝트
    private Enemy enemy;

    private GameObject poisonEffectPrefab; // 독 효과 프리팹

    // 생성자에서 독 지속 시간, 독 데미지, 독 효과 프리팹을 전달받음
    public PoisonedState(float duration, float damage, GameObject poisonEffectPrefab)
    {
        poisonDuration = duration;
        poisonDamage = damage;
        this.poisonEffectPrefab = poisonEffectPrefab;
    }

    // 상태 초기화 메서드
    public override void Initialize(Enemy enemy)
    {
        base.Initialize(enemy); // 부모 클래스의 초기화 메서드 호출
        this.enemy = enemy;

        // 적에게 독 효과 프리팹을 적용
        if (poisonEffectPrefab != null)
        {
            poisonEffectInstance = GameObject.Instantiate(
                poisonEffectPrefab,
                enemy.transform.position,
                Quaternion.identity
            );
            poisonEffectInstance.transform.SetParent(enemy.transform); // 적을 따라다니도록 함
        }
    }

    // 상태 실행 메서드
    public override void Execute(Enemy enemy)
    {
        base.Execute(enemy);

        // 독 지속 시간을 감소시킴
        poisonDuration -= Time.deltaTime;

        // 독 데미지를 지속적으로 적에게 가함
        enemy.TakeDamage(Mathf.CeilToInt(poisonDamage * Time.deltaTime));

        // 독 지속 시간이 끝나면 독 효과를 제거하고 상태를 NormalState로 전환
        if (poisonDuration <= 0)
        {
            if (poisonEffectInstance != null)
            {
                GameObject.Destroy(poisonEffectInstance); // 독 효과 제거
            }
            enemy.SetState(new NormalState()); // 상태를 NormalState로 전환
        }
    }
}
