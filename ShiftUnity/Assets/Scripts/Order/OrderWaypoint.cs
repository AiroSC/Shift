using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OrderWaypoint : MonoBehaviour
{

    private Image iconImg;
    private Text distText;

    public Transform target;
    public Transform car;
    public Camera cam;

    public Vector3 offset;

    public float closeIn;

    public DeliveryManager dm;


    private void Start()
    {

        /*
        GameObject pointer = new GameObject();
        pointer.AddComponent<Image>();
        pointer.GetComponent<Image>().sprite = iconImg.sprite;
        pointer.AddComponent<Text>*/
        iconImg = GetComponent<Image>();
        distText = GetComponentInChildren<Text>();
        //target = gameObject.transform;
        //car = GameObject.FindGameObjectWithTag("Player").transform;
        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        //  target = GameObject.FindGameObjectWithTag("PickUp").transform;
    }

    private void Update()
    {

        //if (target == null)
        //{
        //    target = dm.markPos;

        //}
        //else
        {
            GetDistance();
            CheckOnScrreen();
        }
    }

    private void GetDistance()
    {
        float dist = Vector3.Distance(car.position, target.position);
        distText.text = dist.ToString("f1") + "m";

        if (dist < closeIn)
        {

            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void CheckOnScrreen()
    {

        float camfolo = Vector3.Dot((target.position - cam.transform.position).normalized, cam.transform.forward);

        if (camfolo <= 0)
        {


            ToggleUI(false);
        }
        else
        {

            ToggleUI(true);
            transform.position = cam.WorldToScreenPoint(target.position);
        }

    }

    private void ToggleUI(bool _value)
    {
        iconImg.enabled = _value;
        distText.enabled = _value;
    }

}
    //public Vector3 offset;
    //public Text meter;
    //public Image img;
    //public Transform target;

    /* public void Update()
     {
         float minX = img.GetPixelAdjustedRect().width / 2;
         float maxX = Screen.width - minX;

         float minY = img.GetPixelAdjustedRect().height / 2;
         float maxY = Screen.height - minY;

         Vector3 pos = Camera.main.ScreenToWorldPoint(target.position + offset);

         if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
         { 
          if (pos.x < Screen.width/ 2)
             { pos.x = maxX; }
          else
             { pos.x = minX; }    
         }

         pos.x = Mathf.Clamp(pos.x, minX, maxX);
         pos.x = Mathf.Clamp(pos.y, minY, maxY);

         img.transform.position = pos;
         meter.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m";
  }
  */

