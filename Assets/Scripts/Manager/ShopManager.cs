using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public void Init()
    {
        NpcManager.Instance.RegisterNpcEvent(NpcFunction.InvokeShop, OnOpenShop);
    }

    public bool BuyItem(int shopId, int shopItemID)
    {
        int rc = 0;
        if (DataManager.Instance.ShopItems.ContainsKey(shopId))
        {
            rc = 1;
            ShopItemDefine shopItem = null;
            if (DataManager.Instance.ShopItems[shopId].TryGetValue(shopItemID, out shopItem))
            {
                rc = 2;
                if (Player.Instance.Gold >= shopItem.Price)
                {
                    rc = 3;
                    if(ItemManager.Instance.AddItem(shopItem.ItemID, shopItem.Count))
                    {
                        Player.Instance.Gold -= shopItem.Price;
                        return true;
                    }
                }
            }
        }
        Debug.LogFormat("BuyItem failed, rc = {0}", rc);
        return false;
    }

    bool OnOpenShop(NpcDefine npc)
    {
        Debug.LogFormat("OnOpenShop npc.Param{0}", npc.Param);
        ShowShop(npc.Param);
        return true;
    } 

    void ShowShop(int shopid)
    {
        Debug.LogFormat("ShowShop: shopId{0}", shopid);
        ShopDefine shopDefine;
        if(DataManager.Instance.Shops.TryGetValue(shopid, out shopDefine))
        {
            UIShop ui = MMOUIManager.Instance.Show<UIShop>();
            if(ui != null)
            {
                ui.SetShop(shopDefine);
            }
        }
    }
}
