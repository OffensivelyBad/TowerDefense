using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileTypes
{
    rock, arrow, fireball
}

public class Projectile : MonoBehaviour
{

    [SerializeField] private int attackStrength = 1;
    [SerializeField] private ProjectileTypes projectileType;
    public int AttackStrength
    {
        get
        {
            return attackStrength;
        }
    }
    public ProjectileTypes ProjectileType
    {
        get
        {
            return projectileType;
        }
    }

}