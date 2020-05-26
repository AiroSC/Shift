using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{
    public Vector2 destination;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        destination = Random.insideUnitCircle * 25;
        nav.destination = new Vector3(destination.x, 0, destination.y);
    }

    // Update is called once per frame
    void Update()
    { 
        if(nav.remainingDistance <= 0.75)
        {
            destination = Random.insideUnitCircle * 250;
            nav.destination = new Vector3(destination.x, 0, destination.y);
        }
    }
}
