using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonsGUI : MonoBehaviour
{
    AudioSource musicPlayer;
    [SerializeField] Canvas inGameMenu , inGame, quitMenu, gameOverMenu;
    [SerializeField] GameObject transition;
    [SerializeField] float transitionTime = 1.1f;

    bool isEnabled = false;

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
        SceneManager.LoadScene(1);
        musicPlayer.Stop();
        musicPlayer.Play();
        Time.timeScale = 1;
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
        transition.SetActive(true);
        AudioManager.instance.Play("Click Sound");
        StartCoroutine(LoadMenu());
        Time.timeScale = 1;
        musicPlayer.Stop();
        musicPlayer.Play();
    }
    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);
    }

    public void GameOverMenu(bool isDead)
    {
        if (isDead)
        {
            Time.timeScale = 0;
            inGame.gameObject.SetActive(false);
            gameOverMenu.gameObject.SetActive(true);
            gameOverMenu.GetComponentInChildren<Animator>().SetBool("open", true);
            isDead = false;
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
