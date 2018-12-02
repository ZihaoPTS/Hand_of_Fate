using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finger4Spread : MonoBehaviour
{

    // Use this for initialization
    public Vector3 Pivot = Vector3.down;
    public float xRotation;


    void Start()
    {
        xRotation = transform.localEulerAngles.x;
        Debug.Log("Original position (x) for finger 4 is ");
        Debug.Log(xRotation);
    }

    // Update is called once per frame
    void Finger4Spread(float newXRotation)
    {
        float valueToChange = newXRotation - xRotation;
        xRotation = newXRotation;
        transform.position += (transform.rotation * Pivot);
        transform.rotation *= Quaternion.AngleAxis(valueToChange, Vector3.right);
        transform.position -= (transform.rotation * Pivot);
    }
}
