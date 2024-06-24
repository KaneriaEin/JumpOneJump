using GameServer.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : Singleton<GameObjectManager>
{
    Dictionary<int, GameObject> Characters = new Dictionary<int, GameObject>();
    private void CreateCharacterObject(Character character)
    {
        if (!Characters.ContainsKey(character.entityId) || Characters[character.entityId] == null)
        {
        }
        this.InitGameObject(Characters[character.entityId], character);
    }

    private void InitGameObject(GameObject go, Creature character)
    {
        EntityController ec = go.GetComponent<EntityController>();
        if (ec != null)
        {
        }
    }


}
