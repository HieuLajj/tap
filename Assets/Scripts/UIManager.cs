using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Touchpad touchpad;
    public GameObject parent;
    public GameObject parent2;
    public Transform CameraMain;
    private void Update()
    {
        parent.transform.Rotate(CameraMain.up, -touchpad.LookInput().x);
        parent2.transform.Rotate(CameraMain.right, touchpad.LookInput().y);
    }
}
