using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public enum NpcType
{
    None = 0,
    Functional = 1,
    Task,
}
public enum NpcFunction
{
    None = 0,
    InvokeShop = 1,
    InvokeStory = 2,
}

public class NpcDefine
{
    public int ID {  get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Vector3 Position { get; set; }
    public NpcType Type { get; set; }
    public NpcFunction Function { get; set; }
    public int Param {  get; set; }
}
