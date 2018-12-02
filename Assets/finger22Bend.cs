using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finger22Bend : MonoBehaviour
{

    // Use this for initialization
    public Vector3 Pivot = Vector3.down;
    public int spreadingIndex = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Finger22Bend(int bending)
    {
        transform.position += (transform.rotation * Pivot);
        transform.rotation *= Quaternion.AngleAxis(-1 * bending, Vector3.forward);
        transform.position -= (transform.rotation * Pivot);
    }
}
