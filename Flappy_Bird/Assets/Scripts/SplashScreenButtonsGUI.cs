using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenButtonsGUI : MonoBehaviour
{
    [SerializeField] Canvas splashButtons, quitMenu;
    [SerializeField] GameObject transition;
    [SerializeField] float transitionTime = 2f;

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
    }

    public void QuitGame()
    {
        AudioManager.instance.Play("Click Sound");
        Application.Quit();
    }

    public void CloseQuitMenu()
    {
        AudioManager.instance.Play("Click Sound");
        quitMenu.gameObject.SetActive(false);
        splashButtons.gameObject.SetActive(true);
    }
}
