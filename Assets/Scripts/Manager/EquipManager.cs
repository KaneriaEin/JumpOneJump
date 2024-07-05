using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using System.IO;
using TMPro.EditorUtilities;

public enum EquipSlot
{
    Weapon = 0,
    Accessory,
    Helmet,
    Chest,
    Shoulder,
    Pants,
    Boots,
    SlotMax
}

public class EquipManager : Singleton<EquipManager>
{
    public delegate void OnEquipChangeHandler();
    public event OnEquipChangeHandler OnEquipChanged;

    public Item[] Equips = new Item[(int)EquipSlot.SlotMax];

    public void Init()
    {
        //读角色身上的装备信息
        Dictionary<int, EquipList> dic;
        string dataPath = "Assets/Data/";
        string json = File.ReadAllText(dataPath + "EquipList.txt");
        dic = JsonConvert.DeserializeObject<Dictionary<int, EquipList>>(json);
        foreach (var a in dic)
        {
            if (a.Value.ID == 0)
            {
                Equips[a.Key - 1] = null;
            }
            else
            {
                Equips[a.Key - 1] = ItemManager.Instance.Items[a.Value.ID];
            }
        }
        Debug.Log("EquipManager Init finish!");
    }

    public bool Contains(int itemId)
    {
        for(int i = 0; i < Equips.Length && Equips[i] != null; i++)
        {
            if(Equips[i].ItemId == itemId)
            {
                return true;
            }
        }
        return false;
    }

    public void EquipItem(Item equip)
    {
        OnEquipItem(equip);
    }

    public void UnEquipItem(Item equip)
    {
        OnUnEquipItem(equip);
    }

    public void OnEquipItem(Item equip)
    {
        if (this.Equips[(int)equip.EquipInfo.Slot] != null && this.Equips[(int)equip.EquipInfo.Slot].ItemId == equip.ItemId)
        {
            return;
        }
        this.Equips[(int)equip.EquipInfo.Slot] = ItemManager.Instance.Items[equip.ItemId];
        if (OnEquipChanged != null)
            OnEquipChanged();
    }

    public void OnUnEquipItem(Item equip)
    {
        if (this.Equips[(int)equip.EquipInfo.Slot] == null || this.Equips[(int)equip.EquipInfo.Slot].ItemId != equip.ItemId)
        {
            return;
        }
        this.Equips[(int)equip.EquipInfo.Slot] = null;
        if (OnEquipChanged != null)
            OnEquipChanged();
    }


}
