using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    [Header("Attributes")]
    private int enemyDefeatedCount = 0;
    private int currentWave = -1;
    private int waveCountTotal = 0;
    [SerializeField] private int wavesAmountToSpawnElite;

    [Header("References")]
    [SerializeField] private List<Wave> waves = new List<Wave>();
    private List<Enemy> enemiesAlive = new List<Enemy>();
    private ObjectPool[] enemyPools;

    



    private void Awake()
    {
        instance = this;

        enemyPools = GetComponentsInChildren<ObjectPool>(true);
    }

    public static void SpawnNextWave()
    {
        instance.StopCoroutine(instance.SpawnNextWaveCoroutine());
        instance.StartCoroutine(instance.SpawnNextWaveCoroutine());
    }

    private IEnumerator SpawnNextWaveCoroutine()
    {
        waveCountTotal++;
        currentWave = currentWave < waves.Count ? 0 : currentWave++;
        Wave wave = waves[currentWave];
        enemyDefeatedCount = 0;
        enemiesAlive.Clear();

        string log = "Spawning wave " + wave.name;
        
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < wave.enemies.Count; i++)
        {
            log += "\n     Spawning enemy " + wave.enemies[i].ToString();
            SpawnEnemy(wave.enemies[i]);
        }

        if (wave.hasElites && waveCountTotal % wavesAmountToSpawnElite == 0) enemiesAlive[Random.Range(0, enemiesAlive.Count)].SetElite();

        Debug.Log(log);
    }

    private void SpawnEnemy(EnemyType enemyType)
    {
        var enemy = enemyPools[(int)enemyType - 1].GetNext().GetComponent<Enemy>();

        enemy.OnEnemyDefeated += OnEnemyDefeated;

        enemiesAlive.Add(enemy.GetComponent<Enemy>());

        enemy.transform.position = Player.position + (Quaternion.Euler(0, Random.Range(0, 359), 0) * Vector3.forward * DifficultyManager.difficulty * 10);
        enemy.gameObject.SetActive(true);
    }

    private void OnEnemyDefeated(Enemy enemy)
    {
        enemy.OnEnemyDefeated -= OnEnemyDefeated;

        instance.enemyDefeatedCount++;

        if (instance.enemyDefeatedCount >= instance.waves[instance.currentWave].enemies.Count) 
        {
            DifficultyManager.IncreaseDifficulty();
            SpawnNextWave();
        }
    }
}