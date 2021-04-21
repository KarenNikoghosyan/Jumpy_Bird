using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Michsky.UI.ModernUIPack;
using System;
using MText;
using UnityEngine.UI;

public class GameButtonsGUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas inGame;
    [SerializeField] private Canvas inGameMenu;
    [SerializeField] private Canvas quitMenu;
    [SerializeField] private Canvas gameOverMenu;
    [SerializeField] private Canvas settingsMenu;

    [Header("Transition Animation")]
    [SerializeField] private GameObject transition1;
    [SerializeField] private GameObject transition2;

    [Header("Score")] 
    [SerializeField] private Modular3DText scoreUI;
    [SerializeField] private Modular3DText speedMessageUI;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI currentMenuScore;

    [Header("Volume Slider Setting")]
    [SerializeField] private SliderManager sliderManager;

    [Header("Input Field")] 
    [SerializeField] private TMP_InputField currentlyPlaying;

    bool isEnabled = false;
    private PlayerCollisionHandler playerCollisionHandler;

    AudioSource musicPlayer;

    private void Awake()
    {
        playerCollisionHandler = FindObjectOfType<PlayerCollisionHandler>();
        MusicVolumeSlider();
    }
    
    private void MusicVolumeSlider()
    {
        musicPlayer = GameObject.Find("Music Player").GetComponent<AudioSource>();
        musicPlayer.volume = PlayerPrefs.GetFloat(Constants.SLIDER_VOLUME);
        sliderManager.mainSlider.value = PlayerPrefs.GetFloat(Constants.SLIDER_VOLUME);
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
            AudioManager.instance.Play(Constants.PAUSE_GAME_SOUND);
            
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
        if (isEnabled) 
        {
            AudioManager.instance.Play(Constants.RESUME_GAME_SOUND);
            
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
        SaveHighScore();
        AudioManager.instance.Play(Constants.CLICK_SOUND);
        transition2.SetActive(true);
    }

    public void HomeButton()
    {
        AudioManager.instance.Play(Constants.CLICK_SOUND);
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
        AudioManager.instance.Play(Constants.CLICK_SOUND);
        quitMenu.gameObject.SetActive(false);
        inGameMenu.gameObject.SetActive(true);
        inGameMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }

    public void SettingsButton()
    {
        AudioManager.instance.Play(Constants.CLICK_SOUND);
        inGameMenu.GetComponentInChildren<Animator>().SetBool("open", false);
        StartCoroutine(SettingsButtonDelay());

        currentlyPlaying.text = PlayerPrefs.GetString(Constants.CURRENTLY_PLAYING);
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
        PlayerPrefs.SetFloat(Constants.SLIDER_VOLUME, musicPlayer.volume);
        AudioManager.instance.Play(Constants.CLICK_SOUND);
        settingsMenu.gameObject.SetActive(false);
        inGameMenu.gameObject.SetActive(true);
        inGameMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }

    public void QuitGame()
    {
        SaveHighScore();
        AudioManager.instance.Play(Constants.CLICK_SOUND);
        transition1.SetActive(true);
    }

    public void SaveHighScore()
    {
        int score = playerCollisionHandler.GetScore();
        
        if (score > PlayerPrefs.GetInt(Constants.HIGHSCORE))
        {
            PlayerPrefs.SetInt(Constants.HIGHSCORE, score);
        }
    }
    
    public void GameOverMenu(bool isDead)
    {
        if (isDead)
        {
            Time.timeScale = 0;
            inGame.gameObject.SetActive(false);
            gameOverMenu.gameObject.SetActive(true);
            gameOverMenu.GetComponentInChildren<Animator>().SetBool("open", true);
            
            ShowHighScore();
            isDead = false;
        }
    }

    private void ShowHighScore()
    {
        int score = playerCollisionHandler.GetScore();
        currentScore.text = score.ToString();

        if (score > PlayerPrefs.GetInt(Constants.HIGHSCORE))
        {
            PlayerPrefs.SetInt(Constants.HIGHSCORE, score);
            highscoreText.text = score.ToString();
        }

        else
        {
            highscoreText.text = PlayerPrefs.GetInt(Constants.HIGHSCORE).ToString();
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
        scoreUI.gameObject.SetActive(false);
        
        speedMessageUI.gameObject.SetActive(true);
        speedMessageUI.GetComponent<Typewriter>().StartTyping();

        yield return new WaitForSecondsRealtime(2f);
        
        speedMessageUI.gameObject.SetActive(false);
        speedMessageUI.UpdateText("");
        
        inGame.gameObject.SetActive(true);
        scoreUI.gameObject.SetActive(true);
    }
}