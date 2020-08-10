using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType { normal, fire, water, grass }
[CreateAssetMenu(fileName = "New Moves", menuName = "Moves")]
public class Moves : ScriptableObject
{
    public int power;
    public string movesName;
    public MoveType moveType;
    public int pp;
    

}
