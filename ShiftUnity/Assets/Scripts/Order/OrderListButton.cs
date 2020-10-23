using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderListButton : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI restaurant;
    [SerializeField]
    private TextMeshProUGUI OrderNo;
    [SerializeField]
    private TextMeshProUGUI distance;
    [SerializeField]
    private TextMeshProUGUI tier;
    [SerializeField]
    private TextMeshProUGUI area;
    [SerializeField]
    private OrderListControl orderControl;
    



    private void Awake()
    {
        orderControl = FindObjectOfType<OrderListControl>();
        
    }

    void Start()
    {
       
        //orderControl.Timeout(OrderNo.text, lifeTime);
        Destroy(gameObject, orderControl.LIFETIME);
    }

    public void SetText(string restaurant, string ONo, string distance, string tier, string area)
    {
        this.restaurant.text = restaurant;
        this.OrderNo.text = ONo;
        this.distance.text = distance;
        this.tier.text = tier;
        this.area.text = area;
    }
     

    public void Onclick()
    {
        orderControl.orderClicked(OrderNo.text);
        Destroy(gameObject);
    }

}
