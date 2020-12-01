using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartShift : MonoBehaviour
{
    ToggleGroup tod;
    [SerializeField] Toggle day;
    [SerializeField] Toggle noon;
    [SerializeField] Toggle night;
    [SerializeField] GameManager gm;
    
    // Start is called before the first frame update
    void Start()
    {
        tod = GameObject.Find("Toggle Group").GetComponent<ToggleGroup>();
        day = GameObject.Find("Day").GetComponent<Toggle>();
        noon = GameObject.Find("Noon").GetComponent<Toggle>();
        night = GameObject.Find("Night").GetComponent<Toggle>();
        gm = GameObject.Find("gameManager").GetComponent<GameManager>();
        tod.RegisterToggle(day);
        tod.RegisterToggle(noon);
        tod.RegisterToggle(night);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        if (day.isOn)
        {
            gm.Tod = "Day";
        }
        if (noon.isOn)
        {
            gm.Tod = "Noon";
        }
        if (night.isOn)
        {
            gm.Tod = "Night";
        }
        SceneManager.LoadScene(1);
    }
}
