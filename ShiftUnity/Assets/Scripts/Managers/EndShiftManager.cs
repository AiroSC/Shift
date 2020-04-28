using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndShiftManager : MonoBehaviour
{
    [SerializeField]
    Text score;
    

    // Start is called before the first frame update
    void Start()
    {
        //score.text = GetComponent<GameManager>().Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
