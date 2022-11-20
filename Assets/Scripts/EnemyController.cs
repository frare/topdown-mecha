using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    [Header("Attributes")]
    private int enemyDefeatedCount = 0;
    private int currentWave = -1;

    [Header("References")]
    [SerializeField] private List<Wave> waves;
    private List<Enemy> enemies;
    private ObjectPool[] enemyPools;

    



    private void Awake()
    {
        instance = this;

        enemyPools = GetComponentsInChildren<ObjectPool>(true);
    }

    public static void OnEnemyDefeated()
    {
        instance.enemyDefeatedCount++;

        if (instance.enemyDefeatedCount >= instance.waves[instance.currentWave].enemies.Count) 
        {
            if (instance.currentWave + 1 >= instance.waves.Count) print("defeated final wave");
            else SpawnNextWave();
        }
    }

    public static void SpawnNextWave()
    {
        instance.StopCoroutine(instance.SpawnNextWaveCoroutine());
        instance.StartCoroutine(instance.SpawnNextWaveCoroutine());
    }

    private IEnumerator SpawnNextWaveCoroutine()
    {
        currentWave++;
        Wave wave = waves[currentWave];
        enemyDefeatedCount = 0;

        Debug.Log("Spawning wave " + wave.name);
        
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < wave.enemies.Count; i++)
        {
            Debug.Log("Spawning enemy " + wave.enemies[i].ToString());
            GameObject enemy = enemyPools[(int)wave.enemies[i] - 1].GetNext();
            enemy.transform.position = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            enemy.SetActive(true);
        }
    }
}