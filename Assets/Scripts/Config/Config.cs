using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Horizon,
    Vertical,
    Forward,
    Back,
    Left,
    Right,
}

class Config
{
    public static float HorSpeed
    {
        get
        {
            return PlayerPrefs.GetFloat("HorizontalCameraSpeed", 66.7f);
        }
        set
        {
            PlayerPrefs.SetFloat("HorizontalCameraSpeed", value);
            SettingManager.Instance.SetPlayerCameraSpeed(Direction.Horizon, value);
        }
    }

    public static float VerSpeed
    {
        get
        {
            return PlayerPrefs.GetFloat("VerticalCameraSpeed", 20);
        }
        set
        {
            PlayerPrefs.SetFloat("VerticalCameraSpeed", value);
            SettingManager.Instance.SetPlayerCameraSpeed(Direction.Vertical, value);
        }
    }

    ~Config()
    {
        PlayerPrefs.Save();
    }

}
