using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    private GameManager gameManager;
    public Button startButton;
    public AudioClip buttonClickAudio;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(() => StartCoroutine(OnClick()));

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = buttonClickAudio;
    }

    private IEnumerator OnClick()
    {
        audioSource.PlayOneShot(buttonClickAudio);
        yield return new WaitForSeconds(audioSource.clip.length);
        gameManager.startGame();
    }
}
