using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform tr;
    Rigidbody rbody;
    float bulletSpeed = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        tr = transform;

        rbody.AddForce(tr.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(tr.gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
