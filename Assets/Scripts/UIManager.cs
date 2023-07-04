using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Touchpad touchpad;
    public GameObject parent;
    
    public Transform CameraMain;
    private float currentVelocity;

    private void Update()
    {
    }

    public void Ok()
    {
        parent.transform.Rotate(Vector3.up, -touchpad.LookInput().x, Space.World);
        parent.transform.Rotate(Vector3.right, touchpad.LookInput().y, Space.World);
        //Quaternion targetRotation = Quaternion.Euler(0, -touchpad.LookInput().x, 0);
        //float smoothTime = 0.3f; 
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);
        //float targetangle = -touchpad.lookinput().x;
        //float smoothangle = mathf.smoothdampangle(parent.transform.eulerangles.y, targetangle, ref currentvelocity, 0.3f);
        //parent.transform.rotation = quaternion.euler(0, smoothangle, 0);
    }
}
