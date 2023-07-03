using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class Test : MonoBehaviour
{
    public GameObject PrefabsObject;
    public Transform pretransform;
    public GameObject[] object1212;
    public int[] arraydata;

    private void Start()
    {
        CreateMap();
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

    public void CreateMap()
    {
        int flag = 0;
        int flagx = 0;
        for(int i=0; i < 2; i++)
        {
            while (arraydata[flag]==-1)
            {
                flagx++;
                flag++;
            }
            GameObject gameei = Instantiate(PrefabsObject, new Vector3(flagx*1,0,0+i*1f), Quaternion.identity, pretransform);
            flag++;
        }
    }
}
