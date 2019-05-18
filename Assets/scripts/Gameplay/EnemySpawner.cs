using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    GameObject enemyPrefab;

    GameObject forrest;

    //SpawnPoints
    [SerializeField]
    GameObject leftUpperSpawnPoint;
    [SerializeField]
    GameObject rightUpperSpawnPoint;
    [SerializeField]
    GameObject leftBottomSpawnPoint;
    [SerializeField]
    GameObject rightBottomSpawnPoint;
    [SerializeField]
    GameObject leftMiddleSpawnPoint;
    [SerializeField]
    GameObject rightMiddleSpawnPoint;

    // Enemy Swap timer
    Timer enemySwapTimer;
    float spawnTime = 8f;
    int currentNumberOfSpawnPoints = 1;

    //spawn system support
    bool spawnCircleRun = false;
    bool leftUpperPointWasDone = false;
    bool leftMiddlePointWasDone = false;
    bool leftBottomPointWasDone = false;
    bool rightUpperPointWasDone = false;
    bool rightMiddlePointWasDone = false;
    bool rightBottomPointWasDone = false;


    // Start is called before the first frame update
    void Start()
    {
        enemySwapTimer = gameObject.AddComponent<Timer>();
        enemySwapTimer.Duration = spawnTime;
        enemySwapTimer.Run();

        //Event support
        EventManager.AddListener(EventName.DifficultyChangeEvent, MakeGameMoreDifficalty);
    }

    // Update is called once per frame
    void Update()
    {
        //Creating enemies Algoritm 
        if (enemySwapTimer.Finished && !spawnCircleRun) // check if timer was finished and spawn circle 
        {
            spawnCircleRun = true;
            int spawnPointDone = 0; 
            while (spawnPointDone < currentNumberOfSpawnPoints) // spawn an amount enemies in random points but not spawn in differents points
            {
                Vector2 spawnPoint;
                int spawnPointNumber = Random.Range(0, 6);

                if (spawnPointNumber == 0 && !leftUpperPointWasDone)
                {
                    leftUpperPointWasDone = true; // if enemy was spawn in this point next one you can't spawn in this point
                    spawnPoint = leftUpperSpawnPoint.transform.position;
                    GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                    enemy.GetComponent<Enemy>().Flip(); // if enemy was spawn in left coner we need to flip him
                    spawnPointDone++;
                }
                else if (spawnPointNumber == 1 && !leftMiddlePointWasDone)
                {
                    leftMiddlePointWasDone = true; // if enemy was spawn in this point next one you can't spawn in this point
                    spawnPoint = leftMiddleSpawnPoint.transform.position;
                    GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                    spawnPointDone++;
                    enemy.GetComponent<Enemy>().Flip(); // if enemy was spawn in left coner we need to flip him

                }
                else if (spawnPointNumber == 2 && !leftBottomPointWasDone)
                {
                    leftBottomPointWasDone = true; // if enemy was spawn in this point next one you can't spawn in this point
                    spawnPoint = leftBottomSpawnPoint.transform.position;
                    GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                    spawnPointDone++;
                    enemy.GetComponent<Enemy>().Flip(); // if enemy was spawn in left coner we need to flip him
                }
                else if (spawnPointNumber == 3 && !rightUpperPointWasDone)
                {
                    rightUpperPointWasDone = true; // if enemy was spawn in this point next one you can't spawn in this point
                    spawnPoint = rightUpperSpawnPoint.transform.position;
                    Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                    spawnPointDone++;
                }
                else if (spawnPointNumber == 4 && !rightMiddlePointWasDone)
                {
                    rightMiddlePointWasDone = true; // if enemy was spawn in this point next one you can't spawn in this point
                    spawnPoint = rightMiddleSpawnPoint.transform.position;
                    Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                    spawnPointDone++;
                }
                else if (spawnPointNumber == 5 && !rightBottomPointWasDone)
                {
                    rightBottomPointWasDone = true; // if enemy was spawn in this point next one you can't spawn in this point
                    spawnPoint = rightBottomSpawnPoint.transform.position;
                    Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                    spawnPointDone++;
                }
            }

            //restart a parameters
            spawnCircleRun = false;
            leftUpperPointWasDone = false;
            leftMiddlePointWasDone = false;
            leftBottomPointWasDone = false;
            rightUpperPointWasDone = false;
            rightMiddlePointWasDone = false;
            rightBottomPointWasDone = false;
            enemySwapTimer.Duration = spawnTime;
            enemySwapTimer.Run(); // run timer again
        }
     
    }


    /// <summary>
    /// make games more difficalty depends how much points character get
    /// </summary>
    /// <param name="points"> current score</param>
    void MakeGameMoreDifficalty(int points)
    {
        if (points >= 5000)
        {
            spawnTime = 3;
        }
        else if (points >= 2000)
        {
            currentNumberOfSpawnPoints = 4;
        }
        else if (points >= 1000)
        {
            currentNumberOfSpawnPoints = 3;
        }
        else if (points >= 500)
        {
            currentNumberOfSpawnPoints = 2;
            spawnTime = 5;
        }
        else if (points >= 300)
        {
            spawnTime = 6;
        }
        else if (points >= 100)
        {
            currentNumberOfSpawnPoints = 2;
            spawnTime = 7;
        }
    }
}
