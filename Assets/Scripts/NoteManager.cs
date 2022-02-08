using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RedNoize.References;
//Note travels from 5.5 to - 1.5

public class NoteManager : MonoBehaviour
{
    //By default 4 lanes. Each lane corresponds to a button
    public List<Queue<NoteController>> lanes = new List<Queue<NoteController>>();
    [SerializeField] int bpm;

    //The note spawning is beat 0
    [SerializeField] int beatsToPlayer;
    [SerializeField] GameObject noteRef;

    //If set to true the game will pause after every beat
    [SerializeField] bool debugMode;

    /// <summary>
    /// Initializes our 4 lanes and starts the metronome
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            lanes.Add(new Queue<NoteController>());
        }

        InvokeRepeating("Metronome", 0, 60 / (float)bpm);

        //Sets the global note manager equal to this object
        nm = this;
    }

    /// <summary>
    /// Spawns the note in a given lane. 
    /// That note moves downwards and is added to the queue
    /// </summary>
    /// <param name="lane">The given lane</param>
    void SpawnNote(int lane)
    {
        NoteController note = Instantiate(noteRef, new Vector2(LaneNumToXPos(lane), 5.5f),
                                    Quaternion.identity).GetComponent<NoteController>();

        note.lane = lane;

        float beatsPerSecond = ((float)bpm / 60);
        note.GetComponent<Rigidbody2D>().velocity = new Vector2(0, (-7 * beatsPerSecond) / beatsToPlayer);

        lanes[lane].Enqueue(note);
    }

    /// <summary>
    /// Converts a lane number to a world space X coordinate
    /// </summary>
    /// <param name="laneNum">The given lane</param>
    /// <returns></returns>
    float LaneNumToXPos(int laneNum)
    {
        return (1.5f * laneNum) - 2.25f;
    }

    /// <summary>
    /// Called every beat
    /// </summary>
    void Metronome()
    {
        print("Click");
        if (Random.Range(0, 10) == 0)
        {
            SpawnNote(Random.Range(0, 4));
        }

        if (debugMode)
        {
            Debug.Break();
        }
    }

}