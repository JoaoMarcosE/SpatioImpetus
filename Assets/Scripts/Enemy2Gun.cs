using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Gun : MonoBehaviour
{
    public GameObject enemyBullet;
    public GameObject enemyBulletPosition1;
    public GameObject enemyBulletPosition2;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FireEnemyBullet", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("Player");

        if (playerShip != null)
        {
            GameObject bullet1 = (GameObject)Instantiate(enemyBullet);
            bullet1.transform.position = enemyBulletPosition1.transform.position;

            GameObject bullet2 = (GameObject)Instantiate(enemyBullet);
            bullet2.transform.position = enemyBulletPosition2.transform.position;

            Vector2 direction = playerShip.transform.position - bullet1.transform.position;
            bullet1.GetComponent<EnemyBullet>().SetDirection(direction);

            direction = playerShip.transform.position - bullet2.transform.position;
            bullet2.GetComponent<EnemyBullet>().SetDirection(direction);
        }

        ScheduleNextEnemyBullet();
    }

    void ScheduleNextEnemyBullet()
    {
        Invoke("FireEnemyBullet", 1f);
    }
}
