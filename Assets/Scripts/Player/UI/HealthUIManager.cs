using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour, IHealthObserver
{
    public TMP_Text healthText;

    private void OnEnable()
    {
        GameManager.Instance.RegisterHealthObserver(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.UnregisterHealthObserver(this);
    }

    private void Awake()
    {
        healthText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        OnHealthChanged(GameManager.Instance.playerHealth);
    }

    public void OnHealthChanged(int newHealth)
    {
        healthText.text = "Health: " + newHealth.ToString();
    }
}
