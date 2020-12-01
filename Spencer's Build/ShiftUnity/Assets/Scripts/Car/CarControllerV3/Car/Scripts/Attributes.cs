using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class Attributes : MonoBehaviour
{
    

    [Header("Attributes")]
    public float TipRate = 0.10f;
    public float speed;
    public float fuel = 100f;
    public float fuelConsumption = 2f;
    public float damage = 0f;
    public float damageMultiplier = 2f;
    public float maintenance = 60.0f;
    public float Insurance = 100f;
    public float storage = 30f;

    [Header("Collision")]
    [SerializeField]
    GameObject hitEffect;
    DeliveryManager dm;
    GameManager gm;
    void Awake()
    {
        dm = GameObject.Find("OrderManager").GetComponent<DeliveryManager>();
        gm = GameObject.Find("gameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        speed = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().CurrentSpeed;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            switch (other.gameObject.GetComponent<DeliveryObjects>().collectibleType)
            {
                case DeliveryObjects.CollectibleType.T1:
                    Debug.Log("Collide T1");
                    dm.GenerateDelivery(other.gameObject.GetComponent<DeliveryObjects>().ordered );
                    Destroy(other.gameObject);
                    break;

                case DeliveryObjects.CollectibleType.T2:
                    Debug.Log("Collide T2");
                    dm.GenerateDelivery(other.gameObject.GetComponent<DeliveryObjects>().ordered ); 
                    Destroy(other.gameObject);
                    break;

                case DeliveryObjects.CollectibleType.T3:
                    Debug.Log("Collide T3");
                    dm.GenerateDelivery(other.gameObject.GetComponent<DeliveryObjects>().ordered ); 
                    Destroy(other.gameObject);
                    break;
                case DeliveryObjects.CollectibleType.DropOff:
                    Debug.Log("Collide T3");
                    gm.Earned(other.gameObject.GetComponent<DeliveryObjects>().ordered.orders[0].money);
                    Destroy(other.gameObject);
                    break;

            }
        } 
    }

    public void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
            Instantiate(hitEffect, contact.point, Quaternion.identity);
            damage++;
            //Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }
}
