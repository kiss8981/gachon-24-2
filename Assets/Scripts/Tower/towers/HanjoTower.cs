using UnityEngine;

public class HanjoTower : Tower
{
    public GameObject arrowPrefab;
    public float projectileSpeed = 10f;

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
        GameObject arrow = Instantiate(
            arrowPrefab,
            base.gameObject.transform.position,
            Quaternion.identity
        );

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.SetTarget(enemy);
            arrowScript.SetSpeed(projectileSpeed);
        }
    }
}
