using TMPro;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    NoteManager noteManager;
    [SerializeField] GameObject playerSelector;
    [SerializeField] GameObject enemySelector;

    void Start()
    {
        noteManager = GetComponent<NoteManager>();
       // StartCoroutine(CountDown());
    }

    private void Update()
    {
       
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



