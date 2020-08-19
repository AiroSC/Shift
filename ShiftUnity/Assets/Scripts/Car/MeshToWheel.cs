using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshToWheel : MonoBehaviour
{
    public WheelCollider wheelC;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion quat;
        Vector3 position;
        wheelC.GetWorldPose(out position, out quat);
        transform.position = position;
        transform.rotation = quat *= Quaternion.Euler(90, 0, 90);
    }
}
