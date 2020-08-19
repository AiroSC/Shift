using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] GameObject T1;
    [SerializeField] GameObject T2;
    [SerializeField] GameObject T3;
    [SerializeField] GameObject Drop;

    GameManager gm;
    DropOffSpawn DOS;
    PickUpSpawn PUS;
    int id;
    int food;
    GameObject Prefab;
    private void Start()
    {
        DOS = GameObject.Find("DropOff").GetComponent<DropOffSpawn>();
        PUS = GameObject.Find("PickUp").GetComponent<PickUpSpawn>();
        gm = GameObject.Find("gameManager").GetComponent<GameManager>();

        GeneratePickup();
    }

    public void GeneratePickup()
    {
        food = Random.Range(0, 10);
        id = Random.Range(0, 2);
        Transform newPickUp = PUS.GetNodeTransform(id);
        switch (id)
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
        gm.NewPickup();
        GameObject temp = Instantiate(Prefab, newPickUp.position, newPickUp.rotation);
    }
    public void GenerateDelivery()
    {
        food = Random.Range(0, 2);
        id = Random.Range(0, 2);
        Transform newDelivery = PUS.GetNodeTransform(id);
        GameObject temp = Instantiate(Drop, newDelivery.position, newDelivery.rotation);
    }
}
