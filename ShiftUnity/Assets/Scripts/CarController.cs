using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    Vector3 moveDirection = Vector3.zero;

    public WheelCollider WheelFL;
    public WheelCollider WheelFR;
    public WheelCollider WheelRL;
    public WheelCollider WheelRR;
    public Transform WheelFLtrans;
    public Transform WheelFRtrans;
    public Transform WheelRLtrans;
    public Transform WheelRRtrans;
    public Vector3 eulertest;
    [SerializeField]
    float maxFwdSpeed = -50000;
    [SerializeField]
    float maxBwdSpeed = 10000f;
    float gravity = 9.8f;
    private bool braked = false;
    private float maxBrakeTorque = 5000;
    private Rigidbody rb;
    public Transform centreofmass;
    [SerializeField]
    private float maxTorque = 90000;
    DeliveryManager dm;
    GameManager gm;
    public float speed;
    private GameObject _Inventory;

    void Start()
    {
        dm = GameObject.Find("OrderManager").GetComponent<DeliveryManager>();
        gm = GameObject.Find("Phone").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!braked)
        {
            WheelFL.brakeTorque = 0;
            WheelFR.brakeTorque = 0;
            WheelRL.brakeTorque = 0;
            WheelRR.brakeTorque = 0;
        }
        //speed of car, Car will move as you will provide the input to it.

        WheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
        WheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");
        WheelFR.motorTorque = maxTorque * Input.GetAxis("Vertical");
        WheelFL.motorTorque = maxTorque * Input.GetAxis("Vertical");
        //changing car direction
        //Here we are changing the steer angle of the front tyres of the car so that we can change the car direction.
        WheelFR.steerAngle = 45 * Input.GetAxis("Horizontal");
        WheelFL.steerAngle = 45 * Input.GetAxis("Horizontal");

    }
    private void Update()
    {
        speed = WheelFR.rpm / 60 * 360 * Time.deltaTime;
        HandBrake();
        //for tyre rotate
        WheelFRtrans.Rotate(0, WheelFR.rpm / 60 * 360 * Time.deltaTime, 0);
        WheelFLtrans.Rotate(0, WheelFL.rpm / 60 * 360 * Time.deltaTime, 0);
        WheelRLtrans.Rotate(0, WheelRL.rpm / 60 * 360 * Time.deltaTime, 0);
        WheelRRtrans.Rotate(0, WheelRL.rpm / 60 * 360 * Time.deltaTime, 0);
        //changing tyre direction
        Vector3 temp = WheelFLtrans.localEulerAngles;
        Vector3 temp1 = WheelFRtrans.localEulerAngles;
        temp.y = WheelFL.steerAngle - (WheelFLtrans.localEulerAngles.y);
        WheelFLtrans.localEulerAngles = temp;
        temp1.y = WheelFR.steerAngle - WheelFRtrans.localEulerAngles.y;
        WheelFRtrans.localEulerAngles = temp1;
        eulertest = WheelFLtrans.localEulerAngles;
    }
    void HandBrake()
    {
        //Debug.Log("brakes " + braked);
        if (Input.GetButton("Jump"))
        {
            braked = true;
        }
        else
        {
            braked = false;
        }
        if (braked)
        {
            //rear
            WheelRL.brakeTorque = maxBrakeTorque * 5000;
            WheelRR.brakeTorque = maxBrakeTorque * 5000;
            WheelRL.motorTorque = 0;
            WheelRR.motorTorque = 0;
            //front
            //WheelFL.brakeTorque = maxBrakeTorque * 5000;
            //WheelFR.brakeTorque = maxBrakeTorque * 5000;
            //WheelFL.motorTorque = 0;
            //WheelFR.motorTorque = 0;
        }
    }

    public GameObject Inventory
    {
        get
        {
            return _Inventory;
        }
        set
        {
            _Inventory = value;
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
                    gm.Earned(DeliveryObjects.CollectibleType.T1);
                    Destroy(other.gameObject);
                    break;

                case DeliveryObjects.CollectibleType.T2:
                    Debug.Log("Collide T2");
                    dm.GenerateDelivery();
                    gm.Earned(DeliveryObjects.CollectibleType.T2);
                    Destroy(other.gameObject);
                    break;

                case DeliveryObjects.CollectibleType.T3:
                    Debug.Log("Collide T3");
                    dm.GenerateDelivery();
                    gm.Earned(DeliveryObjects.CollectibleType.T3);
                    Destroy(other.gameObject);
                    break;
            }
        }

        if(other.gameObject.CompareTag("DropOff"))
        {
            Debug.Log("Delivered");
            dm.GeneratePickup();
            Destroy(other.gameObject);

        }
    }
}
