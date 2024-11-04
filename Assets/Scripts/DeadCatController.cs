using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCatController : MonoBehaviour, Cat
{
    private GameManager gameManager;

    private Animator animator;
    private Rigidbody2D deadCatRb;
    private float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        deadCatRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dead();
    }

    // Update is called once per frame
    void Update()
    {
        moveLeft();
    }

    void moveLeft()
    {
        if (deadCatRb.transform.position.x >= 0)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
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
        animator.SetTrigger("IsJumping");
    }

    public void dead()
    {
        animator.SetTrigger("IsDead");
    }
}
