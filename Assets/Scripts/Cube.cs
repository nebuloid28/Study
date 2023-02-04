using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cube : MonoBehaviour
{
    NavMeshAgent cube;
    public static Cube instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cube = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, cube.destination) < 0.1f)
        {
            cube.isStopped = true;
        }
    }

    public void Move(Vector3 dest)
    {
        cube.isStopped = false;
        cube.destination = dest;
    }
}
