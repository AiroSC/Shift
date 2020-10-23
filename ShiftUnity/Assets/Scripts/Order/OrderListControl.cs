using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderListControl : MonoBehaviour
{

    [SerializeField] OrderManager om;
    [SerializeField] private GameObject orderTemplate;
    [Header("Display Viewport")]
    public GameObject viewport;
    [Header("Restaurant Data")]
    [SerializeField] public List<Restaurant> activeOrders;
    private bool refresh = true;
    [Header("Order")]
    [SerializeField] GameObject T1;
    [SerializeField] GameObject T2;
    [SerializeField] GameObject T3;
    [SerializeField] float SpawnRate = 0.0f;
    [SerializeField] float lifeTime = 0;


    private void Awake()
    {
        om = GameObject.Find("OrderManager").GetComponent<OrderManager>();
        if (SpawnRate <= 0.0f)
            SpawnRate = 3.0f;
        if (lifeTime <= 0.0f)
            lifeTime = 10.0f;
    }
    private void Start()
    {

        StartCoroutine(OrderGeneration());
        StartCoroutine(Timeout(lifeTime));
    }
    IEnumerator OrderGeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnRate);
            Debug.Log("Order Available");

            activeOrders.Add(om.PushOutOrder());
            refresh = true; 
        }
    }

    public void DisplayAllOrders()
    {

        for (int i = 0; i < om.restaurantsData.Length; i++)
        {
            for (int j = 0; j < om.restaurantsData[i].orders.Count; j++)
            {

                GameObject order = Instantiate(orderTemplate) as GameObject;
                order.SetActive(true);

                order.GetComponent<OrderListButton>().SetText(om.restaurantsData[i].name, om.restaurantsData[i].orderNo.ToString(), om.restaurantsData[i].distance.ToString(), om.restaurantsData[i].orders[j].tier.ToString(), om.restaurantsData[i].orders[j].area);

                order.transform.SetParent(viewport.transform, false);
            }
        }


    }
    private void Update()
    {

        if (refresh)
        {

            for (int i = 0; i < viewport.transform.childCount; i++)
            {
                Destroy(viewport.transform.GetChild(i).gameObject);
            }
            foreach (Restaurant r in activeOrders)
            {

                GameObject order = Instantiate(orderTemplate) as GameObject;
                order.SetActive(true);

                order.GetComponent<OrderListButton>().SetText(r.name, r.orderNo.ToString(), r.distance.ToString(), r.orders[r.orders.Count - 1].tier.ToString(), r.orders[r.orders.Count - 1].area);

                order.transform.SetParent(viewport.transform, false);

            }
        }
        refresh = false;

    }
    public void orderClicked(string orderno)
    {
        Restaurant temp = GetRestaurant(orderno);
        if (temp != null)
        {
            GameObject pickup;
            if (temp.orders[0].tier == 1)
            {
                pickup = Instantiate(T1, temp.pos.position, Quaternion.identity);
            }
            else if (temp.orders[0].tier == 2)
            {
                pickup = Instantiate(T2, temp.pos.position, Quaternion.identity);
            }
            else
            {
                pickup = Instantiate(T3, temp.pos.position, Quaternion.identity);
            }
            pickup.GetComponent<DeliveryObjects>().ordered = temp;
            if (activeOrders.Contains(temp))
            {
                activeOrders.Remove(temp);
            }
        }

    }

    IEnumerator Timeout(float lifetime)
    {
        while (true)
        {
            yield return new WaitForSeconds(lifetime); 
            if (activeOrders.Count > 2)
                activeOrders.Remove(activeOrders[0]); 
        }
    }

    public Restaurant GetRestaurant(string orderno)
    {
        foreach (Restaurant r in activeOrders)
        {
            if (r.orderNo.ToString() == orderno)
            {
                return r;
            }
        }
        return null;
    }

    public float LIFETIME
    {
        get
        {
            return lifeTime;
        }
    }
}
