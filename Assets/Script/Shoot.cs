using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject[] BulletPrefebs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootBullet()
    {
        int BulletIndex = Random.Range(0, BulletPrefebs.Length);
        Instantiate(BulletPrefebs[BulletIndex]);
    }
}
