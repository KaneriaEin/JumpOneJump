using GameServer.Entities;
using System.Collections.Generic;

public class SkillManager
{
    private Enemy Owner;
    public List<Skill> Skills { get; private set; }

    public Skill NormalSkill { get; private set; }

    public SkillManager(Enemy owner)
    {
        this.Owner = owner;
        this.Skills = new List<Skill>();
        this.InitSkills();
    }

    void InitSkills()
    {
        this.Skills.Clear();

        SkillDefine skilldef = new SkillDefine();
        skilldef.ID = 1;
        skilldef.Name = "Ð¡±øµÄ»ðÇò";
        skilldef.CD = 4;
        skilldef.CastRange = 20;
        skilldef.SkillAnim = "Attack04";
        skilldef.SkillTime = 0.6f;

        Skill skill = new Skill(Owner.gameObject, skilldef);
        this.AddSkill(skill);
    }

    public void AddSkill(Skill skill)
    {
        this.Skills.Add(skill);
    }

    internal Skill GetSkill(int skillId)
    {
        for (int i = 0; i < this.Skills.Count; i++)
        {
            if (this.Skills[i].Define.ID == skillId)
                return this.Skills[i];
        }
        return null;
    }

    internal void Update()
    {
        for (int i = 0; i < this.Skills.Count; i++)
        {
            this.Skills[i].Update();
        }
    }
}
