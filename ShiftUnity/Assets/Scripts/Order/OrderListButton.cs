using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    [SerializeField]
    private OrderListControl orderControl;

 
    private string myTextString;
    public void SetText(string textString)
     {
         myTextString = textString;
         myText.text = textString;
     }
     

    public void Onclick()
    {
        orderControl.orderClicked(myTextString);
    }

}
