using UnityEngine;

public class InvisibleState : NormalState
{
    private float duration;
    private float elapsedTime = 0f;

    public InvisibleState(float duration)
    {
        this.duration = duration;
    }

    public override void Initialize(Enemy enemy)
    {
        base.Initialize(enemy);
        enemy.isInvisible = true;
        enemy.spriteRenderer.enabled = false; // 은신 상태에서 적을 보이지 않게 함
    }

    public override void Execute(Enemy enemy)
    {
        base.Execute(enemy);
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= duration)
        {
            enemy.isInvisible = false;
            enemy.spriteRenderer.enabled = true; // 은신 시간이 끝나면 다시 보이게 함
            enemy.SetState(new NormalState()); // 다시 일반 상태로 전환
        }
    }
}
