using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameServer.Entities
{
    public class Entity
    {
        public int entityId;

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set {
                position = value;
            }
        }

        private Vector3Int direction;
        public Vector3Int Direction
        {
            get { return direction; }
            set
            {
                direction = value;
            }
        }

        private int speed;
        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
            }
        }

        public Entity(Vector3 pos,Vector3 dir)
        {
        }



        public virtual void Update()
        {

        }
    }
}
