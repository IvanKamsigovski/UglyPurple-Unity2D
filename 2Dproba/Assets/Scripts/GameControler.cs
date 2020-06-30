using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{
    //public float slowDownFactor = 10f;

    //Zavrsavanje igre
    public void EndGame()
    {
        //restart igre
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // StartCoroutine(RestartLevel());
    }

    //Odgadanje zavrsetka i efekt usporavanja
  /*  IEnumerator RestartLevel()
    {
       Time.timeScale = 1f / slowDownFactor;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowDownFactor;
        
        yield return new WaitForSeconds(1f / slowDownFactor); //odgoda jednu sec

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowDownFactor;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/
}
