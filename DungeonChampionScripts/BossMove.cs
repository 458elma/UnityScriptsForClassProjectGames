using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Movement code inspired by Mr.Pergerson's platformer tutorial 
 * https://mr-pergerson.itch.io/platformer-starter-project 
 * 
 */

public class BossMove : MonoBehaviour
{
    private float moveBoundary = 10;
    private float startPosition;
    private float leftBound;
    private float rightBound;
    public SpriteRenderer rend;
    private bool direction;
    public float speed = 10;
    public Rigidbody2D rigBod;
    public GameObject player;
    private int hitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        leftBound = startPosition - moveBoundary;
        rightBound = startPosition + moveBoundary;
        direction = rend.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftBound)
        {
            rend.flipX = true;
        }
        if (transform.position.x > rightBound)
        {
            rend.flipX = false;
        }
        direction = rend.flipX;

        // section inspired from Mr. Pergerson (more details above)
        float vRight = speed;
        float vLeft = -1 * speed;
        if (direction)
        {
            rigBod.velocity = new Vector2(vRight, rigBod.velocity.y);
        }
        else
        {
            rigBod.velocity = new Vector2(vLeft, rigBod.velocity.y);
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            Destroy(gameObject);
        }
        */
    }

    // Collision detection
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" &&
            player.transform.position.y - transform.position.y > 1)
        {
            switch (hitCount) {
                case 0:
                    transform.position = new Vector3(-55.5f, -7.6f, 0f);
                    startPosition = transform.position.x;
                    leftBound = startPosition - moveBoundary;
                    rightBound = startPosition + moveBoundary;
                    hitCount++;
                    
                    break;
                case 1:
                    transform.position = new Vector3(49.4f, -18.4f, 0f);
                    startPosition = transform.position.x;
                    moveBoundary = 23;
                    leftBound = startPosition - moveBoundary;
                    rightBound = startPosition + moveBoundary;
                    hitCount++;
                    break;
                case 2:
                    transform.position = new Vector3(-47.9f, -18.2f, 0f);
                    startPosition = transform.position.x;
                    leftBound = startPosition - moveBoundary;
                    rightBound = startPosition + moveBoundary;
                    hitCount++;
                    break;
                case 3:
                    Destroy(gameObject);
                    break;
            }   
        }
        else if (collision.gameObject.tag == "Player" &&
          player.transform.position.y - transform.position.y <= 1)
        {
            rend.flipX = !(rend.flipX);
            //Destroy(player);
        }

    }
}
