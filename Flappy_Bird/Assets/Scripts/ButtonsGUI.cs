using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsGUI : MonoBehaviour
{
    AudioSource musicPlayer;
    [SerializeField] Canvas inGameMenu , inGame;

    bool isEnabled = false;

    void Awake()
    {
        inGameMenu.enabled = false;
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
        SceneManager.LoadScene(0);
        musicPlayer.Stop();
        musicPlayer.Play();
        Time.timeScale = 1;
    }
}
