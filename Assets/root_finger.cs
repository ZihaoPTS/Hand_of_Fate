using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;


//tribution to tutorial and code used
//http://aarlangdi.blogspot.com/2016/04/unity-tutorial-rotate-object-with-mouse.html
//https://aarlangdi.blogspot.com/2016/07/rotate-object-with-another-object-in.html
//https://www.youtube.com/watch?v=Z9O0u0EmtiI&feature=youtu.be

public class root_finger : MonoBehaviour {

    /*float rotSpeed = 20;
    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        transform.RotateAround(Vector3.up, -rotX);
        transform.RotateAround(Vector3.right, rotY);
        //transform.Rotate(Vector3.down, rotY);
    }
    */
    public Vector3 Pivot = Vector3.down;
   
    //it could be a Vector3 but its more user friendly
    public bool RotateX = true;
    //public bool RotateY = false;
    public bool RotateZ = true;

    public Vector3 originalPosition;

    float rotSpeed = 20;

    //public float Mouse_X = 0;
    public const float Max_rotation_X = 1;

    void Start()
    {
        originalPosition = transform.position;
    }

    //void Update()
    void OnMouseDrag()
    { 
    
        
        transform.position += (transform.rotation*Pivot);

        float Mouse_X = Input.GetAxis("Mouse X");

        float Mouse_Y = Input.GetAxis("Mouse Y");

        //The nuckle will update the amount of difference
        if (RotateX)
            transform.rotation *= Quaternion.AngleAxis(rotSpeed * Mouse_X, Vector3.right);
        //if (RotateY)
        //    transform.rotation *= Quaternion.AngleAxis(rotSpeed * Input.GetAxis("Mouse Y"), Vector3.up);
        //Y is not matter, Y axel rotation is "circle the cylinder"
        if (RotateZ)
            transform.rotation *= Quaternion.AngleAxis(-1 * rotSpeed * Mouse_Y, Vector3.forward);
        transform.position -= (transform.rotation*Pivot);

        /*if(transform.position.x >= )
        {

        }*/

    }

}
