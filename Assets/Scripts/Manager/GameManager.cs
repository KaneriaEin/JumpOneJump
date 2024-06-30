using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Score { get { return mScore; } set { mScore = value; UIManager.Instance.UpdateScoreNum(mScore); spawnScore += value; } }
    private int mScore;
    public bool isOver = false;
    public bool gaming = false;
    public int spawnScore = 0;

    public Action OnGameStart;

    public Action OnGameStartInit;


    private void Awake()
    {
        Instance = this;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    void Start()
    {
        InitManager.Instance.Init();
        EnemyManager.Instance.Init();
        PlayerPrefs.SetInt("UnitySelectMonitor", 0);
        Score = 0;
        isOver = false;
    }

    public void Over()
    {
        gaming = false;
        isOver = true;
        Time.timeScale = 0;
        MusicManager.Instance.StopMusic();
        UIManager.Instance.StopAllCoroutines();
        UIManager.Instance.GameResult(Score);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameStart()
    {
        MusicManager.Instance.PlaySound("Music/clickButton");
        if(OnGameStartInit != null)
            OnGameStartInit();

        EnemyManager.Instance.Init();
        Score = 0;
        PlatformManager.Instance.OnGameStart();
        Player.Instance.OnGameStart();
        UIManager.Instance.OnGameStart();
        UIManager.Instance.UpdateScoreNum(Score);
        isOver = false;
        gaming = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        if(OnGameStart != null)
            OnGameStart();
        Time.timeScale = 1;
    }

    public void OnGamePause()
    {
        //Player.Instance.GetComponent<WeaponController>().enabled = false;
        UIManager.Instance.OnGamePause();
    }

    public void OnGameResume()
    {
        //Player.Instance.GetComponent<WeaponController>().enabled = enabled;
        UIManager.Instance.OnGameResume();
    }
}
