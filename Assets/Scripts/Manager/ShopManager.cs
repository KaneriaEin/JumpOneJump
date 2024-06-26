using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    void Init()
    {
        NpcManager.Instance.RegisterNpcEvent(NpcFunction.InvokeShop, sdf);
    }

    bool sdf(NpcDefine npc)
    {
        return true;
    } 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
