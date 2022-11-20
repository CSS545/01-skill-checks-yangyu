using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 +timed spawner on Apple and Poo
   +Spawn center follows the player
   +Spawn in circular
   +Spawn radius = camera width (adjustable by diff device)
*/
public class Spawner : MonoBehaviour
{
    public GameScreen gameScreen;
    public GameObject spawnPrefabPoo;
    public GameObject spawnPrefabApple;
    public GameObject coffeePrefab;
    public GameObject icecreamPrefab;

    public GameObject player;
    public Timer timer;

    private float currrentPooSpawnTime = 0;
    private float currrentAppSpawnTime = 0;

    private float CircleRadius;
    private int PooNum;

    public float pooSpawnTime = 5;
    public float AppleSpawnTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        CircleRadius = Camera.main.orthographicSize *
                       Camera.main.aspect *
                       2;
        PooNum = 1;

        gameScreen.CurPooCount = PooNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            transform.position = player.transform.position;
        }
        if (timer.GetTimerActive())
        {
            currrentPooSpawnTime += Time.deltaTime;
            currrentAppSpawnTime += Time.deltaTime;
        }
        if (currrentPooSpawnTime > pooSpawnTime)
        {
            SpawnbyType(spawnPrefabPoo);

            
            currrentPooSpawnTime = 0;
        }
        if (currrentAppSpawnTime > AppleSpawnTime)
        {
            SpawnbyType(spawnPrefabApple);

            SpawnbyType(coffeePrefab);
            SpawnbyType(icecreamPrefab);

            currrentAppSpawnTime = 0;
        }
        

    }

    public void SpawnbyType(GameObject prefab) {
        //if poo update data
        if (prefab == spawnPrefabPoo) {
            PooNum++;
            gameScreen.CurPooCount = PooNum;
        }
        //if coffe or icecream, spawn farthur
        if (prefab == coffeePrefab)
        {
            Instantiate(prefab,
                        RandomCircle(transform.position, CircleRadius + 10),
                        Quaternion.identity);
        } else {
            Instantiate(prefab,
                        RandomCircle(transform.position, CircleRadius),
                        Quaternion.identity);
        }

    }



    //https://answers.unity.com/questions/714835/best-way-to-spawn-prefabs-in-a-circle.html

    Vector3 RandomCircle (Vector3 center, float radius) {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
