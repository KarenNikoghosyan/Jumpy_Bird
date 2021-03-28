using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;

public class ThemeManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private HorizontalSelector horizontalSelector;
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
        horizontalSelector.ForwardClick();
        i++;

        if (i == audioClips.Length)
        {
            i = 0;
            horizontalSelector.index = i;
        }
        
        audioSource.clip = audioClips[i];
        audioSource.Play();
        horizontalSelector.index = i;
        
        PlayerPrefs.SetInt(Constants.THEME_INDEX, i);
    }

    public void LeftArrow()
    {
        horizontalSelector.PreviousClick();
        i--;

        if (i == -1)
        {
            i = audioClips.Length - 1;
            horizontalSelector.index = i;
        }
        
        audioSource.clip = audioClips[i];
        audioSource.Play();
        horizontalSelector.index = i;
        
        PlayerPrefs.SetInt(Constants.THEME_INDEX, i);
    }

    public void ChangeThemeText()
    {
        horizontalSelector.defaultIndex = i;
    }
}