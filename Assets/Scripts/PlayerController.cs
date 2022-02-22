using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RedNoize.References;
using TMPro;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    //Bad hit range should be larger than good hit Range
    //The hit range is the half length
    [SerializeField] float badHitRange;
    [SerializeField] float goodHitRange;
    [SerializeField] float perfectHitLine;
    [SerializeField] SpriteRenderer noteTypeVisual;
    [SerializeField] Sprite[] noteType;
    int scaleNum = 0;

    //Health vars
    [SerializeField] int maxHealth = 20;
    private int currentHealth;
    [SerializeField] Slider healthBar;

    #region Player Sounds
    [SerializeField] AudioSource combatSound;
    [SerializeField] AudioClip kick;
    [SerializeField] AudioClip snare;
    [SerializeField] AudioClip clap;
    [SerializeField] AudioClip cymbal;
    [SerializeField] AudioClip bassC;
    [SerializeField] AudioClip bassFlatE;
    [SerializeField] AudioClip bassG;
    [SerializeField] AudioClip bassHiC;
    [SerializeField] AudioClip lowC;
    [SerializeField] AudioClip flatE;
    [SerializeField] AudioClip G;
    [SerializeField] AudioClip hiC;
    #endregion

    [SerializeField] TextMeshProUGUI feedbackText;

    [SerializeField] AttackPattern newAttack;
    int currentNote;

    int antiSpam;


    //Set the global Player Reference to this player
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.value = maxHealth;
        player = this;
        newAttack = new AttackPattern();
    }

    /// <summary>
    /// Each button press will block a different Lane
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scaleNum += 1;
            scaleNum %= 3;
            noteTypeVisual.sprite = noteType[scaleNum];
        }

        switch (scaleNum)
        {
            case 2:
                #region Rhythm
                if (Input.GetKeyDown("d"))
                {
                    BlockLane(0);
                    combatSound.PlayOneShot(kick);
                }
                if (Input.GetKeyDown("f"))
                {
                    BlockLane(1);
                    combatSound.PlayOneShot(snare);
                }
                if (Input.GetKeyDown("j"))
                {
                    BlockLane(2);
                    combatSound.PlayOneShot(clap);
                }
                if (Input.GetKeyDown("k"))
                {
                    BlockLane(3);
                    combatSound.PlayOneShot(cymbal);
                }
                #endregion
                break;
            case 1:
                #region Bass
                if (Input.GetKeyDown("d"))
                {
                    BlockLane(0);
                    combatSound.PlayOneShot(bassC);
                }
                if (Input.GetKeyDown("f"))
                {
                    BlockLane(1);
                    combatSound.PlayOneShot(bassFlatE);
                }
                if (Input.GetKeyDown("j"))
                {
                    BlockLane(2);
                    combatSound.PlayOneShot(bassG);
                }
                if (Input.GetKeyDown("k"))
                {
                    BlockLane(3);
                    combatSound.PlayOneShot(bassHiC);
                }
                #endregion
                break;
            default:
                #region Lead
                if (Input.GetKeyDown("d"))
                {
                    BlockLane(0);
                    combatSound.PlayOneShot(lowC);
                }
                if (Input.GetKeyDown("f"))
                {
                    BlockLane(1);
                    combatSound.PlayOneShot(flatE);
                }
                if (Input.GetKeyDown("j"))
                {
                    BlockLane(2);
                    combatSound.PlayOneShot(G);
                }
                if (Input.GetKeyDown("k"))
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
            TryThrow(lane);
            return;
        }
        NoteController note = nm.lanes[lane].Peek();

        //If the note is within the good note range
        if (note.transform.position.y < perfectHitLine + goodHitRange &&
            note.transform.position.y > perfectHitLine - goodHitRange)
        {
            StartCoroutine(FeedBack("Nice Hit!"));

            BlockNote(note, lane);
        }
        else if (note.transform.position.y < perfectHitLine + badHitRange &&
            note.transform.position.y > perfectHitLine - badHitRange)
        {
            StartCoroutine(FeedBack("Eh, I guess"));

            BlockNote(note, lane);

            //Penalty 
            //TODO
        }
        else
        {
            TryThrow(lane);

        }
    }

    void TryThrow(int lane)
    {
        if (nm.canThrow)
        {
            nm.canThrow = false;
            StartCoroutine(RestoreNote());
            float nextAccurateBeat = nm.timeAtLastMetronome + (60 / nm.bpm);
            if (Time.time < nextAccurateBeat + .15f && Time.time > nextAccurateBeat - .15f)
            {
                ThrowNote(lane);
            }
            else
            {
                ++antiSpam;
                if (antiSpam >= 2)
                {
                    TakeDamage();
                }
                StartCoroutine(FeedBack("Swing and a miss"));
                // print("Swing and a miss");
                //TO DO Penalty
            }
        }
        else
        {
            ++antiSpam;
        }
    }

    IEnumerator RestoreNote()
    {
        yield return new WaitForSeconds(((nm.bpm / (float)60) / (float)nm.type) - .25f);
        nm.canThrow = true;
    }

    void BlockNote(NoteController note, int lane)
    {
        antiSpam = 0;
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
            Destroy(note.gameObject);
            /*
            note.GetComponent<Rigidbody2D>().velocity *= -1;
            note.color = new Color(0, .5f, 1, .5f);
            Destroy(note.gameObject, ((float)nm.bpm / 60) * nm.beatsToPlayer);*/
        }
    }

    private GameObject pNote;

    void ThrowNote(int lane)
    {
        /*if (currentNote > 4 * (int)nm.type)
        {
            currentNote = 0;
            newAttack.lanes = new AttackPattern.lane[4];
            newAttack.length = 4 * (int)nm.type;
        }

        print(lane + "<- Lane Index | Lanes Length ->" + newAttack.lanes.Length);
        print(currentNote + " <- Current Note | Notes Length ->" + newAttack.length);
        newAttack.lanes[lane].notes[currentNote] = true;

*/
     
        switch(scaleNum)
        {
            case 2:
                pNote = nm.playerDrum;
                break;

            case 1:
                pNote = nm.playerBass;
                break;

            default:
                pNote = nm.playerLead;
                break;
        }

        NoteController note = Instantiate(pNote, new Vector2(nm.LaneNumToXPos(lane), -1.5f),
                                    Quaternion.identity).GetComponent<NoteController>();

        note.tag.Replace("Untagged", "Player Note");

        note.lane = lane;

        float beatsPerSecond = ((float)nm.bpm / 60);
        note.GetComponent<Rigidbody2D>().velocity = new Vector2(0, (7 * beatsPerSecond) / nm.beatsToPlayer);
        Destroy(note.gameObject, ((float)nm.bpm / 60) * nm.beatsToPlayer);
    }

    public void IncrementAttack()
    {
        ++currentNote;
        //  ++newAttack.length;

    }

    public void TakeDamage()
    {
        currentHealth--;
        healthBar.value = currentHealth;
        StartCoroutine(DamageAnimation());
    }

    IEnumerator DamageAnimation()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(.75f);
        sprite.color = Color.white;
    }

    IEnumerator FeedBack(string feedback)
    {
        feedbackText.gameObject.SetActive(true);
        feedbackText.text = feedback;
        yield return new WaitForSeconds(.50f);
        feedbackText.gameObject.SetActive(false);
    }
}
