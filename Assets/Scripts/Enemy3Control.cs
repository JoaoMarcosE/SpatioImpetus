using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Control : MonoBehaviour
{
    public GameObject explosion;
    public GameObject ammoDrop;
    public GameObject energyDrop;

    bool movingUp;

    float speed;

    int timerKamikaze;
    Vector2 _direction;

    bool kamikazeMode = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        movingUp = false;
        timerKamikaze = 0;

        Invoke("KamikazeTimer", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        Vector2 screenBottomLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenTopLimit = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 movementLimit = Camera.main.ViewportToWorldPoint(new Vector2(0.7f, 0));

        if (!kamikazeMode)
        {
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
        }
        else
        {
            position += _direction * speed * Time.deltaTime;
        }

        transform.position = position;

        if (transform.position.x < screenBottomLimit.x)
        {
            Destroy(gameObject);
        }

        if (timerKamikaze >= 8)
        {
            GameObject playerShip = GameObject.Find("Player");
            _direction = playerShip.transform.position - transform.position;

            speed = 1.2f;

            timerKamikaze = 0;
            kamikazeMode = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerShipTag" || collision.tag == "PlayerBulletTag")
        {
            PlayExplosion();

            if (!DropAmmo())
                DropEnergy();

            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject newExplosion = (GameObject)Instantiate(explosion);
        newExplosion.transform.position = transform.position;
    }

    void KamikazeTimer()
    {
        ++timerKamikaze;

        if (timerKamikaze < 8)
            Invoke("KamikazeTimer", 1f);
    }

    bool DropAmmo()
    {
        int probabilityNumber = Random.Range(0, 5);
        if (probabilityNumber == 3)
        {
            GameObject newAmmoDrop = (GameObject)Instantiate(ammoDrop);
            newAmmoDrop.transform.position = transform.position;

            return true;
        }

        return false;
    }

    bool DropEnergy()
    {
        int probabilityNumber = Random.Range(0, 5);
        if (probabilityNumber == 4)
        {
            GameObject newEnergyDrop = (GameObject)Instantiate(energyDrop);
            newEnergyDrop.transform.position = transform.position;

            return true;
        }

        return false;
    }
}
