using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public int target = 0;
    public GameObject healthBar;

    private Collider2D coll;
    private Rigidbody2D rb;

    private float maxHealthBarWidth;
    private float healthBarWidth;

    private IEnemyState currentState;

    void Awake()
    {
        coll = GetComponent<Collider2D>();

        if (coll == null)
        {
            coll = gameObject.AddComponent<Collider2D>();
        }

        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.isKinematic = false;
    }

    void Update()
    {
        healthBarUpdate();
        currentState.Execute(this);
    }

    void Start()
    {
        SetState(new NormalState());

        maxHealthBarWidth = healthBar.transform.localScale.x;
    }

    public void SetState(IEnemyState newState)
    {
        currentState = newState;
        currentState.Initialize(this);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WayPoint"))
        {
            target += 1;
        }
        else if (other.CompareTag("Finish"))
        {
            GameManager.Instance.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    private void healthBarUpdate()
    {
        healthBarWidth = (float)health / 100 * maxHealthBarWidth;

        // 현재 localScale을 수정
        healthBar.transform.localScale = new Vector3(
            healthBarWidth,
            healthBar.transform.localScale.y,
            healthBar.transform.localScale.z
        );

        // 체력바의 위치를 고정된 축을 기준으로 오른쪽으로 이동시키기
        Vector3 healthBarPosition = healthBar.transform.localPosition;
        healthBarPosition.x = -(maxHealthBarWidth - healthBarWidth) / 2;
        healthBar.transform.localPosition = healthBarPosition;
    }

    public void Freeze(float duration)
    {
        SetState(new FrozenState(duration));
    }

    public void Poison(float duration, float damage)
    {
        GameObject poisonEffectPrefab = Resources.Load<GameObject>("Prefabs/PoisonEffect");
        SetState(new PoisonedState(duration, damage, poisonEffectPrefab));
    }

    public void Burn(float duration, float damage)
    {
        GameObject fireEffectPrefab = Resources.Load<GameObject>("Prefabs/FireEffect");
        SetState(new BurnState(duration, damage, fireEffectPrefab));
    }
}
