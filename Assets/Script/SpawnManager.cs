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

    void Start()
    {
        InvokeRepeating("SpawnRandomObstacle",2,1.5f);
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
