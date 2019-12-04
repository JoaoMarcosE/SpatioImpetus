using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Gun : MonoBehaviour
{
    public GameObject enemyBullet;
    int timerKamikaze;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FireEnemyBullet", 1f);
        timerKamikaze = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireEnemyBullet()
    {
        if (timerKamikaze < 8)
        {
            GameObject playerShip = GameObject.Find("Player");

            if (playerShip != null)
            {
                GameObject bullet = (GameObject)Instantiate(enemyBullet);
                bullet.transform.position = transform.position;

                Vector2 direction = playerShip.transform.position - bullet.transform.position;
                bullet.GetComponent<EnemyBullet>().SetDirection(direction);
            }

            ScheduleNextEnemyBullet();
        }

        ++timerKamikaze;
    }

    void ScheduleNextEnemyBullet()
    {
        Invoke("FireEnemyBullet", 1f);
    }
}
