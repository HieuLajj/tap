using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEngine.UI.Image;

public class Test : MonoBehaviour
{
    public GameObject PrefabsObject;
    public Transform pretransform;
    public GameObject[] object1212;
    public int[] arraydata;
    int flag = 0;
    public int[] arrayxyz;
    private string[] numbers;
    public string[] lines;
    private void Start()
    {
        LoaddataFromLocal();
        Edittext(lines[10]);
        pretransform.localPosition = new Vector3(2, 2.5f, 1);
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
        flag = 0;
        for(int i=0; i < arrayxyz[2]; i++)
        {
            for(int j=0; j< arrayxyz[1]; j++)
            {
                for(int g=0; g< arrayxyz[0]; g++)
                {
                    if (arraydata[flag] != -1)
                    {
                        GameObject block = Instantiate(PrefabsObject, new Vector3(j+0.5f, i+0.5f, g+0.5f), Quaternion.identity, pretransform);
                        Block blockscript = block.GetComponent<Block>();
                        blockscript.GetDirectionBlock(arraydata[flag]);                        
                        blockscript.Crack();
                        blockscript.MoveBlock();

                    }
                    flag++;
                }
            }
            
        }
    }

    private void Edittext( string inputString)
    {
        numbers = inputString.Split('|');
        arrayxyz = new int[3];
        arraydata = new int[numbers.Length - 3];

        for (int i = 0; i < 3; i++)
        {
            arrayxyz[i] = int.Parse(numbers[i]);
        }

        for (int i = 3; i < numbers.Length; i++)
        {
            arraydata[i - 3] = int.Parse(numbers[i]);
        }
    }


    public void LoaddataFromLocal()
    {
        TextAsset mapText = Resources.Load("level-normal") as TextAsset;
        if (mapText != null)
        {
            {
                ProcessGameDataFromString(mapText.text);
            }

        }
    }
    public void ProcessGameDataFromString(string mapText)
    {

        lines = mapText.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

    }  
}
