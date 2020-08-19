using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarTruck : System.Object
{
    public bool UseForDirectional = false;
    public TruckWheel LeftWheel;
    public TruckWheel RightWheel;

}

[System.Serializable]
public class TruckWheel : System.Object
{
    public WheelCollider WheelCollider;
    public GameObject WheelMesh;
    public bool HasTorque = false;
    public bool HasBreake = false;

    public float GetCurrentSpeed()
    {
        return 2f * 3.14f * WheelCollider.radius * WheelCollider.rpm;
    }

}

public class CarControllerv2 : MonoBehaviour
{
    [Header("Settings")]
    public float TorqueForce = 1000f;
    public float BreakeForce = 2000f;
    public float TorqueFriction = 100f;
    public float DirectionForce = 200f;
    public float DirectionDelay = 100f;
    public bool EnableDebug = false;
    public List<CarTruck> Trucks;
    int pickupTier = 0;
    public Transform CoM;

    [Header("Attributes")]
    public float TipRate = 0.10f;
    public float speed;
    public float fuel = 100f;
    public float fuelConsumption = 2f;
    public float damage = 100f;
    public float damageMultiplier = 2f;
    public float maintenance = 60.0f;
    public float Insurance = 100f;
    public float storage = 30f;

    private Rigidbody rigidbody;
    DeliveryManager dm;
    GameManager gm;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        //rigidbody.centerOfMass = CoM.position.normalized;
        dm = GameObject.Find("OrderManager").GetComponent<DeliveryManager>();
        gm = GameObject.Find("gameManager").GetComponent<GameManager>();
    }
    public float GetCurrentSpeed()
    {
        float total = 0;
        foreach (CarTruck truck in Trucks)
        {
            total += truck.LeftWheel.GetCurrentSpeed() + truck.RightWheel.GetCurrentSpeed();
        }

        return total / (Trucks.Count / 2);
    
    }

    void FixedUpdate()
    {
        rigidbody = GetComponent<Rigidbody>();

        foreach (CarTruck truck in Trucks)
        { 
            Vector3 position;
            Quaternion rotation;

            truck.LeftWheel.WheelCollider.GetWorldPose(out position, out rotation);
            //rotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
            truck.LeftWheel.WheelMesh.transform.rotation = rotation;

            truck.RightWheel.WheelCollider.GetWorldPose(out position, out rotation);
            //rotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
            truck.RightWheel.WheelMesh.transform.rotation = rotation;



            if (truck.LeftWheel.HasTorque)
                truck.LeftWheel.WheelCollider.motorTorque = Input.GetAxis("Vertical") * TorqueForce;
            if (truck.RightWheel.HasTorque)
                truck.RightWheel.WheelCollider.motorTorque = Input.GetAxis("Vertical") * TorqueForce;

            float currentFriction;
            if (Input.GetKey(KeyCode.Space))
            {
                currentFriction = BreakeForce;
            }
            else
            {
                currentFriction = TorqueFriction - Mathf.Abs(Input.GetAxis("Vertical") * TorqueFriction);
            }
            //float currentFriction = Input.GetKey(KeyCode.Space) ? BreakeForce : TorqueFriction - Mathf.Abs(Input.GetAxis("Vertical") * TorqueFriction);

            if (truck.LeftWheel.HasBreake)
                truck.LeftWheel.WheelCollider.brakeTorque = currentFriction;
            if (truck.RightWheel.HasBreake)
                truck.RightWheel.WheelCollider.brakeTorque = currentFriction;

            if (truck.UseForDirectional)
            {
                float directional = Mathf.Lerp(truck.LeftWheel.WheelCollider.steerAngle, DirectionForce * Input.GetAxis("Horizontal"), Time.deltaTime * DirectionDelay);
                truck.LeftWheel.WheelCollider.steerAngle = directional;
                truck.RightWheel.WheelCollider.steerAngle = directional;
            }

            if (EnableDebug)
            {
                Debug.Log("motorTorque: " + truck.LeftWheel.WheelCollider.motorTorque + " | brakeFriction: " + currentFriction);
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            switch (DeliveryObjects.collectibleType)
            {
                case DeliveryObjects.CollectibleType.T1:
                    Debug.Log("Collide T1");
                    dm.GenerateDelivery();
                    pickupTier = 1;
                    Destroy(other.gameObject);
                    break;

                case DeliveryObjects.CollectibleType.T2:
                    Debug.Log("Collide T2");
                    dm.GenerateDelivery();
                    pickupTier = 2;
                    Destroy(other.gameObject);
                    break;

                case DeliveryObjects.CollectibleType.T3:
                    Debug.Log("Collide T3");
                    dm.GenerateDelivery();
                    pickupTier = 3;
                    Destroy(other.gameObject);
                    break;
            }
        }
        if (other.gameObject.CompareTag("DropOff"))
        {
            switch (pickupTier)
            {
                case 1:
                    Debug.Log("DropOff Collide T1");
                    gm.Earned(DeliveryObjects.CollectibleType.T1);
                    Destroy(other.gameObject);
                    break;

                case 2:
                    Debug.Log("DropOff Collide T2");
                    gm.Earned(DeliveryObjects.CollectibleType.T2);
                    Destroy(other.gameObject);
                    break;

                case 3:
                    Debug.Log("DropOff Collide T3");
                    gm.Earned(DeliveryObjects.CollectibleType.T3);
                    Destroy(other.gameObject);
                    break;
            }
            pickupTier = 0;
            dm.GeneratePickup();
        }

    }
    public int Inventory
    {
        get
        {
            return pickupTier;
        }
    }
}
