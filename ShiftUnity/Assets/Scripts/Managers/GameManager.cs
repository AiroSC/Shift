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


    int mins;
    int secs;
    [SerializeField]
    Slider qualitybar;
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
        //StartCoroutine(UpdateQualityBar());

    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Sunny" || SceneManager.GetActiveScene().name == "Game")
        {
            timer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
            score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
            //speed = GameObject.Find("Speed").GetComponent<TextMeshProUGUI>();
            qualitybar = GameObject.Find("QualityBar").GetComponent<Slider>();
            tod = GameObject.Find("TOD").GetComponent<TextMeshProUGUI>();

            time -= Time.deltaTime;
            UpdateLevelTimer(time);
            score.text = "Total Earned: " + (earned + tips).ToString();
            tod.text = TOD;
            //speed.text = "Speed: " + Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().currentSpeed).ToString();
            Debuginfo();
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
            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().Inventory)
            {
                case 1:
                    qualitybar.enabled = true;
                    quality -= T1OrderQualityDrop;
                    break;
                case 2:
                    qualitybar.enabled = true;
                    quality -= T2OrderQualityDrop;
                    break;
                case 3:
                    qualitybar.enabled = true;
                    quality -= T3OrderQualityDrop;
                    break;
                default:
                    qualitybar.enabled = false;
                    break;
            }
            qualitybar.value = quality;
            yield return new WaitForSeconds(5.0f);
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
               fuelcost = (100-GameObject.FindGameObjectWithTag("Player").GetComponent<CarControllerv2>().fuel)*20;
               repaircost = GameObject.FindGameObjectWithTag("Player").GetComponent<CarControllerv2>().damage* GameObject.FindGameObjectWithTag("Player").GetComponent<CarControllerv2>().damageMultiplier;
               SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }
    public void Earned(DeliveryObjects.CollectibleType type)
    {
        switch (type)
        {
            case DeliveryObjects.CollectibleType.T1:
                float orderEarnings = UnityEngine.Random.Range(10, 20);
                earned += orderEarnings;
                tips += orderEarnings * 0.20f;
                break;

            case DeliveryObjects.CollectibleType.T2:
                earned += UnityEngine.Random.Range(21, 50);
                break;

            case DeliveryObjects.CollectibleType.T3:
                earned += UnityEngine.Random.Range(51, 100);
                break;
        }
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
    }
    public void NewPickup()
    {
        quality = 100;
    }
}

