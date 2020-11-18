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
        Invoke("CloseQuitMenuDelay", 0.4f);
    }

    private void CloseQuitMenuDelay()
    {
        quitMenu.gameObject.SetActive(false);
        splashButtons.gameObject.SetActive(true);
    }

}
