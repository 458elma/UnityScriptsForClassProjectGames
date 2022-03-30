using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Some code inspired by my previous code in Dungeon Champion
 * Code author: Elston Ma
 * 
 */

public class ShurikenThrow : MonoBehaviour
{
    public SpriteRenderer sRend;
    private bool isFire = false;
    public GameObject shadow;
    private float startX;
    private float startY;
    private Color rendColor;
    public float maxTrav = 2f;
    private float startPosX = 0f;
    public float throwSpeed = 60f;
    private int throwDir = 1;
    private float staticThrowSpeed;
    public Rigidbody2D rigidbod;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        rendColor = sRend.color;
        rendColor.a = 0;
        sRend.color = rendColor;
        staticThrowSpeed = throwSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // code from Dungeon Champion
        // to determine throw direction
        switch (Input.GetAxisRaw("Horizontal")) {
            case -1:
                sRend.flipX = true;
                break;
            case 1:
                sRend.flipX = false;
                break;
        }

        // add player movement speed to shuriken velocity if running
        if (Input.GetAxisRaw("Horizontal") != 0) {
            throwSpeed = staticThrowSpeed + 40;
        }

        // press f to throw shuriken
        if (shadow.transform.rotation.eulerAngles.z != 90 && Input.GetKeyDown(KeyCode.F)) {
            isFire = true;

            rendColor.a = 1;
            sRend.color = rendColor;

            // use throw direction to set position to throw from
            // and which direction to travel
            if (sRend.flipX) {
                float newX = shadow.transform.position.x - 5;
                float newY = shadow.transform.position.y;
                startPosX = newX;
                throwDir = -1;
                transform.position = new Vector3(newX, newY, 0f);
            } else {
                float newX = shadow.transform.position.x + 5;
                float newY = shadow.transform.position.y;
                startPosX = newX;
                throwDir = 1;
                transform.position = new Vector3(newX, newY, 0f);
            }
        }

        if (isFire) {
            // if allowed to throw, make shuriken move
            transform.position += new Vector3(throwDir * throwSpeed * Time.deltaTime, 0f, 0f);
            // shuriken should disappear after travelling a certain distance
            if (Mathf.Abs(transform.position.x - startPosX) > maxTrav) {
                isFire = false;
            }
        } else {
            // when throw is disabled, get rid of shuriken
            rendColor.a = 0;
            sRend.color = rendColor;
            float oldX = startX;
            float oldY = startY;
            transform.position = new Vector3(oldX, oldY, 0f);
            // reset velocity of shuriken
            rigidbod.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Platform" 
            || collision.gameObject.tag == "Spike"
            || collision.gameObject.tag == "Samurai"
            || collision.gameObject.tag == "Keystone"
            || collision.gameObject.tag == "Treasure") {
            isFire = false;
        }
    }

}
