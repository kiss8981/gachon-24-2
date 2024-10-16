using UnityEngine;

public class ElectrifiedState : NormalState
{
    private float shockDuration;
    private float shockDamage;
    private GameObject shockEffectInstance;
    private Enemy enemy;

    private GameObject shockEffectPrefab;

    public ElectrifiedState(float duration, float damage, GameObject shockEffectPrefab)
    {
        shockDuration = duration;
        shockDamage = damage;
        this.shockEffectPrefab = shockEffectPrefab;
    }

    public override void Initialize(Enemy enemy)
    {
        base.Initialize(enemy);
        this.enemy = enemy;

        if (shockEffectPrefab != null)
        {
            shockEffectInstance = GameObject.Instantiate(
                shockEffectPrefab,
                enemy.transform.position,
                Quaternion.identity
            );
            shockEffectInstance.transform.SetParent(enemy.transform);
        }
    }

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
