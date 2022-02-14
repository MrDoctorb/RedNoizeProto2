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

    [SerializeField] int scaleNum = 0;

    #region Player Sounds
    [SerializeField] AudioSource combatSound;
    [SerializeField] AudioClip flatB;
    [SerializeField] AudioClip lowC;
    [SerializeField] AudioClip flatD;
    [SerializeField] AudioClip flatE;
    [SerializeField] AudioClip F;
    [SerializeField] AudioClip G;
    [SerializeField] AudioClip sharpG;
    [SerializeField] AudioClip hiC;
    [SerializeField] AudioClip hiFlatD;
    [SerializeField] AudioClip hiFlatE;
    [SerializeField] AudioClip hiF;
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
        if (Input.GetKeyDown(KeyCode.UpArrow) && scaleNum != 3)
        {
            scaleNum += 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && scaleNum != 0)
        {
            scaleNum -= 1;
        }

        switch(scaleNum)
        {
            case 3:
                #region F Scale
                if (Input.GetKeyDown("q"))
                {
                    BlockLane(0);
                    combatSound.PlayOneShot(F);
                }
                if (Input.GetKeyDown("w"))
                {
                    BlockLane(1);
                    combatSound.PlayOneShot(sharpG);
                }
                if (Input.GetKeyDown("e"))
                {
                    BlockLane(2);
                    combatSound.PlayOneShot(hiFlatD);
                }
                if (Input.GetKeyDown("r"))
                {
                    BlockLane(3);
                    combatSound.PlayOneShot(hiF);
                }
                #endregion
                break;
            case 2:
                #region E Flat Scale
                if (Input.GetKeyDown("q"))
                {
                    BlockLane(0);
                    combatSound.PlayOneShot(flatE);
                }
                if (Input.GetKeyDown("w"))
                {
                    BlockLane(1);
                    combatSound.PlayOneShot(G);
                }
                if (Input.GetKeyDown("e"))
                {
                    BlockLane(2);
                    combatSound.PlayOneShot(flatB);
                }
                if (Input.GetKeyDown("r"))
                {
                    BlockLane(3);
                    combatSound.PlayOneShot(hiFlatE);
                }
                #endregion
                break;
            case 1:
                #region D Flat Scale
                if (Input.GetKeyDown("q"))
                {
                    BlockLane(0);
                    combatSound.PlayOneShot(flatD);
                }
                if (Input.GetKeyDown("w"))
                {
                    BlockLane(1);
                    combatSound.PlayOneShot(F);
                }
                if (Input.GetKeyDown("e"))
                {
                    BlockLane(2);
                    combatSound.PlayOneShot(sharpG);
                }
                if (Input.GetKeyDown("r"))
                {
                    BlockLane(3);
                    combatSound.PlayOneShot(hiFlatD);
                }
                #endregion
                break;
            default:
                #region C Scale
                if (Input.GetKeyDown("q"))
                {
                    BlockLane(0);
                    combatSound.PlayOneShot(lowC);
                }
                if (Input.GetKeyDown("w"))
                {
                    BlockLane(1);
                    combatSound.PlayOneShot(flatE);
                }
                if (Input.GetKeyDown("e"))
                {
                    BlockLane(2);
                    combatSound.PlayOneShot(G);
                }
                if (Input.GetKeyDown("r"))
                {
                    BlockLane(3);
                    combatSound.PlayOneShot(hiC);
                }
                #endregion
                break;
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
            float nextAccurateBeat = nm.timeAtLastMetronome + (60 / nm.bpm);
            if (Time.time < nextAccurateBeat + .1f && Time.time > nextAccurateBeat - .1f)
            {
                ThrowNote(lane);
            }
            else
            {
                print("Swing and a miss");
                //TO DO Penalty
            }

        }
    }

    void BlockNote(NoteController note, int lane)
    {
        //Remove note from the queue
        nm.lanes[lane].Dequeue();

        if (note.catchable)
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
