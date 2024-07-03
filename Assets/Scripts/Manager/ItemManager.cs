using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.TextCore.Text;

public class ItemManager : Singleton<ItemManager>
{
    public Dictionary<int, Item> Items = new Dictionary<int, Item>();

    public void Init()
    {
        Dictionary<int, ItemList> dic;
        string dataPath = "Assets/Data/";
        string json = File.ReadAllText(dataPath + "ItemList.txt");
        dic = JsonConvert.DeserializeObject<Dictionary<int, ItemList>>(json);
        foreach(var a in dic)
        {
            Item item = new Item(a.Value.ItemID, a.Value.ItemCount);
            Items.Add(item.ItemId, item);
        }
        Debug.Log("ItemManager Init finish!");
    }

    public void ShowAll()
    {
        string str = null;
        foreach(var item in Items)
        {
            str += item.ToString() + "\n";
        }
        Debug.Log(str + "ItemManager.ShowAll Finish!");
    }

    public bool AddItem(int itemID, int count)
    {
        ItemDefine itemDefine = DataManager.Instance.Items[itemID];
        if (this.Items.ContainsKey(itemID))
        {
            //此处stackLimit的意义是背包单格子的限制数量，而此物品的全局最大持有数，比如剧情道具之类，暂时不做此限制，无脑+=count；
            //if (this.Items[itemID].Count + count > itemDefine.StackLimit)
            //{
            //    Debug.LogFormat("AddItem failed, over stackLimit！");
            //    return false;
            //}
            //else
            //{
            //    this.Items[itemID].Count += count;
            //}
            this.Items[itemID].Count += count;
        }
        else
        {
            Item item = new Item(itemID, count);
            this.Items[itemID] = item;
        }
        BagManager.Instance.AddItem(itemID, count);
        return true;
    }
}
