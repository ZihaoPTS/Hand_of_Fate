using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snd_finger : MonoBehaviour {

    // Use this for initialization
    public GameObject obj;

    void Update()
    {
        transform.rotation = obj.transform.rotation;
    }
}
