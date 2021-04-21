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
    [SerializeField] private Canvas creditsMenu;

    [Header("Transition Animation")]
    [SerializeField] private GameObject transition;

    [Header("Volume Slider Setting")]
    [SerializeField] private SliderManager sliderManager;

    [Header("Sound Setting")]
    [SerializeField] private Button muteButton;
    [SerializeField] private Sprite audioOff, audioOn;
    [SerializeField] private TextMeshProUGUI muteText;

    [Header("Battery Mode Setting")] 
    [SerializeField] private SwitchManager switchManager;

    [Header("In Game Debug Console")]
    [SerializeField] private GameObject inGameDebugConsole;

    bool audioSwitcher = true;
    
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
        if (!PlayerPrefs.HasKey(Constants.SLIDER_VOLUME)) { PlayerPrefs.SetFloat(Constants.SLIDER_VOLUME, 0.35f); }
        //Gets the volume slider value and volume
        musicPlayer.volume = PlayerPrefs.GetFloat(Constants.SLIDER_VOLUME);
        //Changes the slider value
        sliderManager.mainSlider.value = PlayerPrefs.GetFloat(Constants.SLIDER_VOLUME);
    }

    public void VolumeSlider()
    {
        // Invoked when the value of the slider changes.
        musicPlayer.volume = sliderManager.mainSlider.value;
    }

    private void SoundSetting()
    {
        //Gets the mute or unmute setting
        audioSwitcher = PlayerPrefs.GetInt(Constants.MUTE_SFX) == 1 ? true : false;
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
        PlayerPrefs.SetInt(Constants.MUTE_SFX, 1);
    }
    private void UnmuteSound()
    {
        muteButton.GetComponent<Image>().sprite = audioOn;
        muteText.text = "Mute";
        AudioListener.volume = 1f;
        audioSwitcher = true;
        
        //Saves the mute or unmute setting
        PlayerPrefs.SetInt(Constants.MUTE_SFX, 0);
    }

    private void BatterySetting()
    {
        Application.targetFrameRate = !PlayerPrefs.HasKey(Constants.FRAMERATES) ? 60 : PlayerPrefs.GetInt(Constants.FRAMERATES);
        //Gets the current battery toggle mode
        switchManager.isOn = PlayerPrefs.GetInt(Constants.BATTERY_TOGGLE) == 1 ? true : false;
    }
    
    public void BatteryModeOn()
    {
        Application.targetFrameRate = 30;
        PlayerPrefs.SetInt(Constants.FRAMERATES, 30);
        PlayerPrefs.SetInt(Constants.BATTERY_TOGGLE, 1);
    }

    public void BatteryModeOff()
    {
        Application.targetFrameRate = 60;
        PlayerPrefs.SetInt(Constants.FRAMERATES, 60);
        PlayerPrefs.SetInt(Constants.BATTERY_TOGGLE, 0);
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
        AudioManager.instance.Play(Constants.START_GAME_SOUND);
        transition.SetActive(true);
        isPlaying = true;
    }

    public void OpenQuitMenu()
    {
        AudioManager.instance.Play(Constants.CLICK_SOUND);
        
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
        AudioManager.instance.Play(Constants.CLICK_SOUND);
        
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
        AudioManager.instance.Play(Constants.CLICK_SOUND);
        
        splashButtons.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
        
        settingsMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }

    public void CloseSettingsMenuButton()
    {
        AudioManager.instance.Play(Constants.CLICK_SOUND);
        
        //Saves the volume setting
        PlayerPrefs.SetFloat(Constants.SLIDER_VOLUME, musicPlayer.volume);
        
        settingsMenu.GetComponentInChildren<Animator>().SetBool("open", false);
        StartCoroutine(CloseSettingsMenuDelay());
    }

    IEnumerator CloseSettingsMenuDelay()
    {
        
        yield return new WaitForSeconds(0.4f);
        settingsMenu.gameObject.SetActive(false);
        splashButtons.gameObject.SetActive(true);
    }

    public void OpenCreditsMenu()
    {
        AudioManager.instance.Play(Constants.CLICK_SOUND);
        
        splashButtons.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(true);
        
        creditsMenu.GetComponentInChildren<Animator>().SetBool("open", true);
    }

    public void CloseCreditsMenu()
    {
        AudioManager.instance.Play(Constants.CLICK_SOUND);
        
        creditsMenu.GetComponentInChildren<Animator>().SetBool("open", false);
        StartCoroutine(CloseCreditsMenuDelay());
    }

    IEnumerator CloseCreditsMenuDelay()
    {
        yield return new WaitForSeconds(0.4f);
        
        creditsMenu.gameObject.SetActive(false);
        splashButtons.gameObject.SetActive(true);
    }
}