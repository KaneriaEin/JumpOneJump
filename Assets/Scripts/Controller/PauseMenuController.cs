using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance != null && GameManager.Instance.gaming == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            MMOUIManager.Instance.Show<UIBag>();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        GameManager.Instance.OnGamePause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        InputManager.Instance.InClickEvent();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        InputManager.Instance.InGamingBattleMode();
        GameManager.Instance.OnGameResume();
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        settingMenuUI.SetActive(true);
    }
}
