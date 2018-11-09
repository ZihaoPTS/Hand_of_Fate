using System.Collections;
using System.Collections.Generic;
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
    public bool RotateX = false;
    public bool RotateY = false;
    public bool RotateZ = true;

    float rotSpeed = 20;

    void OnMouseDrag()
    {
        transform.position += (transform.rotation*Pivot);
       
        if (RotateX)
            transform.rotation *= Quaternion.AngleAxis(rotSpeed * Input.GetAxis("Mouse X"), Vector3.right);
        if (RotateY)
            transform.rotation *= Quaternion.AngleAxis(rotSpeed * Input.GetAxis("Mouse Y"), Vector3.up);
        if (RotateZ)
            transform.rotation *= Quaternion.AngleAxis(-1 * rotSpeed * Input.GetAxis("Mouse Y"), Vector3.forward);
 
        transform.position -= (transform.rotation*Pivot);

    }
    
}
