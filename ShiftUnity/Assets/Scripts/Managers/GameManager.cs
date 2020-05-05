using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    Text timer;
    Text score;
    Text speed;
    static int earned;
    int mins;
    int secs;

    private const float gametime = 120.0f;

    void Awake()
    {
        //Check if there is an existing instance of this object
        if ((instance) && (instance.GetInstanceID() != GetInstanceID()))
            DestroyImmediate(gameObject); //Delete duplicate
        else
        {
            instance = this; //Make this object the only instance
            DontDestroyOnLoad(gameObject); //Set as do not destroy
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(time == 0)
            time = gametime;
        earned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            timer = GameObject.Find("Timer").GetComponent<Text>();
            score = GameObject.Find("Score").GetComponent<Text>();
            speed = GameObject.Find("Speed").GetComponent<Text>();
            
            time -= Time.deltaTime;
            UpdateLevelTimer(time);


            score.text = "Earned: " + earned.ToString();
            if(speed.gameObject)
            {
                speed.text = "Speed: " +Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().currentSpeed).ToString();
            }
        }
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            score = GameObject.Find("EndScore").GetComponent<Text>();
            score.text = earned.ToString();
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
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }

    public void Earned(DeliveryObjects.CollectibleType type)
    {
        switch (type)
        {
            case DeliveryObjects.CollectibleType.T1:
                earned += Random.Range(10, 20);
                break;

            case DeliveryObjects.CollectibleType.T2:
                earned += Random.Range(21, 50);
                break;

            case DeliveryObjects.CollectibleType.T3:
                earned += Random.Range(51, 100);
                break;
        }
    }

    public int Score
    {
        get
        {
            return earned;
        }
    }
    public void restart()
    {
        time += gametime;
        earned = 0;
    }
}

