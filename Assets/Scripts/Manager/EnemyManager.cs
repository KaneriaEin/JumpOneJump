using GameServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    interface IEntityNotify
    {
    }
    class EnemyManager : Singleton<EnemyManager>
    {
        public List<GameObject> enemies = new List<GameObject>();

        public void Init()
        {
            foreach (var enemy in enemies)
            {
                if (enemy.gameObject != null)
                {
                    UnityEngine.Object.Destroy(enemy.gameObject);
                }
            }
        }
    }
}
