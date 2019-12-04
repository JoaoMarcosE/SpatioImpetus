using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Control : MonoBehaviour
{
    public GameObject explosion;
    public GameObject ammoDrop;
    public GameObject energyDrop;
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

            int probabilityNumber = Random.Range(0, 10);

            if (probabilityNumber % 2 == 0)
                DropEnergy();
            else
                DropAmmo();

            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject newExplosion = (GameObject)Instantiate(explosion);
        newExplosion.transform.position = transform.position;
    }

    bool DropAmmo()
    {
        int probabilityNumber = Random.Range(0, 10);
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
