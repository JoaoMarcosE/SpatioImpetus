using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    float maxSpawnRateInSeconds = 5f;

    float maxNumberOfEnemiesOnScreen = 5;
    public float numberOfEnemiesOnScreen = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    void SpawnEnemy()
    {
        if (numberOfEnemiesOnScreen < maxNumberOfEnemiesOnScreen)
        {
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            GameObject newEnemy = (GameObject)Instantiate(enemy);
            newEnemy.transform.position = new Vector2(max.x, Random.Range(min.y, max.y));

            //numberOfEnemiesOnScreen++;
        }

        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;

        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawnInNSeconds = 1f;

        Invoke("SpawnEnemy", spawnInNSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }
}
