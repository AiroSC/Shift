using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryObjects : MonoBehaviour
{
    //Orders Order;
    Orders.Order ThisOrder;

    public void WhatAmI(Orders.Order MyOrder)
    {
        ThisOrder = MyOrder;
        TestOrder();
    }

    public void TestOrder()
    {
        Debug.Log("Current order: " + ThisOrder.name);
    }

    public enum CollectibleType
    {
        T1,T2,T3
    }

    public static CollectibleType collectibleType;
}
