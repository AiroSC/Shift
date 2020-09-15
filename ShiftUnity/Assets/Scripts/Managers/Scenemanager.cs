using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenemanager : MonoBehaviour
{
    // Start is called before the first frame update

    GameManager gm;
    void Start()
    {
        gm = GameObject.Find("gameManager").GetComponent<GameManager>();
    }
   
    public void Restart()
    {
        gm.restart();
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
