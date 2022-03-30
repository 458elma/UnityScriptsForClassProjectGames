using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Movement code inspired by Mr.Pergerson's platformer tutorial 
 * https://mr-pergerson.itch.io/platformer-starter-project
 * 
 * Some code inspired by my previous code in Dungeon Champion
 * Code author: Elston Ma
 * 
 */

public class EnemyMove : MonoBehaviour
{
    public float moveBounds = 5;
    private float startPos;
    private float leftStop;
    private float rightStop;
    private bool dir;
    public float speed = 30;
    public Rigidbody2D rb;
    public SpriteRenderer sRend;

    // Start is called before the first frame update
    void Start()
    {
        // from Dungeon Champion
        startPos = transform.position.x;
        leftStop = startPos - moveBounds;
        rightStop = startPos + moveBounds;
        dir = sRend.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        // direction code of samurai from Dungeon Champion
        if (transform.position.x < leftStop) {
            sRend.flipX = true;
        }
        if (transform.position.x > rightStop) {
            sRend.flipX = false;
        }
        dir = sRend.flipX;
        // movement code inspired from Mr. Pergerson
        float velRight = speed;
        float velLeft = -1 * speed;
        if (dir) {
            rb.velocity = new Vector2(velRight, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(velLeft, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Shuriken") {
            Destroy(gameObject);
        }
    }
}
