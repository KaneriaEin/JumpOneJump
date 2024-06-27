using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        foreach(var item in Items)
        {
            Debug.Log(item.ToString());
        }
        Debug.Log("ItemManager.ShowAll Finish!");
    }
}
