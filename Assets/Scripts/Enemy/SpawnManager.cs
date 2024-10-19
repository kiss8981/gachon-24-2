using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : Singleton<SpawnManager>
{
    public GameObject[] enemys;
    public float spawnInterval = 2f;
    public List<GameObject> activeEnemies;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        int wave = GameManager.Instance.currentWave;
        GameManager.Instance.playerHealth = CalculateEnemyCount(wave) * 2;
    }

    private void Update()
    {
        NextWave();
    }

    public void NextWave()
    {
        if (activeEnemies.Count > 0)
        {
            return;
        }
        GameManager.Instance.currentWave++;
        SceneManager.LoadScene(GameManager.Instance.currentWave - 1);
        TowerManager.Instance.DestroyAllTowers();
        TowerManager.Instance.RenameTagsBuildSites();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        int wave = GameManager.Instance.currentWave;
        int enemyCount = CalculateEnemyCount(wave);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = GetSpawnPoint();
            GameObject enemy = Instantiate(enemys[Random.Range(0, enemys.Length)], spawnPosition, Quaternion.identity);
            activeEnemies.Add(enemy);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    int CalculateEnemyCount(int wave)
    {
        return wave * 5;
    }

    Vector3 GetSpawnPoint()
    {
        return GameManager.Instance.mapManager.spawnPoint.transform.position;
    }
}
