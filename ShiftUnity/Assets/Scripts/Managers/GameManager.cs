using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    float time;
    [SerializeField]
    Text timer;
    [SerializeField]
    Text score;
    [SerializeField]
    Text speed;
    int earned;
    int mins;
    int secs;

    // Start is called before the first frame update
    void Start()
    {
        if(time == 0)
            time = 600f;
        earned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        UpdateLevelTimer(time);

        if (time < 0)
        {
            EndShift();
        }

        score.text = earned.ToString();
        speed.text = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().currentSpeed).ToString();

    }

    public void EndShift()
    {
        Debug.Log("Shift End");
        //gameover screen
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
        if(seconds <= 0)
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
}

