using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameServer.Entities
{
    /// <summary>
    /// Character
    /// 玩家角色类
    /// </summary>
    public class Character : Creature
    {
        public Character(CharacterType type, int configId, int level, Vector3Int pos, Vector3Int dir) : base(type, configId, level, pos, dir)
        {
        }

        /// <summary>
        /// 角色离开时调用
        /// </summary>
        public void Clear()
        {
        }

    }
}
