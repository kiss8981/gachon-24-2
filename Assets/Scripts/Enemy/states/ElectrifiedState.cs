using UnityEngine;

public class ElectrifiedState : NormalState
{
    private float shockDuration; // 감전 지속 시간
    private float shockDamage; // 초당 전기 데미지
    private GameObject shockEffectInstance; // 전기 이펙트 인스턴스
    private Enemy enemy;

    private GameObject shockEffectPrefab; // 전기 효과 프리팹

    // 생성자에서 전기 지속 시간, 데미지, 전기 이펙트를 받음
    public ElectrifiedState(float duration, float damage, GameObject shockEffectPrefab)
    {
        shockDuration = duration;
        shockDamage = damage;
        this.shockEffectPrefab = shockEffectPrefab;
    }

    // 상태 초기화
    public override void Initialize(Enemy enemy)
    {
        base.Initialize(enemy); // 부모 클래스 초기화 호출
        this.enemy = enemy;

        // 전기 효과를 적에게 생성
        if (shockEffectPrefab != null)
        {
            shockEffectInstance = GameObject.Instantiate(
                shockEffectPrefab,
                enemy.transform.position,
                Quaternion.identity
            );
            shockEffectInstance.transform.SetParent(enemy.transform); // 전기 효과를 적에게 붙임
        }
    }

    // 상태 실행
    public override void Execute(Enemy enemy)
    {
        base.Execute(enemy);

        shockDuration -= Time.deltaTime;

        enemy.TakeDamage(shockDamage * Time.deltaTime);

        if (shockDuration <= 0)
        {
            if (shockEffectInstance != null)
            {
                GameObject.Destroy(shockEffectInstance);
            }
            else
            {
                enemy.SetState(new NormalState());
            }
        }
    }
}
