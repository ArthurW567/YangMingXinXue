using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disturbance
{
    public int health;
    public int damage;
    public float tracespeed;

   public Disturbance(int health,int damage,float tracespeed)
    {
        this.health = health;
        this.damage = damage;
        this.tracespeed = tracespeed;
    }
}