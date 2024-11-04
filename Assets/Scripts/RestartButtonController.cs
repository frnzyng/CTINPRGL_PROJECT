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
        restartButton.onClick.AddListener(() => StartCoroutine(OnClick()));

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = buttonClickAudio;
    }

    private IEnumerator OnClick()
    {
        audioSource.PlayOneShot(buttonClickAudio);
        yield return new WaitForSeconds(0.4f);
        gameManager.RestartGame();
    }
}
