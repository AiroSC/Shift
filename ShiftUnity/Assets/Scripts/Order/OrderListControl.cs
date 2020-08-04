using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderListControl : MonoBehaviour
{

    [SerializeField]
    private GameObject orderTemplate;

    [SerializeField]
    private int[] intArray;

    private List<GameObject> Order;

    void Start()
    {
        Order = new List<GameObject>();

        if (Order.Count > 0)
        {
            foreach (GameObject order in Order)
            {
                Destroy(order.gameObject);
            }

            Order.Clear();

            foreach (int i in intArray)

            {
                GameObject order = Instantiate(orderTemplate) as GameObject;
                order.SetActive(true);

                order.GetComponent<OrderListButton>().SetText("Order #" + i);

                order.transform.SetParent(orderTemplate.transform.parent, false);
            }
        }
    }
    

    public void orderClicked(string myTextString)
    {
        Debug.Log(myTextString);
    }
}
