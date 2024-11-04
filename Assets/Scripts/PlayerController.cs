using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour, Cat
{
    private GameManager gameManager;

    private Animator animator;
    private Rigidbody2D playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;

    public AudioClip jumpAudio;
    public AudioClip crashAudio;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;

        audioSource = gameObject.AddComponent<AudioSource>();
        run(true);
    }

    // Update is called once per frame
    void Update()
    {
        // CONTROL
        jump();

        ending();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            run(true);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
            dead();
            PlayCrashAudio();
            Debug.Log("Game Over!");
        }
    }

    public void idle(bool toggle)
    {
        animator.SetBool("isIdling", toggle);
    }

    public void walk(bool toggle)
    {
        animator.SetBool("IsWalking", toggle);
    }

    public void run(bool toggle)
    {
        animator.SetBool("IsRunning", toggle);
    }

    public void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameManager.isGameActive && !gameManager.isGameOver && gameManager.getCount() < 5)
        {
            playerRb.AddForce(Vector2.up * jumpForce);
            isOnGround = false;
            animator.SetTrigger("IsJumping");
            PlayJumpAudio();
        }
    }

    public void dead()
    {
        animator.SetTrigger("IsDead");
    }

    public void ending()
    {
        if (gameManager.getCount() >= 10)
        {
            run(false);
            walk(true);
        }

        if (gameManager.getDeadCatPos() <= 0.01f && playerRb.transform.position.x <= -3)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 3);
        }

        if (playerRb.transform.position.x <= -1 && playerRb.transform.position.x >= -3)
        {
            idle(true);
            walk(false);
        }
    }

    private void PlayJumpAudio()
    {
        if (jumpAudio != null)
        {
            audioSource.PlayOneShot(jumpAudio);
        }
    }

    private void PlayCrashAudio()
    {
        if (crashAudio != null)
        {
            audioSource.PlayOneShot(crashAudio);
        }
    }
}
