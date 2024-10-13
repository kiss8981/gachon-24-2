using UnityEngine;

public class FrozenState : IEnemyState
{
    private float freezeTime;
    private SpriteRenderer spriteRenderer;

    public FrozenState(float duration)
    {
        freezeTime = duration;
    }

    public void Initialize(Enemy enemy)
    {
        spriteRenderer = enemy.gameObject.GetComponent<SpriteRenderer>();
    }

    public void Execute(Enemy enemy)
    {
        freezeTime -= Time.deltaTime;
        spriteRenderer.color = Color.blue;

        if (freezeTime <= 0)
        {
            enemy.SetState(new NormalState());
            spriteRenderer.color = Color.white;
        }
    }
}
