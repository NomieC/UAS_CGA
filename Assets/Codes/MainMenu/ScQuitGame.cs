using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScQuitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
