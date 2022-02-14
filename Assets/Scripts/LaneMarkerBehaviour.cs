using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneMarkerBehaviour : MonoBehaviour
{
    public GameObject laneOne;
    public GameObject laneTwo;
    public GameObject laneThree;
    public GameObject laneFour;

    private void Start()
    {
        laneOne.GetComponent<SpriteRenderer>();
        laneTwo.GetComponent<SpriteRenderer>();
        laneThree.GetComponent<SpriteRenderer>();
        laneFour.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        laneFour.SetActive(false);
        laneThree.SetActive(false);
        laneTwo.SetActive(false);
        laneOne.SetActive(false);

        if (Input.GetKey("q"))
        {
            laneOne.SetActive(true);
        }
        if (Input.GetKey("w"))
        {
            laneTwo.SetActive(true);
        }
        if (Input.GetKey("e"))
        {
            laneThree.SetActive(true);
        }
        if (Input.GetKey("r"))
        {
            laneFour.SetActive(true);
        }
    }

}
