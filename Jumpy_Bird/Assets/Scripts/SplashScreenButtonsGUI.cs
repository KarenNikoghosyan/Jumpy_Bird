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
    [SerializeField] private Canvas splashButtons;
    [SerializeField] private Canvas quitMenu;
    [SerializeField] private Canvas settingsMenu;

    [Header("Transition Animation")]
    [SerializeField] private GameObject transition;

    [Header("Volume Slider Setting")]
    [SerializeField] private SliderManager sliderManager;

    [Header("Sound Setting")]
    [SerializeField] private Button muteButton;
    [SerializeField] private Sprite audioOff, audioOn;
    [SerializeField] private TextMeshProUGUI muteText;

    [Header("Battery Mode Setting")]
    [SerializeField] private Toggle batteryToggle;
    [SerializeField] private TextMeshProUGUI onOffToggle;
    
    [Header("In Game Debug Console")]
    [SerializeField] private GameObject inGameDebugConsole;
    
    bool audioSwitcher = true;
    bool BatteryMode = false;
    bool isPlaying = false;
    
    AudioSource musicPlayer;

    private void Start()
    {
        MusicVolumeSlider();
        SoundSetting();
        BatterySetting();
    }
    
    private void MusicVolumeSlider()
    {
        musicPlayer = GameObject.Find("Music Player").GetComponent<AudioSource>();
        //Adds a listener to the main slider and invokes a method when the value changes.
        sliderManager.onValueChanged.AddListener(delegate { VolumeSlider(); });
        //Checks if Slidervolume is null, if there's no key it uses a default value
        if (!PlayerPrefs.HasKey("SliderVolume")) { PlayerPrefs.SetFloat("SliderVolume", 0.15f); }
        //Gets the volume slider value and volume
        musicPlayer.volume = PlayerPrefs.GetFloat("SliderVolume");
        //Changes the slider value
        sliderManager.mainSlider.value = PlayerPrefs.GetFloat("SliderVolume");
    }

    public void VolumeSlider()
    {
        // Invoked when the value of the slider changes.
        musicPlayer.volume = sliderManager.mainSlider.value;
    }

    private void SoundSetting()
    {
        //Gets the mute or unmute setting
        audioSwitcher = PlayerPrefs.GetInt("Mute SFX") == 1 ? true : false;
        //Toggles the sound on or off depends on the saved setting
        MuteButton();
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

    private void BatterySetting()
    {
        //Gets the current battery toggle mode
        BatteryMode = PlayerPrefs.GetInt("Battery Toggle") == 1 ? true : false;
        //Toggles the battery setting depending on the saved setting
        BatteryModeToggle();
    }

    //Toggles the battery mode
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
        onOffToggle.text = "ON";
        Application.targetFrameRate = 30;
        BatteryMode = false;
        PlayerPrefs.SetInt("Battery Toggle", 1);
    }

    private void BatteryModeOff()
    {
        batteryToggle.isOn = false;
        onOffToggle.text = "OFF";
        Application.targetFrameRate = 60;
        BatteryMode = true;
        PlayerPrefs.SetInt("Battery Toggle", 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenQuitMenu();
        }

        if (Debug.isDebugBuild)
        {
            DebugConsole();
        }
    }
    
    private void DebugConsole()
    {
        if (inGameDebugConsole == null) return;
        
        inGameDebugConsole.SetActive(true);
    }

    public void StartGame()
    {
        if (isPlaying) { return; }
        AudioManager.instance.Play("Start Game Sound");
        transition.SetActive(true);
        isPlaying = true;
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
        AudioManager.instance.Play("Click Sound");
        splashButtons.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
        settingsMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }

    public void CloseSettingsMenuButton()
    {
        AudioManager.instance.Play("Click Sound");
        //Saves the volume setting
        PlayerPrefs.SetFloat("SliderVolume", musicPlayer.volume);
        settingsMenu.GetComponentInChildren<Animator>().SetBool("open", false);
        StartCoroutine(CloseSettingsMenuDelay());
    }

    IEnumerator CloseSettingsMenuDelay()
    {
        yield return new WaitForSeconds(0.4f);
        settingsMenu.gameObject.SetActive(false);
        splashButtons.gameObject.SetActive(true);
    }
}