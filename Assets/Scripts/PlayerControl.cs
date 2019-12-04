using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public GameObject playerBullet;
    public GameObject bulletPosition1;
    public GameObject bulletPosition2;
    public GameObject explosion;

    public float speed;

    float energyTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UIManager>().health = 1;
        GetComponent<UIManager>().energy = 30;
        GetComponent<UIManager>().ammunition = 150;

        GetComponent<UIManager>().numOfHearts = 3;
        GetComponent<UIManager>().numOfEnergyBars = 3;
        GetComponent<UIManager>().numOfAmmunitions = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && GetComponent<UIManager>().ammunition > 0)
        {
            GetComponent<AudioSource>().Play();

            GameObject bullet1 = (GameObject)Instantiate(playerBullet);
            bullet1.transform.position = bulletPosition1.transform.position;

            GameObject bullet2 = (GameObject)Instantiate(playerBullet);
            bullet2.transform.position = bulletPosition2.transform.position;

            GetComponent<UIManager>().ammunition -= 1;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        if( GetComponent<UIManager>().energy > 0)
            Move(direction);

        energyTimer += 1f;

        if (energyTimer > 150)
        {
            energyTimer = 0;
            GetComponent<UIManager>().energy -= 1;
        }
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.325f;
        min.x = min.x + 0.325f;

        max.y = max.y - 0.525f;
        min.y = min.y + 0.525f;

        Vector2 position = transform.position;

        position += direction * speed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, min.x, max.x);
        position.y = Mathf.Clamp(position.y, min.y, max.y);

        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyShipTag" || collision.tag == "EnemyBulletTag")
        {
            PlayExplosion();

            GetComponent<UIManager>().numOfHearts--;

            if (GetComponent<UIManager>().numOfHearts == 0)
            {
                PlayerPrefs.SetInt("lastSceneIndex", SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene(5); // Game Over
            }
        }
        else if (collision.tag == "AmmoDrop")
        {
            GetComponent<UIManager>().ammunition += 50;
        }
        else if (collision.tag == "EnergyDrop")
        {
            GetComponent<UIManager>().energy += 10;
        }
    }

    void PlayExplosion()
    {
        GameObject newExplosion = (GameObject)Instantiate(explosion);
        newExplosion.transform.position = transform.position;
    }
}
