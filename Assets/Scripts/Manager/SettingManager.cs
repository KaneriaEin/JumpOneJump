using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance;

    public CinemachineFreeLook playerCam;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerCameraSpeed(Direction dir, float value)
    {
        if (dir == Direction.Horizon)
        {
            this.playerCam.m_XAxis.m_MaxSpeed = value * 2 + 100;
        }
        else if(dir == Direction.Vertical)
        {
            this.playerCam.m_YAxis.m_MaxSpeed = value / 25 + 1;
        }
    }
}
