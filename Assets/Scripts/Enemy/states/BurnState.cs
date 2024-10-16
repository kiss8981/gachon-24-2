using UnityEngine;

public class BurnState : NormalState
{
    private float burnDuration;
    private float burnDamage;
    private GameObject fireEffectInstance;
    private Enemy enemy;

    private GameObject fireEffectPrefab;

    public BurnState(float duration, float damage, GameObject fireEffectPrefab)
    {
        burnDuration = duration;
        burnDamage = damage;
        this.fireEffectPrefab = fireEffectPrefab;
    }

    public override void Initialize(Enemy enemy)
    {
        base.Initialize(enemy);
        this.enemy = enemy;

        if (fireEffectPrefab != null)
        {
            fireEffectInstance = GameObject.Instantiate(
                fireEffectPrefab,
                enemy.transform.position,
                Quaternion.identity
            );
            fireEffectInstance.transform.SetParent(enemy.transform);
        }
    }

    public override void Execute(Enemy enemy)
    {
        base.Execute(enemy);

        burnDuration -= Time.deltaTime;

        enemy.TakeDamage(burnDamage * Time.deltaTime);

        if (burnDuration <= 0)
        {
            if (fireEffectInstance != null)
            {
                GameObject.Destroy(fireEffectInstance);
            }
            else
            {
                enemy.SetState(new NormalState());
            }
        }
    }
}
