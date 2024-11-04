using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private GameManager gameManager;

    // BACKGROUND
    public GameObject background1;
    public GameObject background2;
    private Vector3 background1StartPosition;  // Initial position of image 1
    private Vector3 background2StartPosition;  // Initial position of image 2
    private float scrollSpeed = 5; // Speed to scroll the background
    public float resetPositionX; // The position at which the background should reset

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Get the width of the background sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the starting position of the background
        background1StartPosition = background1.transform.position;
        background2StartPosition = background2.transform.position;
    }

    void Update()
    {
        // Move the background to the left
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Check if the background has moved to the reset position (-39.6x & 40x)
        if (background1.transform.position.x <= resetPositionX)
        {
            // Reset the background's position back to the starting point
            background1.transform.position = new Vector3(background1StartPosition.x, transform.position.y, transform.position.z);
            background2.transform.position = new Vector3(background2StartPosition.x, transform.position.y, transform.position.z);
        }

        // Stop he background when game over
        if (gameManager.isGameOver == true)
        {
            scrollSpeed = 0;
        }

        if (gameManager.getCount() >= 10)
        {
            scrollSpeed = 2;

            // Stops when dead cat reaches at the center screen
            if (gameManager.getDeadCatPos() <= 0.01f)
            {
                scrollSpeed = 0;
            }
        }
    }
}
