using UnityEngine;

public class KirbyTower : Tower
{
    public GameObject whirlwindPrefab;
    public Transform spawnPoint;
    public float whirlwindDuration = 5f;

    [SerializeField]
    private float lastAttackTime = 0f;

    [SerializeField]
    private float attackCooldown;

    private void Update()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Enemy nearestEnemy = base.FindNearestEnemy();
            if (nearestEnemy != null)
            {
                spawnPoint = nearestEnemy.transform;
                SpawnWhirlwind();
                lastAttackTime = Time.time;
            }
        }
    }

    private void SpawnWhirlwind()
    {
        GameObject whirlwind = Instantiate(
            whirlwindPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        Whirlwind whirlwindScript = whirlwind.GetComponent<Whirlwind>();
        if (whirlwindScript != null)
        {
            whirlwindScript.SetDuration(whirlwindDuration);
        }
    }

    public override void Attack(Enemy enemy) { }
}
