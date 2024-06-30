using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcManager : Singleton<NpcManager>
{
    public delegate bool NpcActionHandler(NpcDefine npc);

    private Dictionary<NpcFunction, NpcActionHandler> eventMap = new Dictionary<NpcFunction, NpcActionHandler>();
    private Dictionary<int,Vector3> npcPositions = new Dictionary<int,Vector3>();

    public void RegisterNpcEvent(NpcFunction npcFunction, NpcActionHandler handler)
    {
        if (!eventMap.ContainsKey(npcFunction))
        {
            eventMap[npcFunction] = handler;
        }
        else
        {
            eventMap[npcFunction] +=handler;
        }
    }

    public NpcDefine GetNpcDefine(int id)
    {
        return DataManager.Instance.Npcs[id];
    }

    public void UpdateNpcPosition(int npcId, Vector3 position)
    {
        this.npcPositions[npcId] = position;
    }

    public bool Interactive(int npcId)
    {
        if (DataManager.Instance.Npcs.ContainsKey(npcId))
        {
            var npc = DataManager.Instance.Npcs[npcId];
            return Interactive(npc);
        }
        return false;
    }

    public bool Interactive(NpcDefine npc)
    {
        if(npc.Type == NpcType.Task)
        {
            ItemManager.Instance.ShowAll();
            //return DoTaskInteractive(npc);
        }
        else if(npc.Type == NpcType.Functional)
        {
            //ItemManager.Instance.ShowAll();
            Debug.LogFormat("”Înpc{0}∂‘ª∞", npc.Name);
            return DoFunctionInteractive(npc);
        }
        return true;
    }

    public bool DoFunctionInteractive(NpcDefine npc)
    {
        if(npc.Type != NpcType.Functional)
        {
            return false;
        }
        if (eventMap.ContainsKey(npc.Function))
        {
            return eventMap[npc.Function](npc);
        }
        return false;
    }
}
