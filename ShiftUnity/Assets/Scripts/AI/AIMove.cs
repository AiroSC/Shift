using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{
    public Vector3 destination;
    NavMeshAgent nav;
    public Transform[] navPoint = new Transform[5];
    public int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        destination = Random.insideUnitCircle * 75;
        //destination = navPoint[i].position;
        //i++;
        nav.destination = new Vector3(destination.x, 0, destination.z);
    }

    // Update is called once per frame
    void Update()
    { 
        if(nav.remainingDistance <= 0.75)
        {
            destination = Random.insideUnitCircle * 350;
            //destination = navPoint[i].position;
            //i++;
            //if(i == 6)
            //{
            //    i = 0;
            //}
            nav.destination = new Vector3(destination.x, 0, destination.z);
        }

    }
}
