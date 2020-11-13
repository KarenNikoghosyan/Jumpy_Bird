using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsGUI : MonoBehaviour
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
            Time.timeScale = 0;
            inGameMenu.gameObject.SetActive(true);
            inGame.gameObject.SetActive(false);
            isEnabled = true;
        }
    }
    public void ResumeGame()
    {
        if (isEnabled) {
            AudioManager.instance.Play("Resume Sound");
            musicPlayer.UnPause();
            Time.timeScale = 1;
            inGame.gameObject.SetActive(true);
            inGameMenu.gameObject.SetActive(false);
            isEnabled = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        musicPlayer.Stop();
        musicPlayer.Play();
        Time.timeScale = 1;
    }

    public void Home()
    {
        inGameMenu.gameObject.SetActive(false);
        quitMenu.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        transition.SetActive(true);
        StartCoroutine(LoadMenu());
        Time.timeScale = 1;
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
            isDead = false;
        }
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);
    }

    public void ReturnToInGameMenu()
    {
        quitMenu.gameObject.SetActive(false);
        inGameMenu.gameObject.SetActive(true);
    }

}
