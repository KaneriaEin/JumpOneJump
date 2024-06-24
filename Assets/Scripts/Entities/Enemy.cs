using MyAI;
using UnityEngine;

namespace GameServer.Entities
{
    public class Enemy : MonoBehaviour
    {
        public GameObject Owner;
        public AIAgent AI;
        public SkillManager SkillMgr;
        public GameObject FireballPrefab;
        public Transform FireballSpawnOrigin;

        public AudioSource musicFire;

        public void CastSkill(int id)
        {
            Skill skill = this.SkillMgr.GetSkill(id);
            skill.Cast();
        }

        private void Start()
        {
            SkillMgr = new SkillManager(this);
            AI = new AIAgent(this);
        }


        void Update()
        {
            if(Owner.GetComponent<EntityController>() != null)
            {
                if(Owner.GetComponent<EntityController>().enemyState == EnemyState.Die)
                    return;
            }
            this.AI.Update();
            this.SkillMgr.Update();
        }

        void CastFireBall()
        {
            musicFire.PlayOneShot(musicFire.clip);
            this.transform.LookAt(Player.Instance.gameObject.transform);

            GameObject bullet = Instantiate(FireballPrefab, FireballSpawnOrigin.position, FireballSpawnOrigin.rotation);
            bullet.transform.LookAt(Player.Instance.gameObject.transform);
            Destroy(bullet, 10);
        }

    }
}
