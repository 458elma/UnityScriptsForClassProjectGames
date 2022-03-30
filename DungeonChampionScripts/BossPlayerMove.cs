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
 */

public class BossPlayerMove : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float speed = 10;
    public float jumpPower = 5;
    public Animator animator; // from Brackeys
    private bool canMove = true;
    public GameObject scoreText;
    public GameObject statusText;
    private float score = 0;
    private float maxScore = 4;
    private string winLoseText = "";
    public AudioSource jumpSound;
    public AudioSource enemyHitSound;
    public AudioSource playerLostSound;
    public AudioSource gameWonSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetButtonDown("Jump") && rigidBody.velocity.y == 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpPower);
                animator.SetBool("IsJump", true); // from Brackeys
                jumpSound.Play();
            }

            float runDirection = Input.GetAxisRaw("Horizontal");
            float newVelocity = runDirection * speed;
            rigidBody.velocity = new Vector2(newVelocity, rigidBody.velocity.y);

            if (rigidBody.velocity.y == 0)
            {
                animator.SetBool("IsJump", false);
            }

            animator.SetFloat("Speed", Mathf.Abs(newVelocity)); // from Brackeys
        }
        else
        {
            // Code to restart the boss battle (credit mentioned in credits above)
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString() + " out of " + maxScore.ToString() + " hits landed";
        statusText.GetComponent<TextMeshProUGUI>().text = winLoseText;

        if (score == maxScore)
        {
            winLoseText = "You win! Press x to play again";
            // Code to restart the game
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boss" &&
            transform.position.y - collision.gameObject.transform.position.y <= 1) //&&
            //transform.position.y - collision.gameObject.transform.position.y >= -5)
        {
            canMove = false;
            playerLostSound.Play();
            winLoseText = "You lose! Press x to replay the boss battle";
        }
        else if (collision.gameObject.tag == "Boss" &&
          transform.position.y - collision.gameObject.transform.position.y > 1)
        {
            enemyHitSound.Play();
            score++;
            if (score == maxScore) {
                gameWonSound.Play();
            }
        }
    }
}
