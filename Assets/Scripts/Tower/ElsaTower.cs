public class ElsaTower : Tower
{
    private float freezeDuration = 3.0f;

    public override void Attack(Enemy enemy)
    {
        // 적을 얼리는 로직 구현
        enemy.Freeze(freezeDuration);
    }
}
