using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orders : MonoBehaviour
{

    public class Order
    {
        public string name;
        public int basePrice;
        public int rarityD;
        public int rarityN;
        public int rarityNi;
        public float averageDistance;
        public float distanceVariance;


        public Order(string nm, int bP, int rtyD, int rtyN, int rtyNi, float aD, float dV)
        {
            name = nm;
            basePrice = bP;
            rarityD = rtyD;
            rarityN = rtyN;
            rarityNi = rtyNi;
            averageDistance = aD;
            distanceVariance = dV;
        }
    }
    // Name, Base Price, Rarity (Day, Noon, Night), Average Distancce, Distance Variance
    static public Order Fries = new Order("French Fries", 10, 15, 0, 0, 0, 0);
    static public Order Pancake = new Order("Pancakes", 15, 10, 0, 0, 0, 0);
    static public Order Whiskey = new Order("Whiskey", 25, 0, 0, 10, 0, 0);
    static public Order Burger = new Order("Burger", 15, 4, 10, 15, 0, 0);

    public Order[] OrderArray = {Fries, Pancake, Whiskey, Burger};

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
