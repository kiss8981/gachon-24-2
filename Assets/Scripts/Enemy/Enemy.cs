using System;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    NormalState,
    PoisonedState,
    ElectrifiedState,
    BurnState,
    FrozenState,
    FloatingState
}

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth = 100f;
    public float speed;
    public int target = 0;
    public GameObject healthBar;

    private Collider2D coll;
    private Rigidbody2D rb;

    private float maxHealthBarWidth;
    private float healthBarWidth;

    private IEnemyState currentState;

    public EnemyState state = EnemyState.NormalState;

    private readonly Dictionary<string, GameObject> effectPrefabs =
        new Dictionary<string, GameObject>();

    void Awake()
    {
        InitializeComponents();
        LoadEffectPrefabs();
    }

    void Start()
    {
        SetState(new NormalState());
        maxHealthBarWidth = healthBar.transform.localScale.x;
    }

    void Update()
    {
        healthBarUpdate();
        currentState.Execute(this);
    }

    private void InitializeComponents()
    {
        if (coll == null)
        {
            coll = gameObject.AddComponent<Collider2D>();
        }

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
            rb.isKinematic = false;
        }
    }

    private void LoadEffectPrefabs()
    {
        effectPrefabs["PoisonEffect"] = Resources.Load<GameObject>("Prefabs/PoisonEffect");
        effectPrefabs["FireEffect"] = Resources.Load<GameObject>("Prefabs/FireEffect");
    }

    public void SetState(IEnemyState newState)
    {
        currentState = newState;
        state = (EnemyState)Enum.Parse(typeof(EnemyState), newState.GetType().Name);
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
        healthBarWidth = (health / maxHealth) * maxHealthBarWidth;
        healthBar.transform.localScale = new Vector3(
            healthBarWidth,
            healthBar.transform.localScale.y,
            healthBar.transform.localScale.z
        );

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
        SetState(new PoisonedState(duration, damage, effectPrefabs["PoisonEffect"]));
    }

    public void Burn(float duration, float damage)
    {
        SetState(new BurnState(duration, damage, effectPrefabs["FireEffect"]));
    }

    public void Floating(float duration)
    {
        SetState(new FloatingState(duration));
    }
}
