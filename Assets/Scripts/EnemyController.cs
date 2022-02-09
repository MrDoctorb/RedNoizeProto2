using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public List<AttackPattern> attacks = new List<AttackPattern>();
    public AttackPattern attack;    
}

[System.Serializable]
public class AttackPattern
{
    public int length, wait;
    public List<List<bool>> notes = new List<List<bool>>();
}

