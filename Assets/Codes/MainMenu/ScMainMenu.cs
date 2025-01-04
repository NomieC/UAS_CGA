using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScMainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);  // Memuat scene dengan index 1
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        // Berhenti bermain saat di editor
        EditorApplication.isPlaying = false;
        #else
        // Keluar dari game saat di build player
        Application.Quit();
        #endif
    }
}