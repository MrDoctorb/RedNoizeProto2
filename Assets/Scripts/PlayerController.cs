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
        }
        else if (Input.GetKeyDown("w"))
        {
            BlockLane(1);
        }
        else if (Input.GetKeyDown("e"))
        {
            BlockLane(2);
        }
        else if (Input.GetKeyDown("r"))
        {
            BlockLane(3);
        }
    }

    void BlockLane(int lane)
    {
        NoteController note = nm.lanes[lane].Peek();

        //If the note is within the good note range
        if (note.transform.position.y < perfectHitLine + goodHitRange &&
            note.transform.position.y > perfectHitLine - goodHitRange)
        {
            print("Nice Hit!");

            //Destroy the object and remove it from the queue
            Destroy(nm.lanes[lane].Dequeue().gameObject);

            //Catch stuff
        }
        else if (note.transform.position.y < perfectHitLine + badHitRange &&
            note.transform.position.y > perfectHitLine - badHitRange)
        {
            print("Eh, I guess");

            //Destroy the object and remove it from the queue
            Destroy(nm.lanes[lane].Dequeue().gameObject);

            //Catch stuff
        }
        else
        {
            print("Swing and a miss");
        }
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
