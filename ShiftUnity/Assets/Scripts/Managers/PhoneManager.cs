using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
   public GameObject minimap;
   public GameObject orders;
    bool activateM = true;
    bool activateO = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
       {
            activateM = !activateM;
            activateO = !activateO;
            
            minimap.SetActive(activateM);
            orders.SetActive(activateO);

          
        }

    }
}
