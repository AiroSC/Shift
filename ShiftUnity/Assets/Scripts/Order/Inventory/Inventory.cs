using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxOrders = 3;
    int currentOrdersInInventory = 0;

    Orders od;
    public Orders.Order[] ordersInInventory;

    public OrderSystem os;

    // Start is called before the first frame update
    void Start()
    {
        od = GameObject.Find("OrderManager").GetComponent<Orders>();

        os = GameObject.Find("OrderManager").GetComponent<OrderSystem>();

        ordersInInventory = new Orders.Order[maxOrders];
        currentOrdersInInventory = 0;

        AddEmptySlotsToInventory();

        Invoke("TestFillArray", 1f);

    }

    void TestFillArray()
    {
        AddToInventory(os.GenerateOrder());
        AddToInventory(os.GenerateOrder());
        AddToInventory(os.GenerateOrder());
        AddToInventory(os.GenerateOrder());

        Debug.Log("Inventory Slot 1: " + ordersInInventory[0].name);
        Debug.Log("Inventory Slot 2: " + ordersInInventory[1].name);
        Debug.Log("Inventory Slot 3: " + ordersInInventory[2].name);

        RemoveFromInventory(FindOrderInInventory(Orders.Fries));

        AddToInventory(os.GenerateOrder());

        Debug.Log("Inventory Slot 1: " + ordersInInventory[0].name);
        Debug.Log("Inventory Slot 2: " + ordersInInventory[1].name);
        Debug.Log("Inventory Slot 3: " + ordersInInventory[2].name);

        Debug.Log("Inventory Slot 1 key: " + ordersInInventory[0].key);
        Debug.Log("Inventory Slot 2 key: " + ordersInInventory[1].key);
        Debug.Log("Inventory Slot 3 key: " + ordersInInventory[2].key);

    }



    public void AddToInventory(Orders.Order orderToAdd)
    {
        if (currentOrdersInInventory < maxOrders)
        {
            for (int i = 0; i < maxOrders; i++)
            {
                if (ordersInInventory[i] == null)
                {
                    Debug.Log("Slot " + i + " is empty; adding " + orderToAdd.name);
                    ordersInInventory[i] = orderToAdd;
                    break;
                }
            }
            currentOrdersInInventory++;
        }
        else
        {
            Debug.Log("Inventory Full");
        }
    }

    public int FindOrderInInventory(Orders.Order orderToFind)
    {
        for (int i = 0; i < maxOrders; i++)
        {
            if (ordersInInventory[i] == orderToFind)
            {
                Debug.Log(orderToFind.name + " found in slot " + i);
                return i;
            }
        }
        Debug.Log(orderToFind.name + " not found");
        return -1;
    }

    public void RemoveFromInventory(int orderToRemove)
    {
        Debug.Log(ordersInInventory[orderToRemove].name + " removed from inventory");
        ordersInInventory[orderToRemove] = null;
        currentOrdersInInventory--;
    }

    void AddEmptySlotsToInventory()
    {
        for (int i = 0; i < maxOrders; i++)
        {
            ordersInInventory[i] = null;
        }
    }
}
