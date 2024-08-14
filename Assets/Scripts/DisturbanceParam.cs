using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbanceParam
{
    public int health;
    public int damage;
    public float tracespeed;
    public DisturbanceParam()
    {
        this.health = 0;
        this.damage = 0;
        this.tracespeed = 0f;
    }
    public DisturbanceParam(int health,int damage,float tracespeed)
    {
        this.health = health;
        this.damage = damage;
        this.tracespeed = tracespeed;
    }
}