using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSounds : MonoBehaviour
{
    public AudioSource shThrowSound;
    public AudioSource shHitSound;
    public GameObject shadow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shadow.transform.rotation.eulerAngles.z != 90 && Input.GetKeyDown(KeyCode.F)) {
            shThrowSound.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform"
            || collision.gameObject.tag == "Spike"
            || collision.gameObject.tag == "Samurai"
            || collision.gameObject.tag == "Keystone"
            || collision.gameObject.tag == "Treasure"
            || collision.gameObject.tag == "General"
            || collision.gameObject.tag == "ExitGlory")
        {
            shHitSound.Play();
        }
    }
}
