using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMenuAnimation : MonoBehaviour
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;

    private bool isOpen = false;

    private void Awake()
    {
        settingsButton.GetComponent<Animator>().keepAnimatorStateOnDisable = true;
        creditsButton.GetComponent<Animator>().keepAnimatorStateOnDisable = true;
    }

    public void OpenCloseDropDownMenu()
    {
        if (!isOpen)
        {
            AudioManager.instance.Play(Constants.CLICK_SOUND);
            
            settingsButton.gameObject.SetActive(true);
            creditsButton.gameObject.SetActive(true);
            
            settingsButton.GetComponent<Animator>().SetBool("open", true);
            creditsButton.GetComponent<Animator>().SetBool("open", true);
            
            isOpen = true;
        }

        else
        {
            AudioManager.instance.Play(Constants.CLICK_SOUND);
            
            settingsButton.GetComponent<Animator>().SetBool("open", false);
            creditsButton.GetComponent<Animator>().SetBool("open", false);

            StartCoroutine(CloseDropDownMenuDelay());
            
            isOpen = false;
        }
    }

    IEnumerator CloseDropDownMenuDelay()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        
        settingsButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
    }
}
