using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject StartMenu, SettingsMenu;

    public void Play()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenSettingsMenu()
    {
        StartMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        StartMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }
}
