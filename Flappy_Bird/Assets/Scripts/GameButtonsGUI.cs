using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameButtonsGUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] Canvas inGameMenu;
    [SerializeField] Canvas inGame;
    [SerializeField] Canvas quitMenu;
    [SerializeField] Canvas gameOverMenu;

    [Header("Transition Animation")]
    [SerializeField] GameObject transition1;
    [SerializeField] GameObject transition2;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] TextMeshProUGUI currentScore;

    bool isEnabled = false;
    int highscore = 0, score = 0;

    AudioSource musicPlayer;

    void Awake()
    {
            musicPlayer = GameObject.Find("Music Player").GetComponent<AudioSource>();
    }

    public void PauseGame()
    {
        if (!isEnabled)
        {
            AudioManager.instance.Play("Pause Sound");
            musicPlayer.Pause();
            Time.timeScale = 0; ;
            inGameMenu.gameObject.SetActive(true);
            inGame.gameObject.SetActive(false);
            inGameMenu.GetComponentInChildren<Animator>().SetBool("open", true);
            isEnabled = true;
        }
    }

    public void ResumeGameButton()
    {
        if (isEnabled) {
            AudioManager.instance.Play("Resume Sound");
            musicPlayer.UnPause();
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
        musicPlayer.Stop();
        musicPlayer.Play();
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

    public void QuitGame()
    {
        transition1.SetActive(true);

        AudioManager.instance.Play("Click Sound");
        musicPlayer.Stop();
        musicPlayer.Play();
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
        score = FindObjectOfType<PlayerCollisionHandler>().score;
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


    public void ReturnToInGameMenu()
    {
        AudioManager.instance.Play("Click Sound");
        quitMenu.gameObject.SetActive(false);
        inGameMenu.gameObject.SetActive(true);
        inGameMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }

}
