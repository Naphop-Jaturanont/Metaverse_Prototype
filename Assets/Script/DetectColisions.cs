using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectColisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AMMO")
        {
         ScoreManager.instance.AddPoint();
        Destroy(gameObject);
        Destroy(this.gameObject);   
        }

        if (other.tag == "Finish")
        {
         GameManager.instance.EndGame();   
        }
        
    }
    
}
