using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class DataManager : Singleton<DataManager>
{
    public string DataPath;
    public Dictionary<int, NpcDefine> Npcs = null;

    public DataManager()
    {
        this.DataPath = "Assets/Data/";
        Debug.LogFormat("Here's DataManager()");
    }
    
    public void Load()
    {
        string json = File.ReadAllText(this.DataPath + "NpcDefine.txt");
        this.Npcs = JsonConvert.DeserializeObject<Dictionary<int, NpcDefine>>(json);
    }

    public IEnumerator LoadData()
    {
        string json = File.ReadAllText(this.DataPath + "NpcDefine.txt");
        this.Npcs = JsonConvert.DeserializeObject<Dictionary<int, NpcDefine>>(json);

        yield return null;
    }
}
