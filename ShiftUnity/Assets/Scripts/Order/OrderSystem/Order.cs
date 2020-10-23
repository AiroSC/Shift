using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Order
{
    public int money;
    public Transform pos;
    public int tier;
    public string area;
    public Order( int money, Transform drop, int tier, string area)
    {
        
        this.money = money;
        pos = drop;
        this.tier = tier;
        this.area = area;
    }
    
}

[System.Serializable]
public class Restaurant
{
    public int orderNo;
    public string name;
    public Transform pos;
    public int distance;
    public List<Order> orders;

    public Restaurant(int oN,string name, Transform pos, int distance, List<Order> orders)
    {
        orderNo = oN;
        this.name = name;
        this.pos = pos;
        this.distance = distance;
        this.orders = orders;
    }
    public Restaurant( string name, Transform pos, int distance, List<Order> orders)
    {
        orderNo = 0;
        this.name = name;
        this.pos = pos;
        this.distance = distance;
        this.orders = orders;
    }
}

[System.Serializable]
public class Area
{
    public string name;
    public DropOffSpawn DOS;

    public Area(string name, DropOffSpawn DOS)
    {
        this.name = name;
        this.DOS = DOS;
    }
}