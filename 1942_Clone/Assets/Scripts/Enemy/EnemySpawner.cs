using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector] public int spawnCount;    // Current spawn count
    public int spawnAmount;             // Number of enemies to spawn at a time
    public int level;                   // Level of the spawner
    public int maxSpawn;                // Maximum number of spawns

    public Vector3 spawnRadius;         // Random spawn radius

    public GameObject greenEnemy;       // Green enemy prefab
    public GameObject redEnemy;         // Red enemy prefab
    public GameObject blueEnemy;        // Blue enemy prefab
    public GameObject boss;             // Boss enemy prefab
    private GameObject enemy;           // Current enemy type to spawn

    private Vector3 spawnPoint;         // Spawn point for enemies

    public float spawnInterval = 1.5f;  // Delay between spawns
    public float setSpawnDelay = 5f;    // Delay between waves

    GameManager GM;                     // Reference to the game manager

    void Start()
    {
        GM = FindAnyObjectByType<GameManager>();   // Get a reference to the game manager
        spawnCount = 0;                           // Reset spawn count
        spawnAmount = 5;                          // Default spawn amount
        StartCoroutine(SpawnEnemies());           // Start spawning enemies
        GM.EnemyBoss = 1;
        Instantiate(boss, new Vector3(-26.42f, 6.08f, 0f), Quaternion.Euler(0f, 0f, 180f));
    }

    IEnumerator SpawnEnemies()
    {
        while (GM.EnemyCount < maxSpawn)  // Check if the current enemy count is less than the maximum spawn count
        {
            if (spawnCount == 0)
            {
                spawnPoint = GetSpawnPoint();       // Get a random spawn point
                enemy = GetEnemyType();             // Get a random enemy type
            }

            spawnPoint.x += 1.5f;
            spawnPoint.y += 1.5f;
            GameObject newEnemy = Instantiate(enemy, spawnPoint, Quaternion.Euler(0f, 0f, 180f));
            GM.EnemyCount++;                        // Increase enemy count in the game manager
            spawnCount++;                           // Increase spawn count

            if (spawnCount >= spawnAmount)
            {
                spawnCount = 0;
                yield return new WaitForSeconds(setSpawnDelay);
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        // Wait until all enemies are defeated before finishing the coroutine
        while (GM.EnemyCount > 0)
        {
            yield return null;
        }

        // All enemies have been defeated, do any necessary post-spawn logic here
        Debug.Log("All enemies have been defeated.");
    }

    private Vector3 GetSpawnPoint()
    {
        spawnRadius = new Vector3(UnityEngine.Random.Range(-10f, 10f), 0, 0);    // Randomize spawn radius
        return spawnRadius;
    }

    private GameObject GetEnemyType()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);   // Get a random value between 0 and 1
        if (randomValue <= 0.33f)
        {
            return greenEnemy;                                 // Return green enemy prefab
        }
        else if (randomValue > 0.66f)
        {
            return blueEnemy;                                  // Return blue enemy prefab
        }
        else
        {
            return redEnemy;                                   // Return red enemy prefab
        }
    }
}
