using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public float attackSpeed;
    public int damage;
    public float range;

    public abstract void Attack(Enemy enemy);
}
