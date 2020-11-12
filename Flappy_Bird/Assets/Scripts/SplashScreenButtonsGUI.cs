using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenButtonsGUI : MonoBehaviour
{
    [SerializeField] Canvas splashButtons, quitMenu;
    [SerializeField] GameObject transition;
    [SerializeField] float transitionTime = 2f;

    void Awake()
    {
        quitMenu.enabled = false;
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
        splashButtons.enabled = false;
        quitMenu.enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseQuitMenu()
    {
        quitMenu.enabled = false;
        splashButtons.enabled = true;
    }
}
