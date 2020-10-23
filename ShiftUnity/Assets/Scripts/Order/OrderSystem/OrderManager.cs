using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    GameObject Player;
    PickUpSpawn PUS;
    GameManager gm;
     
    [Header("Restaurants")]
    [SerializeField]
    public List<Restaurant> restaurants;

    [Header("Restaurants")]
    private int id;
    private int distance;
    List<Order> orders;


    [SerializeField]
    List<Area> areas;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //areas.Add( new Area("Downtown RapidCity", GameObject.Find("DropOff").GetComponent<DropOffSpawn>()));
        //areas.Add( new Area("Outskirts RapidCity", GameObject.Find("DropOff").GetComponent<DropOffSpawn>()));
        //areas.Add( new Area("Suburbs RapidCity", GameObject.Find("DropOff").GetComponent<DropOffSpawn>()));
        PUS = GameObject.Find("PickUp").GetComponent<PickUpSpawn>();
        gm = GameObject.Find("gameManager").GetComponent<GameManager>();
    }



    void Start()
    {
       
        id = Random.Range(0, PUS.GetLength());
        distance = (int)Vector3.Distance(PUS.GetNodeTransform(id).position, Player.transform.position);
        generateOrders();
        restaurants.Add(new Restaurant("Razzy's HotDogs", PUS.GetNodeTransform(id), distance, orders));

        id = Random.Range(0, PUS.GetLength());
        distance = (int)Vector3.Distance(PUS.GetNodeTransform(id).position, Player.transform.position);
        generateOrders();
        restaurants.Add(new Restaurant("Cluck&Cluck", PUS.GetNodeTransform(id), distance, orders));

        id = Random.Range(0, PUS.GetLength());
        distance = (int)Vector3.Distance(PUS.GetNodeTransform(id).position, Player.transform.position);
        generateOrders();
        restaurants.Add(new Restaurant("RapidRavioli", PUS.GetNodeTransform(id), distance, orders));
    }

    void Update()
    {

    }

    void generateOrders()
    {
        int temp1 = Random.Range(0, areas.Count);
        int temp2 = Random.Range(0, areas.Count);
        int temp3 = Random.Range(0, areas.Count);
        int temp4 = Random.Range(0, areas.Count);
        int temp5 = Random.Range(0, areas.Count);
        int temp6 = Random.Range(0, areas.Count);
        int temp7 = Random.Range(0, areas.Count);
        if (gm.Tod == "Day")
        {
            orders = new List<Order>
            {
            new Order(10,areas[temp1].DOS.GetNodeTransform(Random.Range(0, areas[temp1].DOS.GetLength())), 1 ,areas[temp1].name),
            new Order(12,areas[temp2].DOS.GetNodeTransform(Random.Range(0, areas[temp2].DOS.GetLength())), 1 ,areas[temp2].name),
            new Order(16,areas[temp3].DOS.GetNodeTransform(Random.Range(0, areas[temp3].DOS.GetLength())), 1 ,areas[temp3].name),
            new Order(18,areas[temp4].DOS.GetNodeTransform(Random.Range(0, areas[temp4].DOS.GetLength())), 1 ,areas[temp4].name),
            new Order(23,areas[temp5].DOS.GetNodeTransform(Random.Range(0, areas[temp5].DOS.GetLength())), 2 ,areas[temp5].name),
            new Order(28,areas[temp6].DOS.GetNodeTransform(Random.Range(0, areas[temp6].DOS.GetLength())), 2 ,areas[temp6].name),
            new Order(80,areas[temp7].DOS.GetNodeTransform(Random.Range(0, areas[temp7].DOS.GetLength())), 3 ,areas[temp7].name)
            };
        }
        else if (gm.Tod == "Noon")
        {
            orders = new List<Order>
            {
            new Order(18,areas[temp1].DOS.GetNodeTransform(Random.Range(0, areas[temp1].DOS.GetLength())), 1 ,areas[temp1].name),
            new Order(20,areas[temp2].DOS.GetNodeTransform(Random.Range(0, areas[temp2].DOS.GetLength())), 1 ,areas[temp2].name),
            new Order(29,areas[temp3].DOS.GetNodeTransform(Random.Range(0, areas[temp3].DOS.GetLength())), 2 ,areas[temp3].name),
            new Order(35,areas[temp4].DOS.GetNodeTransform(Random.Range(0, areas[temp4].DOS.GetLength())), 2 ,areas[temp4].name),
            new Order(42,areas[temp5].DOS.GetNodeTransform(Random.Range(0, areas[temp5].DOS.GetLength())), 2 ,areas[temp5].name),
            new Order(60,areas[temp6].DOS.GetNodeTransform(Random.Range(0, areas[temp6].DOS.GetLength())), 2 ,areas[temp6].name),
            new Order(110,areas[temp7].DOS.GetNodeTransform(Random.Range(0, areas[temp7].DOS.GetLength())), 3 ,areas[temp7].name)
            };
        }
        else if (gm.Tod == "Night")
        {
            orders = new List<Order>
            {
            new Order(32,areas[temp1].DOS.GetNodeTransform(Random.Range(0, areas[temp1].DOS.GetLength())), 1 ,areas[temp1].name),
            new Order(46,areas[temp2].DOS.GetNodeTransform(Random.Range(0, areas[temp2].DOS.GetLength())), 1 ,areas[temp2].name),
            new Order(55,areas[temp3].DOS.GetNodeTransform(Random.Range(0, areas[temp3].DOS.GetLength())), 2 ,areas[temp3].name),
            new Order(62,areas[temp4].DOS.GetNodeTransform(Random.Range(0, areas[temp4].DOS.GetLength())), 2 ,areas[temp4].name),
            new Order(68,areas[temp5].DOS.GetNodeTransform(Random.Range(0, areas[temp5].DOS.GetLength())), 2 ,areas[temp5].name),
            new Order(105,areas[temp6].DOS.GetNodeTransform(Random.Range(0, areas[temp6].DOS.GetLength())), 3 ,areas[temp6].name),
            new Order(120,areas[temp7].DOS.GetNodeTransform(Random.Range(0, areas[temp7].DOS.GetLength())), 3 ,areas[temp7].name)
            };
        }
        
        
    }

    public Restaurant PushOutOrder()
    {
        
        int Rid = Random.Range(0, restaurants.Count);
        int Oid = Random.Range(0, restaurants[Rid].orders.Count);
        
        string name = restaurants[Rid].name;
        Transform Rpos = restaurants[Rid].pos;
        int distance = restaurants[Rid].distance;

        int orderNo = Random.Range(100, 4000);
        int money = restaurants[Rid].orders[Oid].money;
        Transform Opos = restaurants[Rid].orders[Oid].pos;
        int tier = restaurants[Rid].orders[Oid].tier;
        string area = restaurants[Rid].orders[Oid].area;

        return new Restaurant(orderNo, name, Rpos, distance, new List<Order> { new Order( money, Opos, tier, area) });
    }
    public Restaurant[] restaurantsData
    {
        get
        {
            return restaurants.ToArray();
        }
    }

}
