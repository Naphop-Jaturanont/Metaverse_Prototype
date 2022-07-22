using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    bool isShoot = false;
    [SerializeField] private GameObject BulletPrefeb;
    [SerializeField] private Transform BulletSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShoot = true)
        {
         shoot();
      Debug.Log("SHOOT!!");   
        }
      
    }
    
    void shoot()
    {
        Vector3 FireBulletPos = new Vector3(BulletSpawn.position.x, BulletSpawn.position.y,BulletSpawn.position.z);

        Instantiate(BulletPrefeb,FireBulletPos,BulletPrefeb.transform.rotation);
    }
}
