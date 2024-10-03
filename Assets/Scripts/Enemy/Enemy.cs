using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public int target = 0;
    private Collider2D coll;
    private Rigidbody2D rb;

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

        rb.gravityScale = 0; // 중력을 비활성화
        rb.freezeRotation = true; // 회전을 고정하여 기울어지지 않도록 설정
        rb.isKinematic = false; // 물리 엔진에 의해 영향을 받도록 설정
    }

    void Start()
    {
        SetState(new NormalState());
    }

    private void FixedUpdate() // FixedUpdate에서 호출
    {
        currentState.Execute(this);
    }

    public void SetState(IEnemyState newState)
    {
        currentState = newState;
        currentState.Initialize(this);
    }

    public void TakeDamage(int damage)
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

    public void Freeze(float duration)
    {
        SetState(new FrozenState(duration));
    }

    public void Poison(float duration, int damage)
    {
        SetState(new PoisonedState(duration, damage));
    }
}
