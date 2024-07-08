using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(isPaused)
            {
                Continuegame();
            }
            else
            {
                Pausegame();
            }
        }
    }

    public void Pausegame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Continuegame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GotoLevelMennu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level Menu");
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
