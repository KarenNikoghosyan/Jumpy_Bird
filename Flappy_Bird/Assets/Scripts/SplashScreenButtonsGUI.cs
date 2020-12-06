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

    [Header("Volume Slider Setting")]
    [SerializeField] SliderManager sliderManager;

    [Header("Sound Setting")]
    [SerializeField] Button muteButton;
    [SerializeField] Sprite audioOff, audioOn;
    [SerializeField] TextMeshProUGUI muteText;

    [Header("Battery Mode Setting")]
    [SerializeField] Toggle batteryToggle;
    
    bool audioSwitcher = true;
    bool BatteryMode = false;
    
    AudioSource audioSource;

    void Start()
    {
        MusicVolumeSlider();
        SoundSetting();
        BatterySetting();
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
        //Toggles the sound on or off depends on the saved setting
        MuteButton();
    }

    private void BatterySetting()
    {
        //Gets the current battery toggle mode
        BatteryMode = PlayerPrefs.GetInt("Battery Toggle") == 1 ? true : false;
        //Toggles the battery setting depending on the saved setting
        BatteryModeToggle();
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
        settingsMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }

    public void CloseSettingsMenuButton()
    {
        //Saves the volume setting
        PlayerPrefs.SetFloat("SliderVolume", audioSource.volume);
        settingsMenu.GetComponentInChildren<Animator>().SetBool("open", false);
        StartCoroutine(CloseSettingsMenuDelay());
    }

    IEnumerator CloseSettingsMenuDelay()
    {
        yield return new WaitForSeconds(0.4f);
        settingsMenu.gameObject.SetActive(false);
        splashButtons.gameObject.SetActive(true);
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

    public void BatteryModeToggle()
    {
        if (BatteryMode)
        {
            BatteryModeOn();
        }

        else
        {
            BatteryModeOff();
        }
    }

    private void BatteryModeOn()
    {
        batteryToggle.isOn = true;
        Application.targetFrameRate = 30;
        BatteryMode = false;
        PlayerPrefs.SetInt("Battery Toggle", 1);
        print("30");
    }
    private void BatteryModeOff()
    {
        batteryToggle.isOn = false;
        Application.targetFrameRate = 60;
        BatteryMode = true;
        PlayerPrefs.SetInt("Battery Toggle", 0);
        print("60");
    }
}