using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffSpawn : MonoBehaviour
{
    public GameObject[] nodes;

    public Transform GetNodeTransform(int id)
    {
        return nodes[id].transform;
    }

    public void Activate(int id)
    {
        nodes[id].SetActive(true);
    }
    public void Deactivate(int id)
    {
        nodes[id].SetActive(false);
    }
}
