using TMPro;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    private void Update()
    {
        //debug code
         if (Input.GetKey(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    /* IEnumerator CountDown()
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
     }*/
}



