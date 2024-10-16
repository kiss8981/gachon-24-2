using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public GameObject[] enemys;
    public float spawnInterval = 2f;

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
        return wave * 5;
    }

    Vector3 GetSpawnPoint()
    {
        return MapManager.Instance.spawnPoint.transform.position;
    }
}
