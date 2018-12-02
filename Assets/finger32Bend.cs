using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finger32Bend : MonoBehaviour
{

    // Use this for initialization
    public Vector3 Pivot = Vector3.down;
    public float zRotation = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Finger32Bend(float bending)
    {
        float valueToChange = bending - zRotation;
        zRotation = bending;
        transform.position += (transform.rotation * Pivot);
        transform.rotation *= Quaternion.AngleAxis(-1 * valueToChange, Vector3.forward);
        transform.position -= (transform.rotation * Pivot);
    }
}
