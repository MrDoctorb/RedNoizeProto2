using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RedNoize.References;

public class MetronomeVisual : MonoBehaviour
{
    public void Pulse()
    {
        transform.localScale = new Vector2(2, 2);
    }

    private void Update()
    {
        float secondsPerBeat = 60 / nm.bpm; 
        Vector2 newScale = new Vector2();
        newScale.x = Mathf.Lerp(transform.localScale.x, Vector2.one.x, (secondsPerBeat / 2) * Time.deltaTime);
        newScale.y = Mathf.Lerp(transform.localScale.y, Vector2.one.y, (secondsPerBeat / 2) * Time.deltaTime);
        transform.localScale = newScale;

    }
}
