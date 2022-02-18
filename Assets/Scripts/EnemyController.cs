using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<AttackPattern> attacks = new List<AttackPattern>();
    // public AttackPattern attack;

    public int health = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player Note")
        {
            health--;
        }
    }
}

[System.Serializable]
public class AttackPattern
{
    public int length;

    [System.Serializable]
    public struct lane
    {
        public bool[] notes;
    }

    public lane[] lanes = new lane[4];
/*
    public int length, wait;
    public List<List<bool>> notes = new List<List<bool>>();*/
}

