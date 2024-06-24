using GameServer.Entities;
using UnityEngine;

namespace MyAI
{
    public class AIAgent
    {
        public Enemy Owner;
        public EntityController Ctl;
        private AIBase ai;

        public AIAgent(Enemy owner)
        {
            this.Owner = owner;
            this.Ctl = owner.gameObject.GetComponent<EntityController>();
            ai = new AIBase(owner);
            ai.OnDamage(Player.Instance.gameObject);
        }

        internal void Update()
        {
            if(Ctl  != null)
            {
                if(Ctl.enemyState == EnemyState.Die)
                {
                    return;
                }
            }
            if(this.ai != null)
            {
                this.ai.Update();
            }
        }

        internal void OnDamage(GameObject source)
        {
            if(this.ai != null)
            {
                ai.OnDamage(source);
            }
        }
    }
}
