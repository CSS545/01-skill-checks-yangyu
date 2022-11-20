using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScreen : MonoBehaviour
{
    public TMP_Text tmpText;
    public PauseScreen pauseScreen;
    public DeathScreen deathScreen;
    public Timer timer;
    public int CurPooCount;
    private bool death;
    private bool paused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }
        tmpText.text = CurPooCount.ToString();
    }

    private void Start()
    {
        death = false;
        paused = false;
        CurPooCount = 1;
    }

    private void Awake()
    {
        OnDisable();
        if (!paused)
        {
            //trun default offscreen on
            pauseScreen.gameObject.SetActive(false);
        }
    }

    public void PauseGame() {

        paused = true;
        timer.TimerActive(false);
        //trun default offscreen on
        pauseScreen.gameObject.SetActive(true);


    }
    private void OnEnable()
    {
        timer.TimerActive(true);
        paused = false;
    }
    private void OnDisable()
    {
        if (!death)
        {
            timer.TimerActive(false);
            //trun default offscreen on
            pauseScreen.gameObject.SetActive(true);
        }
    }

    

    public void OnPlayerDeath()
    {
        death = true;
        PlayerPrefs.SetInt("TotalDeath", 1 + 
                            PlayerPrefs.GetInt("TotalDeath", 0));
        Debug.Log("OnPlayDeathTotalDeath" + 
                            PlayerPrefs.GetInt("TotalDeath", 0));
        timer.TimerActive(false);
        gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);

    }

}
