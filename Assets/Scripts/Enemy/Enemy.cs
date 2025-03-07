using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    NormalState,
    PoisonedState,
    ElectrifiedState,
    BurnState,
    FrozenState,
    FloatingState,
    InvisibleState
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

    [SerializeField]
    private bool useInvisible = false; // 은신 사용 여부

    public float invisibleDuration; // 은신 유지 시간
    public float invisibleCoolTime; // 은신 쿨타임
    private float invisibleCooldownTimer = 0f; // 은신 쿨타임을 추적하는 변수
    private bool isInvisibleOnCooldown = false; // 쿨타임 중인지 여부
    public bool isInvisible = false; // 은신 여부 확인

    [SerializeField]
    private bool useHealthRegen = false; // 체력 회복 사용 여부
    public float healthRegenDuration; // 체력 회복 시간
    public float healthRegenAmount; // 체력 회복량
    private bool isHealthRegenOnCooldown = false; // 체력 회복 쿨타임 중인지 여부
    private float healthRegenCooldownTimer = 0f; // 체력 회복 쿨타임을 추적하는 변수

    public Color defaultColor = Color.white;

    public SpriteRenderer spriteRenderer; // 적의 스프라이트 렌더러

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

        if (useInvisible)
        {
            HandleInvisible();
        }

        if (useHealthRegen)
        {
            HandleHealthRegen();
        }
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

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }
        }

        defaultColor = spriteRenderer.color;
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
            SpawnManager.Instance.activeEnemies.Remove(gameObject);
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
            SpawnManager.Instance.activeEnemies.Remove(gameObject);
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

    private void HandleInvisible()
    {
        // 쿨타임이 돌고 있는지 확인
        if (isInvisibleOnCooldown)
        {
            invisibleCooldownTimer -= Time.deltaTime;
            if (invisibleCooldownTimer <= 0f)
            {
                isInvisibleOnCooldown = false; // 쿨타임 해제
            }
        }
        else if (!isInvisible) // 쿨타임 중이 아니고, 현재 은신 상태가 아니라면
        {
            GoInvisible(invisibleDuration); // 은신 상태로 전환
            isInvisible = true; // 은신 상태 활성화
            isInvisibleOnCooldown = true; // 쿨타임 시작
            invisibleCooldownTimer = invisibleCoolTime; // 쿨타임 설정
        }
    }

    private void HandleHealthRegen()
    {
        if (isHealthRegenOnCooldown)
        {
            healthRegenCooldownTimer -= Time.deltaTime;
            if (healthRegenCooldownTimer <= 0f)
            {
                isHealthRegenOnCooldown = false;
            }
        }
        else if (health < maxHealth)
        {
            HealthRegen(healthRegenDuration, healthRegenAmount);
            isHealthRegenOnCooldown = true;
            healthRegenCooldownTimer = healthRegenDuration;
        }
    }

    public void HealthRegen(float duration, float regenAmount)
    {
        StartCoroutine(HealthRegenRoutine(duration, regenAmount));
    }

    private IEnumerator HealthRegenRoutine(float duration, float regenAmount)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            health += regenAmount * Time.deltaTime;
            health = Mathf.Min(health, maxHealth); // 체력은 최대 체력 이상으로 회복되지 않음
            elapsedTime += Time.deltaTime;
            yield return null;
        }
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

    public void GoInvisible(float duration)
    {
        SetState(new InvisibleState(duration));
    }
}
