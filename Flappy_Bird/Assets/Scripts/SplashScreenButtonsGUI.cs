using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;
using TMPro;

public class SplashScreenButtonsGUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] Canvas splashButtons;
    [SerializeField] Canvas quitMenu;
    [SerializeField] Canvas settingsMenu;

    [Header("transition Animation")]
    [SerializeField] GameObject transition;
    [SerializeField] float transitionTime = 2f;

    [Header("Settings Menu")]
    [SerializeField] SliderManager sliderManager;
    [SerializeField] Button muteButton;
    [SerializeField] Sprite audioOff, audioOn;
    [SerializeField] TextMeshProUGUI muteText;
    
    bool audioSwitcher = true;
    
    AudioSource audioSource;

    void Start()
    {
        MusicVolumeSlider();
        SoundSetting();
    }

    private void MusicVolumeSlider()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        audioSource = GameObject.Find("Music Player").GetComponent<AudioSource>();
        sliderManager.onValueChanged.AddListener(delegate { VolumeSlider(); });
        //Gets the volume slider value and volume
        audioSource.volume = PlayerPrefs.GetFloat("SliderVolume");
        //Changes the slider value
        sliderManager.mainSlider.value = PlayerPrefs.GetFloat("SliderVolume");
    }

    private void SoundSetting()
    {
        //Gets the mute or unmute setting
        audioSwitcher = PlayerPrefs.GetInt("Mute SFX") == 1 ? true : false;
        //Turns the sound on or off depends on the saved setting
        MuteButton();
    }

    public void StartGame()
    {
        AudioManager.instance.Play("Start Game Sound");
        transition.SetActive(true);
        Invoke("LoadLevel", transitionTime);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenQuitMenu()
    {
        AudioManager.instance.Play("Click Sound");
        splashButtons.gameObject.SetActive(false);
        quitMenu.gameObject.SetActive(true);
        quitMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseQuitMenu()
    {
        AudioManager.instance.Play("Click Sound");
        quitMenu.GetComponentInChildren<Animator>().SetBool("open", false);
        StartCoroutine(CloseQuitMenuDelay());
    }

    IEnumerator CloseQuitMenuDelay()
    {
        yield return new WaitForSeconds(0.4f);
        quitMenu.gameObject.SetActive(false);
        splashButtons.gameObject.SetActive(true);
    }

    public void SettingsButton()
    {
        splashButtons.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }

    public void CloseSettingsMenuButton()
    {
        settingsMenu.gameObject.SetActive(false);
        splashButtons.gameObject.SetActive(true);
        //Saves the volume setting
        PlayerPrefs.SetFloat("SliderVolume", audioSource.volume);
    }  
  
    public void VolumeSlider()
    {
        // Invoked when the value of the slider changes.
        audioSource.volume = sliderManager.mainSlider.value;
    }

    //Toggles the mute button
    public void MuteButton()
    {
        if (audioSwitcher)
        {
            MuteSound();
        }

        else
        {
            UnmuteSound();
        }
    }


    private void MuteSound()
    {
        muteButton.GetComponent<Image>().sprite = audioOff;
        muteText.text = "Unmute";
        AudioListener.volume = 0f;
        audioSwitcher = false;
        //Saves the mute or unmute setting
        PlayerPrefs.SetInt("Mute SFX", 1);
    }
    private void UnmuteSound()
    {
        muteButton.GetComponent<Image>().sprite = audioOn;
        muteText.text = "Mute";
        AudioListener.volume = 1f;
        audioSwitcher = true;
        //Saves the mute or unmute setting
        PlayerPrefs.SetInt("Mute SFX", 0);
    }
}