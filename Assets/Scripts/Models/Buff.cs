using GameServer.Entities;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    internal bool Stoped;
    public string IconResource;
    public float restTime;
    public float totalTime;
    public BuffType buffType;

    public string BuffName;

    public Buff(BuffType type)
    {
        this.buffType = type;

        switch (type)
        {
            case BuffType.DoubleHigher:
                this.BuffName = "DoubleHigher";
                this.IconResource = "UI/BuffIcon/UI_Buff_Icon_HighJump";
                this.restTime = 20;
                this.totalTime = 20;
                break;
            case BuffType.ForthDamage:
                this.BuffName = "ForthDamage";
                this.IconResource = "UI/BuffIcon/UI_Buff_Icon_FourDamage";
                this.restTime = 15;
                this.totalTime = 15;
                break;
            case BuffType.BloodDrinking:
                this.BuffName = "BloodDrinking";
                this.IconResource = "UI/BuffIcon/UI_Buff_Icon_BloodDrink";
                this.restTime = 20;
                this.totalTime = 20;
                break;
            case BuffType.BigSquare:
                this.BuffName = "BigSquare";
                this.IconResource = "UI/BuffIcon/UI_Buff_Icon_BigSquare";
                this.restTime = 10;
                this.totalTime = 10;
                break;
            default:
                break;
        }
    }


}
