using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsGUI : MonoBehaviour
{
    AudioSource musicPlayer;
    [SerializeField] Canvas inGameMenu , inGame, quitMenu;
    [SerializeField] GameObject transition;
    [SerializeField] float transitionTime = 1.1f;

    bool isEnabled = false;

    void Awake()
    {
        inGameMenu.enabled = false;
        quitMenu.enabled = false;
        musicPlayer = GameObject.Find("Music Player").GetComponent<AudioSource>();
    }

    public void PauseGame()
    {
        if (!isEnabled)
        {
            AudioManager.instance.Play("Pause Sound");
            musicPlayer.Pause();
            Time.timeScale = 0;
            inGameMenu.enabled = true;
            inGame.enabled = false;
            isEnabled = true;
        }
    }
    public void ResumeGame()
    {
        if (isEnabled) {
            AudioManager.instance.Play("Resume Sound");
            musicPlayer.UnPause();
            Time.timeScale = 1;
            inGame.enabled = true;
            inGameMenu.enabled = false;
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
        inGameMenu.enabled = false;
        quitMenu.enabled = true;
    }

    public void QuitGame()
    {
        transition.SetActive(true);
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

    public void ReturnToInGameMenu()
    {
        quitMenu.enabled = false;
        inGameMenu.enabled = true;
    }

}
