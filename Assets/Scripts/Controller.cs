using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public float timer = 0f;
    public UIManager manager;
    //int m,x,y,z;
    //int n;
    //int z;
    // Update is called once per frame
    private void Start()
    {
        timer = 0;
    }
    void Update()
    {

        if(Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (timer > 0.15f)
            {
                manager.Ok();
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if (timer <= 0.15f)
            {
                screenPosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(screenPosition);

                if (Physics.Raycast(ray, out RaycastHit hitData, Mathf.Infinity, 1 << 6))
                {

                    Block block = hitData.collider.GetComponentInParent<Block>();
                    block.checkRay();

                }
            }
            timer = 0;
        }
      
    }
}
