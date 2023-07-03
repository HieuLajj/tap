using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;

    //int m,x,y,z;
    //int n;
    //int z;
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
        //z = 2; x = 4; y = 5;
        //for(int i=0; i<=y 5; i++)
        //{
        //    tangylenmoivonglap
        //    for(int j=0; j < x 4; j++)
        //    {
        //       tangxlen1.5moivonglap
        //        for(int m = 0; m< z 2; m++)
        //        {
        //            z = 0;
        //            z + 1.5;
        //        }
        //    }
        //}
    }
}
