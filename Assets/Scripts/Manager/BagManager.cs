using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Progress;

public class BagManager : Singleton<BagManager>
{
    public int Unlocked; //解锁背包格子数
    public BagItem[] bagItems;

    public void Init()
    {
        this.Unlocked = 60;
        this.bagItems = new BagItem[Unlocked];
        int bgitemid = 0;
        //for (int i = 0; i < ItemManager.Instance.Items.Count; i++)
        foreach(var kv in ItemManager.Instance.Items)
        {
            if (kv.Value.Count == 0)
                continue;
            Item item = kv.Value;
            //Item item = ItemManager.Instance.Items[i];
            int count = item.Count;
            BagItem bgitem;
            while (count > 0)
            {
                if (count > item.Define.StackLimit)
                {
                    bgitem = new BagItem(item.ItemId, item.Define.StackLimit);
                }
                else
                {
                    bgitem = new BagItem(item.ItemId, count);
                }
                count -= item.Define.StackLimit;
                Debug.LogFormat("bgitemid{0} itemid{1} itemcount{2}", bgitemid, bgitem.ItemId, bgitem.Count);
                bagItems[bgitemid] = bgitem;
                bgitemid++;
            }
        }
    }

    public void AddItem(int itemId, int count)
    {
        ushort addcount = (ushort)count;
        ushort canAdd = 0;
        for(int i = 0; i< bagItems.Length; i++)
        {
            if(bagItems[i].ItemId == itemId)
            {
                canAdd = (ushort)(DataManager.Instance.Items[itemId].StackLimit - bagItems[i].Count);
                if(canAdd >= addcount)
                {
                    bagItems[i].Count += addcount;
                    addcount = 0;
                    break;
                }
                else
                {
                    bagItems[i].Count += canAdd;
                    addcount -= canAdd;
                }
            }
        }
        if(addcount > 0)
        {
            for(int i = 0; i< bagItems.Length; i++)
            {
                if (bagItems[i].ItemId == 0)
                {
                    this.bagItems[i].ItemId = (ushort)itemId;
                    this.bagItems[i].Count = addcount;
                    break;
                }
            }
        }
    }

    //按ItemId顺序重新整理所有BagItem；
    public void Reset()
    {
        int i = 0;
        foreach(var kv in ItemManager.Instance.Items)
        {
            if(kv.Value.Count > 0)
            {
                if(kv.Value.Count <= kv.Value.Define.StackLimit)
                {
                    bagItems[i].Count = (ushort)kv.Value.Count;
                    bagItems[i].ItemId = (ushort)kv.Key;
                    i++;
                }
                else
                {
                    int allcount = kv.Value.Count;
                    while(allcount > 0)
                    {
                        if(allcount <= kv.Value.Define.StackLimit)
                        {
                            bagItems[i].ItemId = (ushort)kv.Key;
                            bagItems[i].Count = (ushort)allcount;
                            allcount = 0;
                        }
                        else
                        {
                            bagItems[i].ItemId = (ushort)kv.Key;
                            bagItems[i].Count = (ushort)kv.Value.Define.StackLimit;
                            allcount -= (ushort)kv.Value.Define.StackLimit;
                        }
                        i++;
                    }
                }
            }
        }
        for(; i < this.Unlocked; i++)
        {
            bagItems[i] = BagItem.zero;
        }
    }

}
