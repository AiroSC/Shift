using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{

    //public Transform target;

    //public float smoothSpeed = 0.125f;
    //public Vector3 offset;

    //void FixedUpdate()
    //{
    //    Vector3 desiredPosition = target.position + offset;
    //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    //    transform.position = smoothedPosition;

    //    transform.LookAt(target);
    //}

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    [SerializeField]
    InputField y; 
    [SerializeField]
    InputField z;
    private void Start()
    {
        
    }
    private void Update()
    {
        //y.text = offsetPosition.y.ToString();
        //z.text = offsetPosition.z.ToString();
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }

    public void CameraAngle()
    {
        InputField.OnChangeEvent onYChange = y.onValueChanged;
        {
            offsetPosition.y = int.Parse(y.text);
        };
        InputField.OnChangeEvent onXChange = z.onValueChanged;
        {
            offsetPosition.y = int.Parse(y.text);
        };
    }
        
}