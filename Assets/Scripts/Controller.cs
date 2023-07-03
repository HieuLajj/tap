using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
   

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            screenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hitData, Mathf.Infinity, 1 << 6))
            {
               
                Block block = hitData.collider.GetComponent<Block>();
                block.checkRay();

            }
        }
    }
}
