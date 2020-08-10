using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType { normal, fire, water, grass }
public enum UnitEnemyType { normal,fire,water,grass}
public class Unit : MonoBehaviour
{
   


    [Header("Base Stats")]
    public int damage;
    public int unitLevel;
    public int baseHp;
    public UnitType UnitEnumType;
    public UnitEnemyType unitEnemyType;
    [Header("Calculated stats")]
    public string unitName;
    public int maxHp;
    
    public int currentHp;
    public int currentXp;
    public int maxExperience;
    public int procentCatch;

    public List<Moves> moves;
  

    public void Awake()
    {
        damage = (2 * unitLevel );
        maxHp = baseHp * 2 * unitLevel/5;
        currentHp = maxHp;
        maxExperience = unitLevel *10;
    }

    public bool TakeDamage(int dmg)
    {

        currentHp -= dmg;


        if (currentHp <= 0)
            return true;
        else
            return false;
    }
    public void LevelSystem(int gainxp)
    {

        currentXp += gainxp;

        if (currentXp >= maxExperience)
        {
            unitLevel += 1;
            currentXp -= maxExperience;
            maxExperience = unitLevel * 10;
        }
            
    }
    public void Healing()
    {
        currentHp = maxHp;
        
    }
}
