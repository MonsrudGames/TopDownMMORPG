using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public void FullScreen(bool state)
    {
        Screen.fullScreen = state;
        Debug.Log("Fullscreen = " + state);
    }
}
