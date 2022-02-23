using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public List<AttackPattern> attacks = new List<AttackPattern>();
    // public AttackPattern attack;

    [SerializeField] int maxHealth = 20;
    private int currentHealth;
    [SerializeField] Slider healthBar;
    [SerializeField] AudioClip enemyHurt;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.value = maxHealth;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player Note")
        {
            PlayerController pc = FindObjectOfType<PlayerController>();
            pc.combatSound.PlayOneShot(enemyHurt);

            currentHealth--;
            healthBar.value = currentHealth;

            Object.Destroy(collision.gameObject);
        }
    }
}

[System.Serializable]
public class AttackPattern
{
    public InstrumentType instrument = InstrumentType.Lead;
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

public enum InstrumentType
{
    Lead,
    Bass,
    Drum
}

