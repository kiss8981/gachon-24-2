using UnityEngine;

public class FloatingState : IEnemyState
{
    private float floatingTime;
    private SpriteRenderer spriteRenderer;

    public FloatingState(float time)
    {
        floatingTime = time;
    }

    public void Initialize(Enemy enemy)
    {
        spriteRenderer = enemy.gameObject.GetComponent<SpriteRenderer>();
    }

    public void Execute(Enemy enemy)
    {
        floatingTime -= Time.deltaTime;
        spriteRenderer.color = Color.grey;

        if (floatingTime <= 0)
        {
            enemy.SetState(new NormalState());
            spriteRenderer.color = Color.white;
        }
    }
}
