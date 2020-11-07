using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsGUI : MonoBehaviour
{
    AudioSource musicPlayer;

    public void PauseGame()
    {
        AudioManager.instance.Play("Pause Sound");
        musicPlayer = GameObject.Find("Music Player").GetComponent<AudioSource>();
        musicPlayer.Stop();
        Time.timeScale = 0;
    }

}
