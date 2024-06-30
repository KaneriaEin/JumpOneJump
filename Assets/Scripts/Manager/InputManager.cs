using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    /* 管理键盘和鼠标操作 */
    /*
     * 平时是TPS操作；
     * 但打开商店或人物对话时，需要调整，鼠标需要出现，镜头需要固定，且忽视键盘操作；甚至将来还可以加入运镜等操作；
     * 
     * 
     */

    public CinemachineVirtualCamera current_virtual_camera = null;
    public int duringUI = 0;
    public void Init()
    {

    }

    //普通游戏界面，wasd移动，鼠标控制镜头和点击射击，隐藏鼠标
    public void InGamingBattleMode()
    {
        if (this.duringUI > 0)
        {
            return;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        if (current_virtual_camera != null)
        {
            current_virtual_camera.gameObject.SetActive(false);
            current_virtual_camera = null;
        }
        Player.Instance.StopMove(false);
    }

    //对话、商店、选项界面，显示鼠标，人物不移动，鼠标负责点击ui，镜头固定
    public void InClickEvent()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Player.Instance.StopMove(true);
    }
}
