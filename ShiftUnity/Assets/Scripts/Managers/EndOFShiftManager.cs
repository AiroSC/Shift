using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOFShiftManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Earned;
    public TextMeshProUGUI Tips;
    public TextMeshProUGUI Fuel_Cost;
    public TextMeshProUGUI Repair_Cost;

    GameManager gm;
    void Start()
    {
        gm = GameObject.Find("gameManager").GetComponent<GameManager>();
        Earned.text = "Earned: $"+ gm.EARNED.ToString();
        Tips.text = "Tips: $" + gm.TIPPED.ToString();
        Fuel_Cost.text = "Fuel Cost: $" + gm.FUEL.ToString();
        Repair_Cost.text = "Damage Cost: $" + gm.DAMAGE.ToString();
    }

    public void Restart()
    {
        gm.restart();
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
