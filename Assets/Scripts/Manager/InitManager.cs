using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InitManager : Singleton<InitManager>
{
    public void Init()
    {
        DataManager.Instance.Load();
        InputManager.Instance.Init();
        ItemManager.Instance.Init();
        BagManager.Instance.Init();
        ShopManager.Instance.Init();
        EquipManager.Instance.Init();
    }
}
