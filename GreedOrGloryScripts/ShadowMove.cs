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

public class ShadowMove : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float speed = 20;
    public float jumpForce = 30;
    public SpriteRenderer sRend;
    public Animator animate; // from Brackeys
    private int keyFragCollected = 0;
    private int totalKeyFrags = 10;
    public GameObject keysTextObj;
    public GameObject restartHint;
    private bool dead = false;
    public AudioSource sJumpSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead) {
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
        } else {
            animate.SetFloat("Speed", 0); // from Brackeys
        }

        // from Dungeon Champion
        if (rigid.velocity.y == 0)
        {
            animate.SetBool("isJump", false);
        }

        // from Dungeon Champion
        // have text follow Shadow
        // update text if keystone frag found
        keysTextObj.transform.position = new Vector2(transform.position.x + 50, 45f);
        keysTextObj.GetComponent<TextMeshProUGUI>().text =
            keyFragCollected.ToString() + " out of " + totalKeyFrags.ToString() 
            + " Keystone fragments found";
        restartHint.transform.position = new Vector2(transform.position.x + 70, 38f);

        // restart the level anytime
        if (Input.GetKeyDown(KeyCode.X)) {
            SceneManager.LoadScene("StartingLevel");
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Keystone") {
            keyFragCollected++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Spike"
            || collision.gameObject.tag == "Samurai"
            || collision.gameObject.tag == "General") {
            dead = true;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (collision.gameObject.tag == "Exit1" && keyFragCollected == totalKeyFrags) {
            SceneManager.LoadScene("ChoiceLevel");
        }
    }
}
