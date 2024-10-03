using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public GameObject[] enemys;
    public float spawnInterval = 2f; // 적 스폰 간격 (초)

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        int wave = GameManager.Instance.currentWave;
        int enemyCount = CalculateEnemyCount(wave);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = GetSpawnPoint();
            Instantiate(enemys[Random.Range(0, enemys.Length)], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    int CalculateEnemyCount(int wave)
    {
        // 예시: 웨이브 수에 따라 적의 수를 계산하는 로직
        return wave * 5; // 예: 웨이브 수 x 5만큼의 적 스폰
    }

    Vector3 GetSpawnPoint()
    {
        return MapManager.Instance.spawnPoint.transform.position;
    }
}
