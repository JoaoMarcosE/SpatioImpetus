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

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UIManager>().health = 1;
        GetComponent<UIManager>().energy = 10;
        GetComponent<UIManager>().ammunition = 10;

        GetComponent<UIManager>().numOfHearts = 3;
        GetComponent<UIManager>().numOfEnergyBars = 3;
        GetComponent<UIManager>().numOfAmmunitions = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<AudioSource>().Play();

            GameObject bullet1 = (GameObject)Instantiate(playerBullet);
            bullet1.transform.position = bulletPosition1.transform.position;

            GameObject bullet2 = (GameObject)Instantiate(playerBullet);
            bullet2.transform.position = bulletPosition2.transform.position;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }

    void PlayExplosion()
    {
        GameObject newExplosion = (GameObject)Instantiate(explosion);
        newExplosion.transform.position = transform.position;
    }
}
