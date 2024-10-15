using UnityEngine;

public class HanjoTower : Tower
{
    public GameObject arrowPrefab; // Arrow projectile prefab
    public float projectileSpeed = 10f; // Speed of the arrow

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
                Attack(nearestEnemy);
                lastAttackTime = Time.time;
            }
        }
    }

    public override void Attack(Enemy enemy)
    {
        // Instantiate an arrow projectile
        GameObject arrow = Instantiate(
            arrowPrefab,
            base.gameObject.transform.position,
            Quaternion.identity
        );

        // Get the Arrow component and set its target and speed
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.SetTarget(enemy);
            arrowScript.SetSpeed(projectileSpeed);
        }
    }
}
