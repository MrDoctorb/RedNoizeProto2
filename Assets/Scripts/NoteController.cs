using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RedNoize.References;

public class NoteController : MonoBehaviour
{
    public int lane;

    /// <summary>
    /// Destroys the Notes once they pass a certain point
    /// </summary>
    public void Update()
    {
        if(transform.position.y <= -3)
        {
            nm.lanes[lane].Dequeue();
            player.TakeDamage();

            Destroy(gameObject);
        }
    }
}
