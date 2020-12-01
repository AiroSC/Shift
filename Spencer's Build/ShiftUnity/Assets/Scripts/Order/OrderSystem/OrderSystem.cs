
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temporary
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OrderSystem : MonoBehaviour
{
    int orderKey;
    Orders Order;

    Orders.Order[] CallList;
    int callListSize = 0;

    string time;

    // Temporary
    public Text orderOutput;

    GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("gameManager").GetComponent<GameManager>();
        time = gm.Tod;
        FillCallList(gm.Tod);
        orderKey = 0;
        CallOrder();
    }

    public Orders.Order GenerateOrder()
    {
        Orders.Order generatedOrder;
        orderKey++;
        Debug.Log("Order Key: " + orderKey);
        int call = Random.Range(0, callListSize);
        generatedOrder = CallList[call];
        generatedOrder.key = orderKey;
        return generatedOrder;
    }

    public void CallOrder()
    {
        FillCallList(time);

        // Testing
        /* 
         Debug.Log("CallListSize: " + callListSize);
         for (int i = 0; i < callListSize - 1; i++)
         {
             Debug.Log(CallList[i].name);
         }
         */

        // Random number to determine what order in CallList is given

        int call = Random.Range(0, callListSize);

        orderOutput.text = CallList[call].name;
    }
    
    void FillCallList(string TOD)
    {
        callListSize = 0;
        if (TOD == "Day")
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
        else if (TOD == "Noon")
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
        else if (TOD == "Night")
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
    }
    
}
