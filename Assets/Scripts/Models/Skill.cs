using GameServer.Core;
using GameServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SkillStatus
{
    None = 0,
    Casting = 1,
    Running = 2,
}
public enum SkillResult
{
    Ok = 0,
    OutOfMp = 1,
    CoolDown = 2,
    InvalidTarget = 3,
    OutOfRange = 4,
    Casting = 5,
}


public class Skill
{
    public GameObject Owner;
    public SkillDefine Define;

    public SkillStatus Status { get; private set; }

    private float skillTime = 0;
    private float cd = 0;
    public float CD
    {
        get { return cd; }
    }


    public bool Instant
    {
        get
        {
            return true;
        }
    }

    public Skill(GameObject owner, SkillDefine skilldef)
    {
        this.Owner = owner;
        this.Define = skilldef;
    }

    public SkillResult CanCast()
    {
        if (this.Status != SkillStatus.None)
        {
            return SkillResult.Casting;
        }

        int distance = (int)(this.Owner.transform.position - Player.Instance.transform.position).magnitude;
        if (distance > this.Define.CastRange)
        {
            return SkillResult.OutOfRange;
        }

        if (this.cd > 0)
        {
            return SkillResult.CoolDown;
        }

        return SkillResult.Ok;
    }

    internal SkillResult Cast()
    {
        SkillResult result = this.CanCast();
        if (result == SkillResult.Ok)
        {
            Owner.GetComponent<Animator>().SetTrigger("AttackFireBall");
            this.skillTime = 0;
            this.cd = this.Define.CD;
            this.Status = SkillStatus.Running;
        }
        return result;
    }

    internal void Update()
    {
        UpdateCD();
        if (this.Status == SkillStatus.Casting)
        {
            this.UpdateCasting();
        }
        else if (this.Status == SkillStatus.Running)
        {
            this.UpdateSkill();
        }
    }

    private void UpdateCasting()
    {
    }

    private void UpdateCD()
    {
        if (this.cd > 0)
        {
            this.cd -= Time.deltaTime;
        }
        if (cd < 0)
        {
            this.cd = 0;
        }
    }

    private void UpdateSkill()
    {
        this.skillTime += Time.deltaTime;
        if (this.skillTime >= this.Define.SkillTime)
            this.Status = SkillStatus.None;

    }
}