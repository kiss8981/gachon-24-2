using UnityEngine;

public class PoisonedState : NormalState
{
    private float poisonDuration;
    private float poisonDamage;
    private GameObject poisonEffectInstance;
    private Enemy enemy;

    private GameObject poisonEffectPrefab;

    public PoisonedState(float duration, float damage, GameObject poisonEffectPrefab)
    {
        poisonDuration = duration;
        poisonDamage = damage;
        this.poisonEffectPrefab = poisonEffectPrefab;
    }

    public override void Initialize(Enemy enemy)
    {
        base.Initialize(enemy);
        this.enemy = enemy;

        if (poisonEffectPrefab != null)
        {
            poisonEffectInstance = GameObject.Instantiate(
                poisonEffectPrefab,
                enemy.transform.position,
                Quaternion.identity
            );
            poisonEffectInstance.transform.SetParent(enemy.transform);
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
