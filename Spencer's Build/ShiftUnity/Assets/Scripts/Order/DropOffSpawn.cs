using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffSpawn : MonoBehaviour
{
    public Transform GetNodeTransform(int id)
    {
        return transform.GetChild(id);
    }
    public int GetLength()
    {
        return transform.childCount;
    }
    public void Activate(int id)
    {
        transform.GetChild(id).gameObject.SetActive(true);
    }
    public void Deactivate(int id)
    {
        transform.GetChild(id).gameObject.SetActive(false);
    }
}
