using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    public GameObject[] object1212;

    private void Start()
    {
        Tests();
    }
    private void Update()
    {
     
    }
    public void Tests()
    {
        for (int i = 0; i < object1212.Length; i++)
        {
            Block block = object1212[i].GetComponent<Block>();
            block.MoveBlock();
        }
    }
}
