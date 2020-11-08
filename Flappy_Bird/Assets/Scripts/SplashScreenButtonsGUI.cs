using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenButtonsGUI : MonoBehaviour
{
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
}
