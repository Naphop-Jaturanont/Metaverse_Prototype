using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] ObstaclePrefebs;
    [SerializeField]
    private float spawnRangex = 9;
    [SerializeField]
    private float spawnRangez = 20;
    [SerializeField]
    private float spawnRangey = 1;
    [SerializeField]
    private float spawnTime = 7;
    [SerializeField]
    private float spawnDelay = 3;

    void Start()
    {
        InvokeRepeating("SpawnRandomObstacle",spawnTime,spawnDelay);
    }

    void Update()
    {
        
    }

    void SpawnRandomObstacle()
    {
        int ObstacleIndex = Random.Range(0, ObstaclePrefebs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangex, spawnRangex), spawnRangey, spawnRangez);
        Instantiate(ObstaclePrefebs[ObstacleIndex], spawnPos, ObstaclePrefebs[ObstacleIndex].transform.rotation);
        Debug.Log("Obstacle Work!!!");
    }
}
