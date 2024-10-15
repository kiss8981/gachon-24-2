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

    public override void Execute(Enemy enemy)
    {
        base.Execute(enemy);
        poisonDuration -= Time.deltaTime;
        enemy.TakeDamage(poisonDamage * Time.deltaTime);

        if (poisonDuration <= 0)
        {
            if (poisonEffectInstance != null)
            {
                GameObject.Destroy(poisonEffectInstance);
            }
            enemy.SetState(new NormalState());
        }
    }
}
