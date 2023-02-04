using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RaycastHit hit;

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void OnDestroy()
    {
        instance = null;
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RayCast();
    }

    public void StartGame()
    {

    }

    public void RayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ball"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                }
                if (Input.GetMouseButtonDown(1))
                {
                    Rigidbody ball = hit.collider.gameObject.GetComponent<Rigidbody>();
                    Vector3 dir = hit.transform.position - hit.point;
                    ball.AddForce(dir * 5.0f, ForceMode.Impulse);
                }
            }

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Plane"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                }
                if (Input.GetMouseButtonDown(1))
                {
                    Cube.instance.Move(hit.point);
                }
            }
        }
    }
}
