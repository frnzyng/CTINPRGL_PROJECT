using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutButton : MonoBehaviour
{
    private GameManager gameManager;
    public Button aboutButton;
    public AudioClip buttonClickAudio;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        aboutButton = GetComponent<Button>();
        aboutButton.onClick.AddListener(() => StartCoroutine(OnClick()));

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = buttonClickAudio;
    }

    private IEnumerator OnClick()
    {
        audioSource.PlayOneShot(buttonClickAudio);
        yield return new WaitForSeconds(audioSource.clip.length);
        gameManager.toggleAbout();
    }
}
