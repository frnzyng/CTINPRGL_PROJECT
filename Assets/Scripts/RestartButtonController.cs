using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButtonController : MonoBehaviour
{
    private GameManager gameManager;
    public Button restartButton;
    public AudioClip buttonClickAudio;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        restartButton = GetComponent<Button>();
        restartButton.onClick.AddListener(OnClick);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = buttonClickAudio;
    }

    private void OnClick()
    {
        audioSource.PlayOneShot(buttonClickAudio);
        
        gameManager.RestartGame();
    }
}
