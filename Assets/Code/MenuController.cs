using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnPlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Introduction");
    }
}
