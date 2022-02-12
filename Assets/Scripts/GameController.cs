using TMPro;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
  
    private float timeDelay = 3;
    
   
    void Start()
    {
        StartCoroutine(CountDown());
    }

     IEnumerator CountDown()
     {
        Time.timeScale = 0f;
         while(timeDelay > 0)
         {
            timer.text = timeDelay.ToString("0");
            yield return new WaitForSeconds(1f);
            timeDelay--;
         }

        Time.timeScale =1f;
        timer.text = "Start";

         yield return new WaitForSeconds(0.5f);
         timer.gameObject.SetActive(false);
     }
}



