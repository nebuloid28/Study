using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform bullet;
    bool isDelay = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("FireCoroutine");
    }

    IEnumerator FireCoroutine()
    {
        if (!isDelay)
        {
            isDelay = true;
            Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1.0f);
            isDelay = false;
        }
    }
}
