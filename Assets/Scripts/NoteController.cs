using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RedNoize.References;

public class NoteController : MonoBehaviour
{
    [System.NonSerialized] public int lane;
    public Color color;
    SpriteRenderer rend;

    //Color is set when catchable is set
    public bool catchable
    {
        get
        {
            return _catchable;
        }
        set
        {
            if (value)
            {
                color = Color.white;
            }
            else
            {
                color = Color.yellow;
            }
            _catchable = value;
        }
    }
    bool _catchable;

    //As the object spawns
    void OnEnable()
    {
        rend = GetComponent<SpriteRenderer>();
        catchable = false;
    }


    /// <summary>
    /// Destroys the Notes once they pass a certain point
    /// </summary>
    public void Update()
    {
        if (transform.position.y <= -3)
        {
            nm.lanes[lane].Dequeue();
            player.TakeDamage();

            Destroy(gameObject);
        }

        //Notes lower in opacity based on bpm
        //Note only lowers to 50% opacity
        rend.color = new Color(color.r, color.r, color.b, rend.color.a - (Time.deltaTime * (float)nm.bpm / 120));
    }

    /// <summary>
    /// Sets the color back to full opacity
    /// </summary>
    public void Flash()
    {
        rend.color = color;
    }
}
