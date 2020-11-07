using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsGUI : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

}
