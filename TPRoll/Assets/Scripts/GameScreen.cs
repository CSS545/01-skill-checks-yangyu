using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    public PauseScreen pauseScreen;
    public DeathScreen deathScreen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }
    }

    public void PauseGame() {
        
        //trun default offscreen on
        pauseScreen.gameObject.SetActive(true);


    }

    public void OnPlayerDeath()
    {
        gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);

    }

}
