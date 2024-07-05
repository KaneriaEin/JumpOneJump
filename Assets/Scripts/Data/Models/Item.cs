using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ItemId;
    public int Count;
    public ItemDefine Define;
    public EquipDefine EquipInfo;

    public Item(int id, int count)
    {
        this.ItemId = id;
        this.Count = count;
        DataManager.Instance.Items.TryGetValue(id, out this.Define);
        DataManager.Instance.Equips.TryGetValue(id, out this.EquipInfo);
    }

    public override string ToString()
    {
        return string.Format("[Item Id:{0} Name:{1} Count{2}]", this.ItemId, this.Define.Name, this.Count);
    }
}
