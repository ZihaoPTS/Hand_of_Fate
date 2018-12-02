using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finger1Bend : MonoBehaviour
{

    // Use this for initialization
    public Vector3 Pivot = Vector3.down;

    void Start()
    {

    }

    // Update is called once per frame
    void Finger1Bend(float bending)
    {
        transform.position += (transform.rotation * Pivot);
        transform.rotation *= Quaternion.AngleAxis(-1 * bending, Vector3.forward);
        transform.position -= (transform.rotation * Pivot);
    }
}
