using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Michsky.UI.ModernUIPack;
using System;

public class GameButtonsGUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas inGame;
    [SerializeField] private Canvas inGameMenu;
    [SerializeField] private Canvas quitMenu;
    [SerializeField] private Canvas gameOverMenu;
    [SerializeField] private Canvas settingsMenu;
    [SerializeField] private Canvas speedText;
    [SerializeField] private Canvas highScoreText;

    [Header("Transition Animation")]
    [SerializeField] private GameObject transition1;
    [SerializeField] private GameObject transition2;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI currentMenuScore;

    [Header("Volume Slider Setting")]
    [SerializeField] private SliderManager sliderManager;

    bool isEnabled = false;
    int highscore = 0;
    float textDelay = 1.5f;

    AudioSource musicPlayer;

    private void Awake()
    {
        MusicVolumeSlider();
    }

    private void MusicVolumeSlider()
    {
        musicPlayer = GameObject.Find("Music Player").GetComponent<AudioSource>();
        musicPlayer.volume = PlayerPrefs.GetFloat("SliderVolume");
        sliderManager.mainSlider.value = PlayerPrefs.GetFloat("SliderVolume");
        sliderManager.onValueChanged.AddListener(delegate { VolumeSlider(); });
    }

    public void VolumeSlider()
    {
        // Invoked when the value of the slider changes.
        musicPlayer.volume = sliderManager.mainSlider.value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }    
    }

    public void PauseGame()
    {
        if (!isEnabled)
        {
            AudioManager.instance.Play("Pause Sound");
            Time.timeScale = 0; ;
            inGameMenu.gameObject.SetActive(true);
            ShowCurrentScore();
            inGame.gameObject.SetActive(false);
            inGameMenu.GetComponentInChildren<Animator>().SetBool("open", true);
            isEnabled = true;
        }
    }

    private void ShowCurrentScore()
    {
        int score = FindObjectOfType<PlayerCollisionHandler>().GetScore();
        currentMenuScore.text = score.ToString();
    }

    public void ResumeGameButton()
    {
        if (isEnabled) {
            AudioManager.instance.Play("Resume Sound");
            Time.timeScale = 1;
            inGameMenu.GetComponentInChildren<Animator>().SetBool("open", false);
            StartCoroutine(ResumeGameAnimator());
            isEnabled = false;
        }
    }

    IEnumerator ResumeGameAnimator()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        inGame.gameObject.SetActive(true);
        inGameMenu.gameObject.SetActive(false);
    }

    public void RestartGameButton()
    {
        AudioManager.instance.Play("Click Sound");
        transition2.SetActive(true);
    }

    public void HomeButton()
    {
        AudioManager.instance.Play("Click Sound");
        inGameMenu.GetComponentInChildren<Animator>().SetBool("open", false);
        StartCoroutine(OpenQuitMenuDelay());
    }
    IEnumerator OpenQuitMenuDelay()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        inGameMenu.gameObject.SetActive(false);
        quitMenu.gameObject.SetActive(true);
        quitMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }
    public void CloseQuitMenu()
    {
        AudioManager.instance.Play("Click Sound");
        quitMenu.gameObject.SetActive(false);
        inGameMenu.gameObject.SetActive(true);
        inGameMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }

    public void SettingsButton()
    {
        AudioManager.instance.Play("Click Sound");
        inGameMenu.GetComponentInChildren<Animator>().SetBool("open", false);
        StartCoroutine(SettingsButtonDelay());
    }

    IEnumerator SettingsButtonDelay()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        inGameMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
        settingsMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }

    public void CloseSettingsButton()
    {
        PlayerPrefs.SetFloat("SliderVolume", musicPlayer.volume);
        AudioManager.instance.Play("Click Sound");
        settingsMenu.gameObject.SetActive(false);
        inGameMenu.gameObject.SetActive(true);
        inGameMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }

    public void QuitGame()
    {
        AudioManager.instance.Play("Click Sound");
        transition1.SetActive(true);
    }


    public void GameOverMenu(bool isDead)
    {
        if (isDead)
        {
            Time.timeScale = 0;
            inGame.gameObject.SetActive(false);
            gameOverMenu.gameObject.SetActive(true);
            gameOverMenu.GetComponentInChildren<Animator>().SetBool("open", true);
            HighScore();
            isDead = false;
        }
    }

    private void HighScore()
    {
        int score = FindObjectOfType<PlayerCollisionHandler>().GetScore();
        currentScore.text = score.ToString();

        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
            highscoreText.text = highscore.ToString();
        }

        else
        {
            highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
        }
    }

    public void ShowSpeedText(bool isSpeed)
    {
        StartCoroutine(SpeedText());
        isSpeed = false;
    }

    IEnumerator SpeedText()
    {
        inGame.gameObject.SetActive(false);
        speedText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(textDelay);
        speedText.gameObject.SetActive(false);
        inGame.gameObject.SetActive(true);
    }

    public void ShowHighScoreText(bool isHighScore)
    {
        StartCoroutine(HighScoreText());
        isHighScore = false;
    }

    IEnumerator HighScoreText()
    {
        inGame.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(textDelay);
        highScoreText.gameObject.SetActive(false);
        inGame.gameObject.SetActive(true);
    }

}
