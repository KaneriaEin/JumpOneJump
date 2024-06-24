using GameServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyAI
{
    class AIBase
    {
        private Enemy owner;
        GameObject target;
        public int SkillRange;
        public int Speed = 7;
        public AIBase(Enemy owner)
        {
            this.owner = owner;
            SkillRange = 20;
        }

        internal void Update()
        {
            this.UpdateBattle();
        }

        private void UpdateBattle()
        {
            if (this.target == null)
            {
                return;
            }
            if (!TryCastSkill())
            {
                //FollowTarget();
            }
        }

        private bool TryCastSkill()
        {
            if (this.target != null)
            {
                foreach(var skill in this.owner.SkillMgr.Skills)
                {
                    if(skill != null && skill.CanCast() == SkillResult.Ok)
                    {
                        this.owner.CastSkill(skill.Define.ID);
                    }
                }
            }
            return false;
        }

        private void FollowTarget()
        {

            int distance = (int)(this.owner.transform.position - this.target.transform.position).magnitude;
            //Debug.LogFormat("### FollowTarget distance{0}", distance);
            if (distance > SkillRange)
            {
                Vector3 dir = target.transform.position - this.owner.transform.position;
                Vector3 movedir = dir.normalized;
                this.owner.transform.position += movedir * Speed * Time.deltaTime;
                this.owner.GetComponent<EntityController>().OnEntityEvent(EntityEvent.MoveFwd, 0);
            }
            else
                this.owner.GetComponent<EntityController>().OnEntityEvent(EntityEvent.Idle, 0);
        }


        internal void OnDamage(GameObject source)
        {
            this.target = source;
        }
    }
}
