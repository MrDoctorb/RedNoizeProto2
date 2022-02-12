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
        
            if(Input.GetKey("q"))
            {
                laneOne.SetActive(true);
            }
            else if (Input.GetKey("w"))
            {
                laneTwo.SetActive(true);
            }
            else if (Input.GetKey("e"))
            {
                    laneThree.SetActive(true);
            }
            else if (Input.GetKey("r"))
            {
                laneFour.SetActive(true);
            }
            else
            {
                laneFour.SetActive(false);
                laneThree.SetActive(false);
                laneTwo.SetActive(false);
                laneOne.SetActive(false);
            }
    }

}
