using UnityEngine;

public class YasuoTower : Tower
{
    public GameObject whirlwindPrefab;
    public Transform spawnPoint;
    public float whirlwindSpeed = 5f;
    public float whirlwindDuration = 5f;

    [SerializeField]
    private float lastAttackTime = 0f;

    [SerializeField]
    private float attackCooldown;

    private void Update()
    {
        spawnPoint = gameObject.transform;
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Enemy nearestEnemy = base.FindNearestEnemy();
            if (nearestEnemy != null)
            {
                SpawnWhirlwind(nearestEnemy);
                lastAttackTime = Time.time;
            }
        }
    }

    private void SpawnWhirlwind(Enemy targetEnemy)
    {
        GameObject whirlwind = Instantiate(
            whirlwindPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        WhirlwindMovement whirlwindScript = whirlwind.GetComponent<WhirlwindMovement>();
        if (whirlwindScript != null)
        {
            whirlwindScript.Initialize(targetEnemy, whirlwindSpeed, whirlwindDuration);
        }
    }

    public override void Attack(Enemy enemy) { }
}
