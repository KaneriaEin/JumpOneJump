using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    /* ������̺������� */
    /*
     * ƽʱ��TPS������
     * �����̵������Ի�ʱ����Ҫ�����������Ҫ���֣���ͷ��Ҫ�̶����Һ��Ӽ��̲������������������Լ����˾��Ȳ�����
     * 
     * 
     */

    public CinemachineVirtualCamera current_virtual_camera = null;
    public int duringUI = 0;
    public void Init()
    {

    }

    //��ͨ��Ϸ���棬wasd�ƶ��������ƾ�ͷ�͵��������������
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

    //�Ի����̵ꡢѡ����棬��ʾ��꣬���ﲻ�ƶ�����긺����ui����ͷ�̶�
    public void InClickEvent()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Player.Instance.StopMove(true);
    }
}
