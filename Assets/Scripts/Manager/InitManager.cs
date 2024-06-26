using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : Singleton<InitManager>
{
    public void Init()
    {
        DataManager.Instance.Load();
    }
}
