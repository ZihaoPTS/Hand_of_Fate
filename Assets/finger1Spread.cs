using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finger1Spread : MonoBehaviour
{

    // Use this for initialization
    public Vector3 Pivot = Vector3.down;
    public float xRotation;

    void Start()
    {
        xRotation = transform.localEulerAngles.x - 360;
        //Debug.Log("Original position (x) for finger 1 is " + (char)(xRotation));
    }

    // Update is called once per frame
    void Finger1Spread(float newXRotation)
    {
        float valueToChange = newXRotation - xRotation;
        xRotation = newXRotation;
        transform.position += (transform.rotation * Pivot);
        transform.rotation *= Quaternion.AngleAxis(valueToChange, Vector3.right);
        transform.position -= (transform.rotation * Pivot);
    }
}
