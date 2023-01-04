using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private float poopSpawnTime = 2f; // default spawn time. once every 2 seconds. will change when difficultLevel increases.
    private float coinSpawnTime = 3f; // default spawn time. once every 2 seconds. will change when difficultLevel increases.
    private float buffSpawnTime = 30f; // default spawn time. once every 2 seconds. will change when difficultLevel increases.
    private float lastPoopSpawned = 1f; // will hold Time number of when the last poop prefab was instantiated.
    private float lastCoinSpawned = 4f; // will hold Time number of when the last coin prefab was instantiated.
    private float lastBuffSpawned = 0f; // will hold Time number of when the last coin prefab was instantiated.
    public int difficultLevel = 0; // Will hold a difficulty level that increase over time.
    public float timeSinceSceneStarted;
    public int tempBuff;
    public float afkTimer; // used in afkChecker() .



    public GameObject poopPrefab;
    public GameObject coinPrefab;
    public GameObject bigCoinPrefab;
    public GameObject poopStickPrefab;
    public GameObject tissuePaperPrefab;
    public GameObject poopStickParticlePrefab;
    public GameObject poopDestroyParticlePrefab;
    public GameObject[] buffs = new GameObject[3]; // Change the array size compared to number of buffs implemented.

    public void Start()
    {
        resetTimers(); // make sure difficult level and all other variables are reset.
        Spawn(poopPrefab, Random.Range(-8, 0), Random.Range(8, 9));
        Spawn(poopPrefab, Random.Range(0, 8), Random.Range(8, 9));
        Spawn(poopPrefab, Random.Range(-2, 4), Random.Range(6, 7));
        Spawn(poopPrefab, Random.Range(-3, 3), Random.Range(6, 7));
        Spawn(poopPrefab, Random.Range(4, 8), Random.Range(6, 8));
        Spawn(poopPrefab, Random.Range(-8, 8), Random.Range(6, 9));

        //Vector2 randompos = new Vector2(Random.Range(-5, 5), 5);                        // location for debuging purposes:
        //Instantiate(poopStickPrefab, randompos, Quaternion.identity);                   //debug:
    } // end of Start
    private void Update()
    {
        timeSinceSceneStarted = Time.timeSinceLevelLoad;
        changeStates(); // Method checks how long the game is running & change the stateCounter accordingly.
        poopSpawnTimer(); // Method checks how much time passes since last spawn, and if should spawn then it spawns according to the difficultLevel
        coinSpawnTimer(); // Method checks how much time passes since last spawn, and if should spawn then it spawns according to the difficultLevel
        buffSpawnTimer(); // Method checks how much time passes since last spawn, and if should spawn then it spawns according to the difficultLevel
        afkChecker();
    } // end of Update.


    public void Spawn(GameObject prefabName, float xRange, float yRange)
    {
        if (prefabName == poopPrefab)
        {
            Vector2 randomPos = new Vector2 (xRange , yRange);
            Instantiate(poopPrefab, randomPos, Quaternion.identity);
            Debug.LogError("Instantiated a Poop. State: " + difficultLevel + " , position: " + randomPos + " , spawnTime: " + poopSpawnTime + " , GameTime:" + timeSinceSceneStarted);
        }
        if (prefabName == tissuePaperPrefab)
        {
            Vector2 randomPos = new Vector2 (xRange , yRange);
            Instantiate(tissuePaperPrefab, randomPos, Quaternion.identity);
            Debug.LogError("Instantiated a tissuePaperPrefab. State: " + difficultLevel + " , position: " + randomPos + " , spawnTime: " + poopSpawnTime + " , GameTime:" + timeSinceSceneStarted);
        }
    }
    public void Spawn(GameObject prefabName)
    {
        // spawn poop
        if(prefabName == poopPrefab)
        {
            Vector2 randomPos = new Vector2(Random.Range(-8,8), Random.Range(6,8));
            Instantiate(poopPrefab, randomPos, Quaternion.identity);
            Debug.LogError("Instantiated a Poop. State: " + difficultLevel + " , position: " + randomPos + " , spawnTime: " + poopSpawnTime + " , GameTime:" + timeSinceSceneStarted);
        }
        // spawn small coin
        else if (prefabName == coinPrefab)
        {
            Vector2 randomPos = new Vector2(Random.Range(-8, 8), 8);
            Instantiate(coinPrefab, randomPos, Quaternion.identity);
            Debug.LogError("Instantiated a Coin. State: " + difficultLevel + " , position: " + randomPos + " , spawnTime: " + coinSpawnTime + " , GameTime:" + timeSinceSceneStarted);
        }
        // spawn big coin
        else if (prefabName == bigCoinPrefab)
        {
            Vector2 randomPos = new Vector2(Random.Range(-8, 8), 9);
            Instantiate(bigCoinPrefab, randomPos, Quaternion.identity);
            Debug.LogError("Instantiated a BIG coin. State: " + difficultLevel + " , position: " + randomPos + " , spawnTime: " + buffSpawnTime + " , GameTime:" + timeSinceSceneStarted);
        }
        else if (prefabName == poopStickPrefab)
        {
            Vector2 randomPos = new Vector2(Random.Range(-8, 8), 9);
            Instantiate(poopStickPrefab, randomPos, Quaternion.identity);
            Debug.LogError("Instantiated a poopStick. State: " + difficultLevel + " , position: " + randomPos + " , spawnTime: " + buffSpawnTime + " , GameTime:" + timeSinceSceneStarted);
        }
        else if (prefabName == tissuePaperPrefab)
        {
            Vector2 randomPos = new Vector2(Random.Range(-8, 8), 9);
            Instantiate(tissuePaperPrefab, randomPos, Quaternion.identity);
            Debug.LogError("Instantiated a poopStick. State: " + difficultLevel + " , position: " + randomPos + " , spawnTime: " + buffSpawnTime + " , GameTime:" + timeSinceSceneStarted);
        }
        else /* spawn a poop in a random location. */
        {
            Vector2 randomPos = new Vector2(Random.Range(-8, 8), Random.Range(6,8));
            Instantiate(poopPrefab, randomPos, Quaternion.identity);
            Debug.LogError("Instantiated a Poop. State: " + difficultLevel + " , position: " + randomPos + " , spawnTime: " + poopSpawnTime + " , GameTime:" + timeSinceSceneStarted);
        }
    }

    public void afkChecker()
    {
        // In a second(60Frames) var 'afkTimer' will reach 6f. after 10sec a poop will spawn with the player X location to prevent AFK'ing.
        afkTimer += 0.1f;
        if (afkTimer > 60f && Character.IsPlayerAlive == true)
        {
            Vector3 playerAfkPos = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, 10f);
            Instantiate(poopPrefab, playerAfkPos, Quaternion.identity);
            Debug.Log("Spawned poop from AfkChecker, on player position");
            afkTimer = 0f;
        }

    }

    public void DestroyAllGameObjects()                                       //Debug:
    {
        GameObject[] poopsToDestroy = GameObject.FindGameObjectsWithTag("Poop");
            for (int i = 0; i < poopsToDestroy.Length; i++)
            {
                Debug.Log("pooped found to destroy number: " + i + " --> " + poopsToDestroy[i]);
                Vector3 poopPos4Par = new Vector3(poopsToDestroy[i].transform.position.x, poopsToDestroy[i].transform.position.y, poopsToDestroy[i].transform.position.z);
                GameObject buff2Destroy = Instantiate(poopDestroyParticlePrefab, poopPos4Par, poopsToDestroy[i].transform.rotation);
                Destroy(buff2Destroy, 3f);
                Destroy(poopsToDestroy[i]);
            }
        Handheld.Vibrate();
    }
   

    public void resetTimers()
    {
        poopSpawnTime = 2f; 
        coinSpawnTime = 3f; 
        buffSpawnTime = 20f;
        lastPoopSpawned = 1f;
        lastCoinSpawned = 4f;
        lastBuffSpawned = 0f;
        difficultLevel = 0;
        timeSinceSceneStarted = 0f;
    }
    public void buffSpawnTimer()
    {
        tempBuff = Random.Range(0, buffs.Length); // exclude last number.
        

        if (timeSinceSceneStarted > lastBuffSpawned + buffSpawnTime)
        {
            if (difficultLevel == 0 || difficultLevel == 1) // EASY & MEDIUM States
            {
                Spawn(buffs[Random.Range(0, buffs.Length)]);
                lastBuffSpawned = timeSinceSceneStarted;
            }
            else if (difficultLevel == 2 || difficultLevel == 3) // HARD & EXPERT States
            {
                Spawn(buffs[Random.Range(0, buffs.Length)]);
                lastBuffSpawned = timeSinceSceneStarted;
                buffSpawnTime = 15f;
            }
            else // POOP MAN State
            {
                Spawn(buffs[Random.Range(0, buffs.Length)]);
                lastBuffSpawned = timeSinceSceneStarted;
                buffSpawnTime = 10f;
            }
        }
    }
    public void coinSpawnTimer()
    {
        if (timeSinceSceneStarted > lastCoinSpawned + coinSpawnTime)
        {
            if (difficultLevel == 0 || difficultLevel == 1) // EASY & MEDIUM States
            {
                Spawn(coinPrefab);
                lastCoinSpawned = timeSinceSceneStarted;
            }
            else if (difficultLevel == 2 || difficultLevel == 3) // HARD & EXPERT States
            {
                Spawn(coinPrefab);
                lastCoinSpawned = timeSinceSceneStarted;
            }
            else // POOP MAN State
            {
                Spawn(coinPrefab);
                lastCoinSpawned = timeSinceSceneStarted;
            }
        }
    }
    public void changeStates()
    {
        if (timeSinceSceneStarted > 120f) // POOP MAN
        {
            poopSpawnTime = 0.65f;
            difficultLevel = 4;
            coinSpawnTime = 3f;
        }
        else if (timeSinceSceneStarted > 75f) // EXPERT
        {
            poopSpawnTime = 0.8f;
            difficultLevel = 3;
            coinSpawnTime = 3.5f;
        }
        else if (timeSinceSceneStarted > 45f) // HARD
        {
            poopSpawnTime = 1f;
            difficultLevel = 2;
            coinSpawnTime = 3.5f;
        }
        else if (timeSinceSceneStarted > 25f) // MEDIUM
        {
            poopSpawnTime = 1.2f;
            difficultLevel = 1;
        }
        else if (timeSinceSceneStarted > 10f) // EASY
        {
            poopSpawnTime = 1.5f;
        }
    }
    public void poopSpawnTimer()
    {
        if (timeSinceSceneStarted > lastPoopSpawned + poopSpawnTime)
        {
            if (difficultLevel == 0 || difficultLevel == 1) // EASY & MEDIUM States
            {
                Spawn(poopPrefab);
                Spawn(poopPrefab);
                lastPoopSpawned = timeSinceSceneStarted;
            }
            else if (difficultLevel == 2 || difficultLevel == 3) // HARD & EXPERT States
            {
                Spawn(poopPrefab, Random.Range(-8, 0), Random.Range(6, 9));
                Spawn(poopPrefab, Random.Range(0, 8), Random.Range(6, 9));
                Spawn(poopPrefab, Random.Range(0, 8), Random.Range(6, 9));
                lastPoopSpawned = timeSinceSceneStarted;
            }
            else // POOP MAN State
            {
                Spawn(poopPrefab, Random.Range(-8, 0), Random.Range(6, 9));
                Spawn(poopPrefab, Random.Range(-8, 0), Random.Range(6, 9));
                Spawn(poopPrefab, Random.Range(0, 8), Random.Range(6, 9));
                Spawn(poopPrefab, Random.Range(-8, 8), Random.Range(6, 9));
                lastPoopSpawned = timeSinceSceneStarted;
            }
        }
    }


    

} // End of Controller Class.



                // if you want to check a new method with a key. put this in Update
//if (Input.GetKeyDown("space")) 
//        {
//            DestroyAllGameObjects();
////gameObject.CompareTag("Poop"));
//Debug.Log("Space Pressed");
//        }