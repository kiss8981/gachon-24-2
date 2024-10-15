using UnityEngine;

public class KirbyTower : Tower
{
    public GameObject whirlwindPrefab; // Prefab of the whirlwind/typhoon
    public Transform spawnPoint; // Point where the whirlwind is created
    public float whirlwindDuration = 5f; // How long the whirlwind lasts

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
        // Instantiate the whirlwind at the specified spawn point
        GameObject whirlwind = Instantiate(
            whirlwindPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        // Set the whirlwind's duration
        Whirlwind whirlwindScript = whirlwind.GetComponent<Whirlwind>();
        if (whirlwindScript != null)
        {
            whirlwindScript.SetDuration(whirlwindDuration);
        }
    }

    public override void Attack(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }
}
