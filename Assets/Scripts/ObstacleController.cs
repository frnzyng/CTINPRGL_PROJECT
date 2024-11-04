using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private GameManager gameManager;

    private float speed = 10;
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        moveLeft();
        destroyObstacle();
    }

    void moveLeft()
    {
        if (gameManager.isGameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            UpdateSpeed();
        }
    }

    void UpdateSpeed()
    {
        float score = gameManager.getScore();
        float newSpeed = speed;

        if (score >= 600) newSpeed = 11f;
        else if (score >= 400) newSpeed = 10.5f;
        else if (score >= 200) newSpeed = 10f;
    }

    void destroyObstacle()
    {
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
