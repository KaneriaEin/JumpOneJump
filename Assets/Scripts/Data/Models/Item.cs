using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ItemId;
    public int Count;
    public ItemDefine Define;

    public Item(int id, int count)
    {
        this.ItemId = id;
        this.Count = count;
        DataManager.Instance.Items.TryGetValue(id, out this.Define);
    }

    public override string ToString()
    {
        return string.Format("[Item Id:{0} Name:{1} Count{2}]", this.ItemId, this.Define.Name, this.Count);
    }
}
