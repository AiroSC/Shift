using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            return instance;
        }
    }

    private static GameManager instance = null;
    [SerializeField]
    float time;
    [Header("UI")]
    [SerializeField]
    TextMeshProUGUI timer;
    [SerializeField]
    public TextMeshProUGUI score;
    [SerializeField]
    TextMeshProUGUI tod;
    //[SerializeField]
    //TextMeshProUGUI speed;
    private float earned;
    private float tips;
    private float fuelcost;
    private float repaircost;

    [Header("Calculations")]
    [SerializeField] float fuelPrice = 0;
    int mins;
    int secs;
    [SerializeField]
    List<Slider> qualitybar;
    [SerializeField]
    Slider fuel;
    int quality;
    private const float gametime = 600.0f;
    private const int T1OrderQualityDrop = 1;
    private const int T2OrderQualityDrop = 3;
    private const int T3OrderQualityDrop = 5;
    string TOD;

    void Awake()
    {
        //Check if there is an existing instance of this object
        if ((instance) && (instance.GetInstanceID() != GetInstanceID()))
        {
            DestroyImmediate(gameObject); //Delete duplicate
        }
        else
        {
            instance = this; //Make this object the only instance
            DontDestroyOnLoad(gameObject); //Set as do not destroy
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (quality <= 0)
            quality = 100;
        if (time <= 0)
            time = gametime;
        earned = 0;
        if (fuelPrice <= 0)
            fuelPrice = 12;
        if (SceneManager.GetActiveScene().name == "Sunny" || SceneManager.GetActiveScene().name == "Game")
        {
            fuel.value = 100;
            tod.text = TOD = "Day";
        }


        //StartCoroutine(UpdateQualityBar());
        StartCoroutine(UpdateFuelBar());
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Sunny" || SceneManager.GetActiveScene().name == "Game")
        {
            timer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
            score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
            qualitybar[0] = GameObject.Find("QualityBar").GetComponent<Slider>();
            qualitybar[1] = GameObject.Find("QualityBar1").GetComponent<Slider>();
            qualitybar[2] = GameObject.Find("QualityBar2").GetComponent<Slider>();
            tod = GameObject.Find("TOD").GetComponent<TextMeshProUGUI>();
            fuel = (GameObject.Find("Fuel").GetComponent<Slider>());
            time -= Time.deltaTime;
            UpdateLevelTimer(time);
            score.text = "Total Earned: " + (earned + tips).ToString();
            tod.text = TOD;
            //speed.text = "Speed: " + Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().currentSpeed).ToString();
            //Debuginfo();
        }
       
    }

    private void Debuginfo()
    {
        Debug.Log("time:" + time);
        Debug.Log("score:" + earned);
        //Debug.Log("speed:" + Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().currentSpeed));
        Debug.Log("quality:" + quality);
    }
    IEnumerator UpdateQualityBar()
    {
        while (true)
        {
            foreach(Slider s in qualitybar)
            {
                switch (GameObject.FindGameObjectWithTag("PickUp").GetComponent<DeliveryObjects>().ordered.orders[0].tier)
                {
                    case 1:
                        s.enabled = true;
                        quality -= T1OrderQualityDrop;
                        break;
                    case 2:
                        s.enabled = true;
                        quality -= T2OrderQualityDrop;
                        break;
                    case 3:
                        s.enabled = true;
                        quality -= T3OrderQualityDrop;
                        break;
                    default:
                        s.enabled = false;
                        break;
                }
                s.value = quality;

            }
            yield return new WaitForSeconds(5.0f);
        }
    }
    IEnumerator UpdateFuelBar()
    {
        while (true)  
        { 
            yield return new WaitForSeconds(5.0f);
            fuel.value -= GameObject.FindGameObjectWithTag("Player").GetComponent<Attributes>().fuelConsumption;
        }
    }
    public void UpdateLevelTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);
        string formatedSeconds = seconds.ToString();
        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }
        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        if(minutes <=0 && seconds <= 0)
        {
               fuelcost = (100-fuel.value)*fuelPrice;
               repaircost = GameObject.FindGameObjectWithTag("Player").GetComponent<Attributes>().damage * GameObject.FindGameObjectWithTag("Player").GetComponent<Attributes>().damageMultiplier;
            if(repaircost <= 0 ) 
            {
                repaircost = 0;
            }
            else if (repaircost <= 20 )
            {
                repaircost = 25;
            }
            else if (repaircost <= 40)
            {
                repaircost = 55;
            }
            else if (repaircost <= 60)
            {
                repaircost = 80;
            }
            else if (repaircost <= 80)
            {
                repaircost = 115;
            }
            else
                repaircost *= 2;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }
    public void Earned(int money)
    {
        earned += money;
        tips += (money * 0.20f);
    }

    
    public float EARNED
    {
        get
        {
            return earned;
        }
        
    }

    public float TIPPED
    {
        get
        {
            return tips;
        }
    }
    public float FUEL
    {
        get
        {
            return fuelcost;
        }
    }
    public float DAMAGE
    {
        get
        {
            return repaircost;
        }
    }
    public string Tod
    {
        get
        {
            return  TOD;
        }
        set
        {
            TOD = value;
        }
    }

    public void restart()
    {
        time += gametime;
        earned = 0;
        quality = 100;
        fuel.value = 100;
    }
    public void NewPickup()
    {
        quality = 100;
    }
}

