using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int playerHealth = 100;
    public int playerMoney = 10000;
    public int currentWave = 1;
    public int totalResources = 0;

    private List<IHealthObserver> healthObservers = new List<IHealthObserver>();

    protected override void Awake()
    {
        base.Awake();
        // 추가적인 초기화 작업
    }

    private void RegisterObserver<T>(List<T> observerList, T observer)
        where T : class
    {
        if (!observerList.Contains(observer))
        {
            observerList.Add(observer);
        }
    }

    private void UnregisterObserver<T>(List<T> observerList, T observer)
        where T : class
    {
        if (observerList.Contains(observer))
        {
            observerList.Remove(observer);
        }
    }

    private void NotifyObservers<T>(List<T> observers, System.Action<T> notifyAction)
    {
        foreach (var observer in observers)
        {
            notifyAction(observer);
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        NotifyHealthChanged();
        if (playerHealth <= 0)
        {
            Debug.Log("GameOver");
        }
    }

    public void subtractMoney(int price)
    {
        playerMoney -= price;
    }

    private void NotifyHealthChanged() =>
        healthObservers.ForEach(o => o.OnHealthChanged(playerHealth));

    public void RegisterHealthObserver(IHealthObserver observer) =>
        RegisterObserver(healthObservers, observer);

    public void UnregisterHealthObserver(IHealthObserver observer) =>
        UnregisterObserver(healthObservers, observer);
}
