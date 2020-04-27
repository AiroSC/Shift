using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] GameObject T1;
    [SerializeField] GameObject T2;
    [SerializeField] GameObject T3;
    [SerializeField] GameObject Drop;

    DropOffSpawn DOS;
    PickUpSpawn PUS;
    int id;
    int food;
    GameObject Prefab;
    private void Start()
    {
        DOS = GameObject.Find("DropOff").GetComponent<DropOffSpawn>();
        PUS = GameObject.Find("PickUp").GetComponent<PickUpSpawn>();
        GeneratePickup();
    }

    public void GeneratePickup()
    {
        food = Random.Range(0, 3);
        id = Random.Range(0, 5);
        Transform newPickUp = PUS.GetNodeTransform(id);
        switch (food)
        {
            case 0:
                Prefab = T1;
                break;
            case 1:
                Prefab = T2;
                break;
            case 2:
                Prefab = T3;
                break;
        }
        GameObject temp = Instantiate(Prefab, newPickUp.position, newPickUp.rotation );
    }
    public void GenerateDelivery()
    {
        food = Random.Range(0, 3);
        id = Random.Range(0, 5);
        Transform newDelivery = PUS.GetNodeTransform(id);
        GameObject temp = Instantiate(Drop, newDelivery.position, newDelivery.rotation);
    }
}
