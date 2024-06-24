using GameServer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIBuffIcon : MonoBehaviour
{
    public GameObject prefabBuff;
    Dictionary<BuffType, GameObject> buffs = new Dictionary<BuffType, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOwner()
    {
        if(Player.Instance != null)
        {
            Player.Instance.OnBuffAdd = OnBuffAdd;
            Player.Instance.OnBuffRemove = OnBuffRemove;
        }
    }

    public void OnBuffAdd(Buff buff)
    {
        GameObject go = Instantiate(prefabBuff, this.transform);
        go.name = buff.BuffName;
        UIBuffItem bi = go.GetComponent<UIBuffItem>();
        bi.SetItem(buff);
        go.SetActive(true);
        this.buffs[buff.buffType] = go;
    }

    public void OnBuffRemove(Buff buff)
    {
        GameObject go;
        if (this.buffs.TryGetValue(buff.buffType, out go))
        {
            this.buffs.Remove(buff.buffType);
            Destroy(go);
        }
    }
}
