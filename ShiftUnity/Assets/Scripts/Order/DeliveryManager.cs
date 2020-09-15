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
    [Header("Day")]
    public int type1Day = 7;
    public int type2Day = 6;
    public int type3Day = 3;

    [Header("Noon")]
    public int type1Noon = 6;
    public int type2Noon = 8;
    public int type3Noon = 3;

    [Header("Night")]
    public int type1Night = 6;
    public int type2Night = 6;
    public int type3Night = 3;
    private void Start()
    {
        DOS = GameObject.Find("DropOff").GetComponent<DropOffSpawn>();
        PUS = GameObject.Find("PickUp").GetComponent<PickUpSpawn>();
        gm = GameObject.Find("gameManager").GetComponent<GameManager>();

        GeneratePickup();
    }

    public void GeneratePickup()
    {

        id = Random.Range(0, 5);
        Transform newPickUp = PUS.GetNodeTransform(id);
        Debug.Log(food);
        if (gm.Tod == "Day")
        {
            //type1day =7 / type2day = 6 / type3day = 3
            food = Random.Range(1, (type1Day + type2Day + type3Day));
            if (food <= type1Day)
            {
                Prefab = T1;
            }
            else if (food <= type1Day + type2Day)
            {
                Prefab = T2;
            }
            else if (food <= type1Day + type2Day + type3Day)
            {
                Prefab = T3;
            }
        }
        if (gm.Tod == "Noon")
        {
            food = Random.Range(1, (type1Noon + type2Noon + type3Noon));
            if (food <= type1Noon)
            {
                Prefab = T1;
            }
            else if (food <= type1Noon + type2Noon)
            {
                Prefab = T2;
            }
            else if (food <= type1Noon + type2Noon + type3Noon)
            {
                Prefab = T3;
            }
        }
        if (gm.Tod == "Night")
        {
            food = Random.Range(1, (type1Night + type2Night + type3Night));
            if (food <= type1Night)
            {
                Prefab = T1;
            }
            else if (food <= type1Night + type2Night)
            {
                Prefab = T2;
            }
            else if (food <= type1Night + type2Night + type3Night)
            {
                Prefab = T3;
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
