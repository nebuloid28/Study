using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public int type;
    public bool isVisible;
    public GameObject block;

    public Block(int _type, bool _isVisible, GameObject _block)
    {
        type = _type;
        isVisible = _isVisible;
        block = _block;
    }
}

public class GameManager : MonoBehaviour
{
    static public int xWidth = 150;
    static public int zWidth = 150;
    static public int height = 150;
    public float ground = 20;
    public float waveLength = 0;
    public float amplitude = 0;


    public GameObject block_Ice;
    public GameObject block_Grass;
    public GameObject block_Dirt;
    public GameObject block_Rock;
    public GameObject block_Gold;
    public GameObject block_Dia;
    public GameObject block_End;

    public RaycastHit hit;

    public Block[,,] mapBlock = new Block[xWidth, height, zWidth];

    private List<GameObject> BlockList = new List<GameObject>();

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
        StartCoroutine(InitGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if(Physics.Raycast(ray, out hit, 5f))
            {
                Vector3 pos = hit.transform.position;

                if (pos.y <= 0) return;

                mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = null;
                Destroy(hit.collider.gameObject);

                Vector3 spBlock;
                spBlock = new Vector3(pos.x + 1, pos.y, pos.z);
                spawnBlock(spBlock);
                spBlock = new Vector3(pos.x - 1, pos.y, pos.z);
                spawnBlock(spBlock);
                spBlock = new Vector3(pos.x, pos.y + 1, pos.z);
                spawnBlock(spBlock);
                spBlock = new Vector3(pos.x, pos.y - 1, pos.z);
                spawnBlock(spBlock);
                spBlock = new Vector3(pos.x, pos.y, pos.z + 1);
                spawnBlock(spBlock);
                spBlock = new Vector3(pos.x, pos.y, pos.z - 1);
                spawnBlock(spBlock);
            }
        }
    }

    IEnumerator InitGame()
    {
        yield return StartCoroutine(InitMap());
    }

    IEnumerator InitMap()
    {
        for (int x = 0; x < xWidth; x++)
        {
            for (int z = 0; z < zWidth; z++)
            {
                float xCoord = x / waveLength;
                float zCoord = z / waveLength;
                int y = (int)(Mathf.PerlinNoise(xCoord, zCoord) * amplitude + ground);

                Vector3 pos = new Vector3(x, y, z);
                StartCoroutine(CreateBlock(y, pos, true));
                while (y > 0)
                {
                    y--;
                    pos = new Vector3(x, y, z);
                    StartCoroutine(CreateBlock(y, pos, false));
                }
            }
        }

        yield return null;
    }

    IEnumerator CreateBlock(int y, Vector3 pos, bool vis)
    {
        if (y == 0)
        {
            if (vis)
            {
                GameObject blockObj = (GameObject)Instantiate(block_Dia, pos, Quaternion.identity);
                mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(7, vis, blockObj);
            }
            else mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(7, vis, null);
        }
        else if (y > 30)
        {
            if (vis)
            {
                GameObject blockObj = (GameObject)Instantiate(block_Ice, pos, Quaternion.identity);
                mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(1, vis, blockObj);
            }
            else mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(3, vis, null);
        }
        else if(y > 20)
        {
            if (vis)
            {
                GameObject blockObj = (GameObject)Instantiate(block_Grass, pos, Quaternion.identity);
                mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(2, vis, blockObj);
            }
            else mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(3, vis, null);
        }
        else if (y > 10)
        {
            if (vis)
            {
                GameObject blockObj = (GameObject)Instantiate(block_Dirt, pos, Quaternion.identity);
                mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(3, vis, blockObj);
            }
            else mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(4, vis, null);
        }
        else
        {
            if (vis)
            {
                GameObject blockObj = (GameObject)Instantiate(block_Rock, pos, Quaternion.identity);
                mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(4, vis, blockObj);
            }
            else mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(4, vis, null);
        }

        if (y > 0 && y < 10 && Random.Range(0, 100) < 5)
        {
            if(mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].block != null)
            {
                Destroy(mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].block);
            }

            if (vis)
            {
                GameObject blockObj = (GameObject)Instantiate(block_Gold, pos, Quaternion.identity);
                mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(5, vis, blockObj);
            }
            else mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(5, vis, null);
        }

        if (y > 0 && y < 5 && Random.Range(0, 100) < 2)
        {
            if (mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].block != null)
            {
                Destroy(mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].block);
            }

            if (vis)
            {
                GameObject blockObj = (GameObject)Instantiate(block_Gold, pos, Quaternion.identity);
                mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(6, vis, blockObj);
            }
            else mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] = new Block(6, vis, null);
        }

        yield return null;
    }

    private void spawnBlock(Vector3 pos)
    {
        if(mapBlock[(int)pos.x, (int)pos.y, (int)pos.z] != null)
        {
            if(!mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].isVisible)
            {
                GameObject spawnedBlock = null;
                mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].isVisible = true;

                if(mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].type == 1)
                    spawnedBlock = (GameObject)Instantiate(block_Ice, pos, Quaternion.identity);
                else if(mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].type == 2)
                    spawnedBlock = (GameObject)Instantiate(block_Grass, pos, Quaternion.identity);
                else if(mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].type == 3)
                    spawnedBlock = (GameObject)Instantiate(block_Dirt, pos, Quaternion.identity);
                else if(mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].type == 4)
                    spawnedBlock = (GameObject)Instantiate(block_Rock, pos, Quaternion.identity);
                else if(mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].type == 5)
                    spawnedBlock = (GameObject)Instantiate(block_Gold, pos, Quaternion.identity);
                else if (mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].type == 6)
                    spawnedBlock = (GameObject)Instantiate(block_Dia, pos, Quaternion.identity);
                else if (mapBlock[(int)pos.x, (int)pos.y, (int)pos.z].type == 7)
                    spawnedBlock = (GameObject)Instantiate(block_End, pos, Quaternion.identity);
            }
        }
    }

    /*
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
    */
}
