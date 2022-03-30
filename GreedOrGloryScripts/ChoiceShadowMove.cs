using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * 
 * Code inspired by Mr. Pergerson's platformer tutorial
 * https://mr-pergerson.itch.io/platformer-starter-project
 * 
 * Animation sections from Brackeys tutorial
 * https://www.youtube.com/watch?v=hkaysu1Z-N8&ab_channel=Brackeys
 * 
 * Code to have restart game option after death from this site
 * https://www.codegrepper.com/code-examples/csharp/how+to+make+a+restart+button+in+unity+2d
 * 
 * Code for text manipulation inspired by forum
 * https://forum.unity.com/threads/changing-textmeshpro-text-from-ui-via-script.462250/
 * 
 * Some code inspired by my previous code in Dungeon Champion
 * Code author: Elston Ma
 * 
 */

public class ChoiceShadowMove : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float speed = 20;
    public float jumpForce = 30;
    public SpriteRenderer sRend;
    public Animator animate; // from Brackeys
    //private int keyFragCollected = 0;
    //private int totalKeyFrags = 10;
    //public GameObject keysTextObj;
    //public GameObject restartHint;
    private bool dead = false;
    public AudioSource sJumpSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            // from Dungeon Champion
            if (Input.GetButtonDown("Jump") && rigid.velocity.y == 0)
            {
                sJumpSound.Play();
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                animate.SetBool("isJump", true); // from Brackeys
            }

            float runVec = Input.GetAxisRaw("Horizontal");
            float newVel = runVec * speed;
            rigid.velocity = new Vector2(newVel, rigid.velocity.y);
            animate.SetFloat("Speed", Mathf.Abs(newVel)); // from Brackeys

            // set sprite facing right direction
            // from Dungeon Champion
            switch (runVec)
            {
                case -1:
                    sRend.flipX = true;
                    break;

                case 1:
                    sRend.flipX = false;
                    break;
            }
        }
        else
        {
            animate.SetFloat("Speed", 0); // from Brackeys
        }

        // from Dungeon Champion
        if (rigid.velocity.y == 0)
        {
            animate.SetBool("isJump", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Glory") {
            SceneManager.LoadScene("BossLevel");
        }
        if (collision.gameObject.tag == "Greed") {
            SceneManager.LoadScene("TreasureLevel");
        }
    }
}
