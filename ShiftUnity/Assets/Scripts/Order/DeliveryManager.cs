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
    OrderWaypoint ow;
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
        ow = GameObject.Find("OrderWaypoint").GetComponent<OrderWaypoint>();

        GeneratePickup();
    }

    public void GeneratePickup()
    {
        food = Random.Range(0, 11);
        id = Random.Range(0, 5);
        Transform newPickUp = PUS.GetNodeTransform(id);
        Debug.Log(food);
        if ( gm.Tod == "Day")
        {
            switch (food)
            {
                case 0:                   
                case 1:                    
                case 2:                   
                case 3:                    
                case 4:                    
                case 5:                    
                case 6:
                    Prefab = T1;
                    break;
                case 7:
                case 8:                   
                case 9:
                    Prefab = T2;
                    break;
                case 10:                   
                case 11:
                    Prefab = T3;
                    break;
                default:
                    Prefab = T1;
                    break;
            }
        }
        if (gm.Tod == "Noon")
        {
            switch (food)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    Prefab = T2;
                    break;
                case 7:
                case 8:
                case 9:
                    Prefab = T1;
                    break;
                case 10:
                case 11:
                    Prefab = T3;
                    break;
                default:
                    Prefab = T2;
                    break;
            }
        }
        if (gm.Tod == "Night")
        {
            switch (food)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    Prefab = T1;
                    break;
                case 4:
                case 5:
                case 6:                    
                case 7:
                    Prefab = T2;
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                    Prefab = T3;
                    break;
                default:
                    Prefab = T3;
                    break;
            }
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
    public Transform markPos
    {
        get { return PUS.GetNodeTransform(id); }
    }
}
