using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Michsky.UI.ModernUIPack;

public class SplashScreenButtonsGUI : MonoBehaviour
{
    [SerializeField] Canvas splashButtons, quitMenu, settingsMenu;
    [SerializeField] GameObject transition;
    [SerializeField] float transitionTime = 2f, musicVolume;
    [SerializeField] AudioSource audioSource;
    [SerializeField] SliderManager sliderManager;

    void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        sliderManager.onValueChanged.AddListener(delegate { VolumeSlider(); });  
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
    }
    
  
    public void VolumeSlider()
    {
        // Invoked when the value of the slider changes.
        audioSource.volume = sliderManager.mainSlider.value;
    }
}