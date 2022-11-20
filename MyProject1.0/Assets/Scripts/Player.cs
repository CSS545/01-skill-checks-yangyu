using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int foodLimiter = 0;
    public MovementJoystick movementJoystick;
    public float playerSpeed;
    public float logBase = 2;
    public Spawner spn;
    public GameObject ememyPref;
    private Rigidbody2D rb;
    public int countTmp = 0;
    public int coffeeCountTmp = 0;
    public int creamCountTmp = 0;
    public AudioSource deathSFSource;
    public AudioClip deathClip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement update
        if (movementJoystick.joystickVec.y != 0)
        {
            rb.velocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed, movementJoystick.joystickVec.y * playerSpeed);
        }
        else {
            rb.velocity = Vector2.zero;
        }

        //collision update

    }

    //Unity magic activated when collision happen
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerSpeed = 0;
            //Destroy(gameObject);
            //this go through all scene to find scene- bad, but ok for once
            GameScreen gameScreen = FindObjectOfType<GameScreen>();
            if (gameScreen != null)
            {
                deathSFSource.PlayOneShot(deathClip, 0.8f);
                //update food collected this round
                PlayerPrefs.SetInt("AppleEat", 
                                    countTmp + PlayerPrefs.GetInt("AppleEat", 0));
                gameScreen.OnPlayerDeath();
            }
            
        }
        //7,2 15,speed up 0.28
        //7,10 15,speed up 0.06
        if (collision.gameObject.CompareTag("Item"))
        {
            //Eat food
            Destroy(collision.gameObject);
            //Digest
            spn.SpawnbyType(ememyPref);
            //Record Food Count
            countTmp++; 

            foodLimiter++;
            playerSpeed = playerSpeed + Mathf.Log(playerSpeed, logBase) / foodLimiter;
            Debug.Log(Mathf.Log(playerSpeed, logBase) + "/"+ foodLimiter + " = " + playerSpeed);

        }
        if (collision.gameObject.CompareTag("Double Edge"))
        {
            //Eat food
            Destroy(collision.gameObject);
            //Digest
            spn.SpawnbyType(ememyPref);
            spn.SpawnbyType(ememyPref);
            //Record Food Count
            coffeeCountTmp++;

            foodLimiter++;
            playerSpeed = playerSpeed + 2 * Mathf.Log(playerSpeed, logBase) / foodLimiter;
            Debug.Log(Mathf.Log(playerSpeed, logBase) + "/" + foodLimiter + " = " + playerSpeed);

        }
        if (collision.gameObject.CompareTag("Poo Maker"))
        {
            //Eat food
            Destroy(collision.gameObject);
            //Digest
            spn.SpawnbyType(ememyPref);
            spn.SpawnbyType(ememyPref);
            spn.SpawnbyType(ememyPref);
            //Record Food Count
            creamCountTmp++;

            if (foodLimiter == 0) { foodLimiter = 1; }
            playerSpeed = playerSpeed + Mathf.Log(playerSpeed, logBase) / foodLimiter;
            Debug.Log(Mathf.Log(playerSpeed, logBase) + "/" + foodLimiter + " = " + playerSpeed);

        }

    }
}
