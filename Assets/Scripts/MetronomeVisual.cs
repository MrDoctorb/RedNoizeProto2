using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RedNoize.References;

public class MetronomeVisual : MonoBehaviour
{
    [SerializeField] float scalingSpeed;
    public void Pulse()
    {
        transform.localScale = new Vector2(5.9f, 1f);
    }

    private void Update()
    {
        float secondsPerBeat = 60 / (float)nm.bpm;
        Vector2 newScale = new Vector2();
        newScale.x = 5.9f;
        //newScale.x = Mathf.Lerp(transform.localScale.x, Vector2.one.x, (secondsPerBeat * scalingSpeed) * Time.deltaTime);
        newScale.y = Mathf.Lerp(transform.localScale.y, 0, (secondsPerBeat * scalingSpeed) * Time.deltaTime);
        transform.localScale = newScale;

    }
}
