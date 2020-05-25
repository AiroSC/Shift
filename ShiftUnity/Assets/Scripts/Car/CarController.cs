using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
    [Header("Settings")]
    public bool motor;
    public bool steering;
    public bool hasBrakes;

    [Header("References")]
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
}

public class CarController : MonoBehaviour
{

    [Header("Info")]
    public float currentSpeed;
    public Vector3 currentVelocity;

    [Header("Settings")]
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque = 1000f;
    public float maxSteeringAngle = 35f;
    public float currentBrakeTorque = 0f;

    // COMPONENTS
    private Rigidbody rb;
    DeliveryManager dm;
    GameManager gm;

    int pickupTier = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dm = GameObject.Find("OrderManager").GetComponent<DeliveryManager>();
        gm = GameObject.Find("gameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // BRAKE
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentBrakeTorque = 10000f;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            currentBrakeTorque = 0f;
        }

        // BOOST
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(transform.forward * 10000f, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // SPEED
        currentVelocity = rb.velocity;
        currentSpeed = rb.velocity.magnitude;

        // INPUT
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            if (axleInfo.hasBrakes)
            {
                axleInfo.leftWheel.brakeTorque = currentBrakeTorque;
                axleInfo.rightWheel.brakeTorque = currentBrakeTorque;
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    //============================================
    // FUNCTIONS (CUSTOM)
    //============================================

    // FINDS THE VISUAL WHEEL, CORRECTLY APPLIES THE TRANSFORM
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) return;

        // GET WHEEL
        Transform visualWheel = collider.transform.GetChild(0);

        // GET POS/ROT
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        // APPLY POS/ROT
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
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