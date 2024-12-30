using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScPauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause ()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Home ()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1f;
    }

    public void Resume ()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
