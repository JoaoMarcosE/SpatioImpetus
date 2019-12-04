using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehaviour : MonoBehaviour
{
    float speed = 1f;

    private void Update()
    {
        Vector2 position = transform.position;
        Vector2 screenBottomLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        position = new Vector2(position.x - speed * Time.deltaTime, position.y);
        transform.position = position;

        if (transform.position.x < screenBottomLimit.x)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerShipTag")
        {
            Destroy(gameObject);
        }
    }
}
