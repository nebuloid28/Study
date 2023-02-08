using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform bullet;
    bool isDelay = false;
    float delayTime = 1.0f;
    float elapseTime;

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();

        if (isDelay)
        {
            elapseTime += Time.deltaTime;
            if(elapseTime >= delayTime)
            {
                elapseTime = 0.0f;
                isDelay = false;
            }
        }
    }

    public void Fire()
    {
        if (!isDelay)
        {
            isDelay = true;
            elapseTime = 0.0f;
            Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
