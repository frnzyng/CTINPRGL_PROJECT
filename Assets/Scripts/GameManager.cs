using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // PLAYER
    public GameObject player;
    private float playerPos;

    // OBSTACLE
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(15, -4, -1);
    private float startDelay = 2f;
    private float repeatRate = 2f;

    // SCORE
    public TextMeshProUGUI scoreText;
    private float score = 0f; 
    private float scoreMultiplier = 15f;  // Multiplier to control score speed

    // ENDING
    public GameObject endingScreen;
    public GameObject deadCat;
    private float deadCatPos;
    private Animator animator;
    private bool isDeadCatSpawned = false;
    private float counter = 0f;

    // START, ABOUT, & GAME OVER SCREEN
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public GameObject aboutScreen;
    private bool isAboutActive = false;
    public bool isGameActive = false;
    public bool isGameOver = false;

    // AUDIO
    public AudioClip menuGameAudio;
    public AudioClip inGameAudio;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        PlayMenuGameAudio();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position.x;
        deadCatPos = deadCat.transform.position.x;
            
        // OBSTACLE
        UpdateSpawnRate();

        // SCORE
        if (isGameActive && !isGameOver)
        {
            UpdateScore();
            UpdateSpawnRate();
        }
    }

    // AUDIO
    public void PlayMenuGameAudio()
    {
        audioSource.clip = menuGameAudio;
        audioSource.Play();
    }

    public void PlayInGameAudio()
    {
        audioSource.clip = inGameAudio;
        audioSource.loop = true;
        audioSource.Play();
    }

    // OBSTACLE
    void SpawnObstacle()
    {
        if (isGameActive && !isGameOver)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }

    void UpdateSpawnRate()
    {
        float newRate = repeatRate;

        if (score >= 500) newRate = 1.25f;
        else if (score >= 300) newRate = 1.5f;
        else if (score >= 100) newRate = 2f;

        // Check if the spawn rate needs updating
        if (newRate != repeatRate)
        {
            repeatRate = newRate;
            CancelInvoke("SpawnObstacle");
            InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        }

        if (score >= 999 && isGameActive && !isGameOver)
        {
            CancelInvoke("SpawnObstacle");
            secretEnding();
        }
    }

    // SCORE
    public float getScore()
    {
        return score;
    }
    
    void UpdateScore()
    {
        // Increase score based on time or distance
        if (score <= 999 && isGameActive && !isGameOver)
        {        
            score += Time.deltaTime * scoreMultiplier;
        }

        // Update the score text display
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    public void ResetScore()
    {
        score = 0;
    }

    // ETC
    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        isGameOver = true;
    }

    public float getCount()
    {
        return counter;
    }

    public float getPlayerPos()
    {
        return deadCatPos;
    }

    public float getDeadCatPos()
    {
        return deadCatPos;
    }

    void secretEnding()
    {
        if (counter < 10)
        {
            counter += Time.deltaTime;
            Debug.Log(counter);
        }

        if (counter >= 10 && !isDeadCatSpawned)
        {
            //deadCat.animator.SetTrigger();
            deadCat.gameObject.SetActive(true);
            isDeadCatSpawned = true;
        }
        if (playerPos <= -1 && playerPos >= -3)
        {
            endGame();
        }
    }

    public void endGame()
    {
        // Start the delay coroutine to show the ending screen
        StartCoroutine(ShowEndingScreenWithDelay(3f)); // 2 seconds delay
    }

    private IEnumerator ShowEndingScreenWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Activate or display your ending screen UI
        endingScreen.gameObject.SetActive(true);
    }

    public void startGame()
    {
        startScreen.gameObject.SetActive(false);
        isGameActive = true;
        scoreText.gameObject.SetActive(true);

        audioSource.Stop();
        PlayInGameAudio();

        // OBSTACLE
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void toggleAbout()
    {
        if (!isAboutActive)
        {
            startScreen.gameObject.SetActive(false);
            aboutScreen.gameObject.SetActive(true);
            isAboutActive = true;
        }
        else
        {
            aboutScreen.gameObject.SetActive(false);
            startScreen.gameObject.SetActive(true);
            isAboutActive = false;
        }
    }
}
