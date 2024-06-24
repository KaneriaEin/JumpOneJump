using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum CharacterType
{
    Player = 0,
    Npc = 1,
    Monster = 2,
}
public enum CharacterState
{
    Idle = 0,
    Move = 1,
}

namespace GameServer.Entities
{
    public class Creature : Entity
    {

        public int Id { get; set; }
        public string Name;

        public CharacterState State;


        public Creature(CharacterType type, int configId, int level, Vector3 pos, Vector3 dir) :
           base(pos, dir)
        {
            this.InitSkills();
            this.InitBuffs();

        }



        internal float Distance(Creature target)
        {
            return Vector3.Distance(this.Position, target.Position);
        }

        internal float Distance(Vector3 position)
        {
            return Vector3.Distance(this.Position, position);
        }

        void InitSkills()
        {
        }

        private void InitBuffs()
        {
        }


        public override void Update()
        {
        }

        protected virtual void OnDamage()
        {

        }
    }
}
