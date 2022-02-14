using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RedNoize.References;
//Note travels from 5.5 to - 1.5

public class NoteManager : MonoBehaviour
{
    //By default 4 lanes. Each lane corresponds to a button
    public List<Queue<NoteController>> lanes = new List<Queue<NoteController>>();
    public int bpm;
    public NoteType type;

    //The note spawning is beat 0
    public int beatsToPlayer;
    public EnemyController enemy;
    int waiting;
    int segmentsTillOnBeat = 0;

    Queue<List<bool>> currentAttack;

    public GameObject noteRef;

    public float timeAtLastMetronome;

    //If set to true the game will pause after every beat
    [SerializeField] bool debugMode;

    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource metronomeTrack;
    [SerializeField] AudioSource combatSound;
    [SerializeField] List<AudioClip> sounds;

    /// <summary>
    /// Initializes our 4 lanes and starts the metronome
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            lanes.Add(new Queue<NoteController>());
        }

        //Sets the global note manager equal to this object
        nm = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentAttack == null)
        {
            print("A");
            LoadNewAttack();

            InvokeRepeating("Metronome", 0, (60 / (float)bpm) * (int)type);
            Invoke("StartSong", 0);
        }
    }

    void StartSong()
    {
        backgroundMusic.Play();
        metronomeTrack.Play();
    }

    /// <summary>
    /// Spawns the note in a given lane. 
    /// That note moves downwards and is added to the queue
    /// </summary>
    /// <param name="lane">The given lane</param>
    void SpawnNote(int lane)
    {
        combatSound.PlayOneShot(sounds[lane]);

        NoteController note = Instantiate(noteRef, new Vector2(LaneNumToXPos(lane), 5.5f),
                                    Quaternion.identity).GetComponent<NoteController>();

        note.lane = lane;

        float beatsPerSecond = ((float)bpm / 60);
        note.GetComponent<Rigidbody2D>().velocity = new Vector2(0, (-7 * beatsPerSecond) / beatsToPlayer);

        if (Random.Range(0, 4) == 0)
        {
            note.catchable = true;
        }


        lanes[lane].Enqueue(note);
    }

    /// <summary>
    /// Converts a lane number to a world space X coordinate
    /// </summary>
    /// <param name="laneNum">The given lane</param>
    /// <returns></returns>
    public float LaneNumToXPos(int laneNum)
    {
        return (1.5f * laneNum) - 2.25f;
    }

    /// <summary>
    /// Called every beat
    /// </summary>
    void Metronome()
    {
        timeAtLastMetronome = Time.time;

        if (currentAttack.Count > 0)
        {
            List<bool> currentNotes = currentAttack.Dequeue();
            for (int i = 0; i < 4; ++i)
            {
                if (currentNotes[i])
                {
                    SpawnNote(i);
                }
            }
        }
        else if (waiting > 1)
        {
            --waiting;
            print("WAIT");
        }
        else
        {
            LoadNewAttack();
        }

        if (debugMode)
        {
            Debug.Break();
        }

        if(segmentsTillOnBeat == 0)
        {
            //Causes the notes to flash on the beat
            foreach (NoteController note in FindObjectsOfType<NoteController>())
            {
                note.Flash();
            }
        }
        else
        {
            segmentsTillOnBeat = (int)type - 1;
        }

    }

    void LoadNewAttack()
    {
        currentAttack = new Queue<List<bool>>();
        AttackPattern attack = enemy.attacks[Random.Range(0, enemy.attacks.Count)];

        foreach (AttackPattern.lane l in attack.lanes)
        {
            List<bool> row = new List<bool>();
            for (int i = 0; i < 4; ++i)
            {
                row.Add(l.notes[i]);
            }
            currentAttack.Enqueue(row);
        }

        waiting = currentAttack.Count * 2;
        beatsToPlayer = currentAttack.Count;
    }
}


public enum NoteType
{
    quarter = 1,
    eighth = 2,
    sixteenth = 3
}