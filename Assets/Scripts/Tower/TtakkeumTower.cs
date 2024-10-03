public class TtakkeumTower : Tower
{
    private float poisonDuration = 2.0f;
    private int poisonDamage = 10;

    public override void Attack(Enemy enemy)
    {
        enemy.Poison(poisonDuration, poisonDamage);
    }
}
