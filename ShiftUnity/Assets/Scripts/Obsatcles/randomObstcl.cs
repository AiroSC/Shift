using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomObstcl : MonoBehaviour
{
    //object list
    public GameObject o1;
    public GameObject o2;
    public GameObject o3;

    public bool manualSpawn = false;
    public int spawnInt;

    public List<GameObject> oList;

    // Start is called before the first frame update
    void Start()
    {
        //set oList
        oList.Add(o1);
        oList.Add(o2);
        oList.Add(o3);

        //Instantiate(oList[Random.Range(0, 3)], this.transform);
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        if (manualSpawn)
        {
            Instantiate(oList[spawnInt], this.transform);
        }
        else
        {
            Instantiate(oList[Random.Range(0, 3)], this.transform);
        }
    }
}
