using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject explosion;
    bool movingUp;

    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        movingUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position      = transform.position;
        Vector2 screenBottomLimit   = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenTopLimit = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 movementLimit = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0));

        if (position.x <= movementLimit.x)
        {
            if (position.y <= screenBottomLimit.y + 0.350f)
            {
                movingUp = true;
            }
            else if (position.y >= screenTopLimit.y - 0.350f)
            {
                movingUp = false;
            }

            if (movingUp)
                position = new Vector2(position.x, position.y + speed * Time.deltaTime);
            else
                position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        }
        else
        {
            position = new Vector2(position.x - speed * Time.deltaTime, position.y);
        }

        transform.position = position;
        
        if (transform.position.x < screenBottomLimit.x)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerShipTag" || collision.tag == "PlayerBulletTag")
        {
            PlayExplosion();

            //GetComponent<EnemySpawner>().numberOfEnemiesOnScreen--;

            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject newExplosion = (GameObject)Instantiate(explosion);
        newExplosion.transform.position = transform.position;
    }
}
