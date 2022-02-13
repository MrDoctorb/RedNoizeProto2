using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RedNoize.References;
public class PlayerController : MonoBehaviour
{
    //Bad hit range should be larger than good hit Range
    //The hit range is the half length
    [SerializeField] float badHitRange;
    [SerializeField] float goodHitRange;
    [SerializeField] float perfectHitLine;

    #region Player Sounds
    [SerializeField] AudioSource CombatSound;

    [SerializeField] AudioClip lowC;
    [SerializeField] AudioClip flatE;
    [SerializeField] AudioClip G;
    [SerializeField] AudioClip hiC;
    #endregion

    //Set the global Player Reference to this player
    private void Start()
    {
        player = this;
    }

    /// <summary>
    /// Each button press will block a different Lane
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            BlockLane(0);
            CombatSound.PlayOneShot(lowC);
        }
        else if (Input.GetKeyDown("w"))
        {
            BlockLane(1);
            CombatSound.PlayOneShot(flatE);
        }
        else if (Input.GetKeyDown("e"))
        {
            BlockLane(2);
            CombatSound.PlayOneShot(G);
        }
        else if (Input.GetKeyDown("r"))
        {
            BlockLane(3);
            CombatSound.PlayOneShot(hiC);
        }
    }

    /// <summary>
    /// Checks to see if a note is close enough to be blocked in a given lane
    /// </summary>
    /// <param name="lane"></param>
    void BlockLane(int lane)
    {
        if (nm.lanes[lane].Count == 0)
        {
            print("Swing and a miss");
            ThrowNote(lane);
            return;
        }
        NoteController note = nm.lanes[lane].Peek();

        //If the note is within the good note range
        if (note.transform.position.y < perfectHitLine + goodHitRange &&
            note.transform.position.y > perfectHitLine - goodHitRange)
        {
            print("Nice Hit!");
            
            BlockNote(note, lane);
        }
        else if (note.transform.position.y < perfectHitLine + badHitRange &&
            note.transform.position.y > perfectHitLine - badHitRange)
        {
            print("Eh, I guess");
            
            BlockNote(note, lane);

            //Penalty 
            //TODO
        }
        else
        {
            print("Swing and a miss");
            ThrowNote(lane);
        }
    }

    void BlockNote(NoteController note, int lane)
    {
        //Remove note from the queue
        nm.lanes[lane].Dequeue();

        if(note.catchable)
        {
            Destroy(note.gameObject);
            //Increase catch values
            //TODO
            
        }
        else
        {
            note.GetComponent<Rigidbody2D>().velocity *= -1;
            note.color = new Color(0, .5f, 1, .5f);
            Destroy(note.gameObject, ((float)nm.bpm / 60) * nm.beatsToPlayer);
        }
    }

    void ThrowNote(int lane)
    {
        NoteController note = Instantiate(nm.noteRef, new Vector2(nm.LaneNumToXPos(lane), -1.5f),
                                    Quaternion.identity).GetComponent<NoteController>();

        note.lane = lane;

        float beatsPerSecond = ((float)nm.bpm / 60);
        note.GetComponent<Rigidbody2D>().velocity = new Vector2(0, (7 * beatsPerSecond) / nm.beatsToPlayer);
        note.color = new Color(0, 1, 1, .5f);
        Destroy(note.gameObject, ((float)nm.bpm / 60) * nm.beatsToPlayer);

    }

    public void TakeDamage()
    {
        StartCoroutine(DamageAnimation());
    }

    IEnumerator DamageAnimation()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(.75f);
        sprite.color = Color.white;
    }
}
