﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrderSystem : MonoBehaviour
{

    public Orders Order;

    Orders.Order[] CallList;
    int callListSize = 0;

    char time = '1';

    // Start is called before the first frame update
    void Start()
    {
        Order = GameObject.Find("OrderManager").GetComponent<Orders>();
        //CallOrder();
    }

    public Orders.Order CallOrder()
    {
        /*
         * Only 1 time for now
        if (time != GetTime())
        {
            time = GetTime();
            FillCallList(time);
        }
        */

        FillCallList('D');

        // Random number to determine what order in CallList is given
        int call = Random.Range(0, callListSize);


        return (CallList[call]);

        

    }
    
    void FillCallList(char time)
    {
        callListSize = 0;
        if (time == 'D')
        {
            Debug.Log("Day Orders: ");
            Orders.Order[] PossibleOrders;
            int numberOfPossibleOrders = 0;
            int callListIndex = 0;
            // Check how many orders exist
            int sizeOfOrderList = Order.OrderArray.Length;
            PossibleOrders = new Orders.Order[sizeOfOrderList];

            // Loop through list of orders, adding potential orders to PossibleOrders
            for (int i = 0; i < Order.OrderArray.Length; i++)
            {
                if (Order.OrderArray[i].rarityD > 0)
                {
                    PossibleOrders[numberOfPossibleOrders] = Order.OrderArray[i];
                    numberOfPossibleOrders++;
                }
            }

            // Get CallList size
            for (int i = 0; i < numberOfPossibleOrders; i++)
            {
                callListSize += PossibleOrders[i].rarityD;
            }
            CallList = new Orders.Order[callListSize];
            // Loop through PossibleOrders adding them to CallList
            for (int i = 0; i < numberOfPossibleOrders; i++)
            {
                for (int j = 0; j < PossibleOrders[i].rarityD; j++)
                {
                    CallList[callListIndex] = PossibleOrders[i];
                    callListIndex++;
                }
            }
        }

        /*
         * Only 1 time of day for now
        else if (time == 'N')
        {
            Debug.Log("Noon Orders: ");
            Orders.Order[] PossibleOrders;
            int numberOfPossibleOrders = 0;
            int callListIndex = 0;
            // Check how many orders exist
            int sizeOfOrderList = Order.OrderArray.Length;
            PossibleOrders = new Orders.Order[sizeOfOrderList];

            // Loop through list of orders, adding potential orders to PossibleOrders
            for (int i = 0; i < Order.OrderArray.Length; i++)
            {
                if (Order.OrderArray[i].rarityN > 0)
                {
                    PossibleOrders[numberOfPossibleOrders] = Order.OrderArray[i];
                    numberOfPossibleOrders++;
                }
            }

            // Get CallList size
            for (int i = 0; i < numberOfPossibleOrders; i++)
            {
                callListSize += PossibleOrders[i].rarityN;
            }
            CallList = new Orders.Order[callListSize];
            // Loop through PossibleOrders adding them to CallList
            for (int i = 0; i < numberOfPossibleOrders; i++)
            {
                for (int j = 0; j < PossibleOrders[i].rarityN; j++)
                {
                    CallList[callListIndex] = PossibleOrders[i];
                    callListIndex++;
                }
            }
        }
        else if (time == 'T')
        {
            Debug.Log("Night Orders: ");
            Orders.Order[] PossibleOrders;
            int numberOfPossibleOrders = 0;
            int callListIndex = 0;
            // Check how many orders exist
            int sizeOfOrderList = Order.OrderArray.Length;
            PossibleOrders = new Orders.Order[sizeOfOrderList];

            // Loop through list of orders, adding potential orders to PossibleOrders
            for (int i = 0; i < Order.OrderArray.Length; i++)
            {
                if (Order.OrderArray[i].rarityNi > 0)
                {
                    PossibleOrders[numberOfPossibleOrders] = Order.OrderArray[i];
                    numberOfPossibleOrders++;
                }
            }

            // Get CallList size
            for (int i = 0; i < numberOfPossibleOrders; i++)
            {
                callListSize += PossibleOrders[i].rarityNi;
            }
            CallList = new Orders.Order[callListSize];
            // Loop through PossibleOrders adding them to CallList
            for (int i = 0; i < numberOfPossibleOrders; i++)
            {
                for (int j = 0; j < PossibleOrders[i].rarityNi; j++)
                {
                    CallList[callListIndex] = PossibleOrders[i];
                    callListIndex++;
                }
            }
        }
        */
    }

    /*
     * Only 1 time of day for now
    char GetTime()
    {
        if (Day.isOn) return 'D';
        if (Noon.isOn) return 'N';
        if (Night.isOn) return 'T';
        else return '0';
    }
    */
}
