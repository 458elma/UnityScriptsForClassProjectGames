using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Movement code inspired by Mr.Pergerson's platformer tutorial 
 * https://mr-pergerson.itch.io/platformer-starter-project 
 * 
 */

public class EnemyMove : MonoBehaviour
{
    public float moveBoundary = 5;
    private float startPosition;
    private float leftBound;
    private float rightBound;
    public SpriteRenderer rend;
    private bool direction;
    public float speed = 10;
    public Rigidbody2D rigBod;
    public GameObject player; 

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
        if (transform.position.x < leftBound) {
            rend.flipX = true;
        }
        if (transform.position.x > rightBound) {
            rend.flipX = false;
        }
        direction = rend.flipX;
  
        // section inspired from Mr. Pergerson (more details above)
        float vRight = speed;
        float vLeft = -1 * speed;
        if (direction) {
            rigBod.velocity = new Vector2(vRight, rigBod.velocity.y);
        } else {
            rigBod.velocity = new Vector2(vLeft, rigBod.velocity.y);
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            Destroy(gameObject);
        }
        */
    }

    // Collision detection
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" &&
            player.transform.position.y - transform.position.y > 1) {
            Destroy(gameObject);
        } else if (collision.gameObject.tag == "Player" &&
            player.transform.position.y - transform.position.y <= 1) {
            rend.flipX = !(rend.flipX);
            //Destroy(player);
        }

        if (collision.gameObject.tag == "Ogre") {
            rend.flipX = !(rend.flipX);
        }
    }
}
