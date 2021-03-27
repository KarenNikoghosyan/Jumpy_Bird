using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private TMP_InputField themeText;
    [SerializeField] private AudioClip[] audioClips = new AudioClip[15];

    private int i;
    private static bool isPlaying = false;

    private void Start()
    {
        audioSource = GameObject.Find("Music Player").GetComponent<AudioSource>();
        i = !PlayerPrefs.HasKey(Constants.THEME_INDEX) ? 0 : PlayerPrefs.GetInt(Constants.THEME_INDEX);
        
        if (isPlaying) return;
       
        audioSource.clip = audioClips[i];
        audioSource.Play();
        isPlaying = true;
    }

    public void RightArrow()
    {
        i++;

        if (i == audioClips.Length)
        {
            i = 0;
            themeText.text = "Theme " + (i + 1);
        }
        
        audioSource.clip = audioClips[i];
        audioSource.Play();
        themeText.text = "Theme " + (i + 1);
    }

    public void LeftArrow()
    {
        i--;

        if (i == -1)
        {
            i = audioClips.Length - 1;
            themeText.text = "Theme " + (i + 1);
        }
        
        audioSource.clip = audioClips[i];
        audioSource.Play();
        themeText.text = "Theme " + (i + 1);
    }

    public int GetIndex()
    {
        return i;
    }
}