using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSounds : MonoBehaviour
{
    //public AudioSource sJumpSound;
    public AudioSource sDeathSound;
    public AudioSource sCollectSound;
    //public Rigidbody2D rigBod;
    //private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isDead) {
            sJumpSound.mute = true;
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigBod.velocity.y) < 0.1) {
            sJumpSound.Play();
        }
        */
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "General"
            || collision.gameObject.tag == "Spike"
            || collision.gameObject.tag == "Samurai")
        {
            sDeathSound.Play();
            //isDead = true;
        }
        if (collision.gameObject.tag == "Keystone"
            || collision.gameObject.tag == "Treasure") {
            sCollectSound.Play();
        }
    }
}
