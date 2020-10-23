using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryObjects : MonoBehaviour
{

    public enum CollectibleType
    {
        T1,T2,T3,DropOff
    }

    public CollectibleType collectibleType;

    public Restaurant ordered;

    private void Start()
    {
        switch (collectibleType)
        {
            case DeliveryObjects.CollectibleType.T1:
            case DeliveryObjects.CollectibleType.T2: 
            case DeliveryObjects.CollectibleType.T3:
                transform.position = ordered.pos.position;
                break;
            case DeliveryObjects.CollectibleType.DropOff:
                transform.position = ordered.orders[0].pos.position;
                break;

        }
       
    }
}
