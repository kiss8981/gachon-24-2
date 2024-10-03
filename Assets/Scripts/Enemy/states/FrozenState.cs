using UnityEngine;

public class FrozenState : IEnemyState
{
    private float freezeTime;

    public FrozenState(float duration)
    {
        freezeTime = duration;
    }

    public void Initialize(Enemy enemy) { }

    public void Execute(Enemy enemy)
    {
        freezeTime -= Time.deltaTime;
        if (freezeTime <= 0)
        {
            enemy.SetState(new NormalState());
        }
    }
}
