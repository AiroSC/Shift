using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streetLightSpawner : MonoBehaviour
{
    public Transform spawn;
    public GameObject slPrefab;

    public float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject temp = Instantiate(slPrefab, spawn);
        Destroy(gameObject);
    }
}
