using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using UnityEngine.UIElements;
using DG.Tweening.Core.Easing;
using System.Linq;
using static Unity.Collections.AllocatorManager;

[System.Serializable]
public class LevelData
{
    public int LevelID;
    public Vector3 statusID;
    public int[] arrayDir;
}
public class LevelManager : Singleton<LevelManager>
{
    public Transform pretransform;
    public Transform temporarymain;
    public GameObject PrefabsObject;
    public static int LevelIDInt;
    public Vector3 statusLevel;

    private string[] linesLevel;
    private string[] numbers;
    private int[] arraydata;
    private int flag = 0;
    private int flag2 = 0;
    public int[] arrayDir;
    private int childCountParent;
    private void Start()
    {
        LoaddataFromLocal();
        //Edittext(linesLevel[LevelIDInt - 1]);
        //pretransform.localPosition = new Vector3(statusLevel.x / 2, (float)statusLevel.y / 2, (float)statusLevel.z / 2);
        //Camera.main.transform.position = new Vector3((float)statusLevel.x / 2, (float)statusLevel.y / 2, -10);
        // Debug.Log((float)arrayxyz[0]+"=="+(float)arrayxyz[2]+"=="+ (float)arrayxyz[1]);
        if (CheckSaveGame())
        {
            Edittext(linesLevel[LevelIDInt - 1]);
            pretransform.localPosition = new Vector3(statusLevel.x / 2, (float)statusLevel.y / 2, (float)statusLevel.z / 2);
            Camera.main.transform.position = new Vector3((float)statusLevel.x / 2, (float)statusLevel.y / 2, -10);
            CreateMapToSave();
        }
        else
        {
            CreateMap();
        }
    }
    public void LoadLevelInGame(int level)
    {
        //an tat ca cac object con tren man hinh
        SetAllFalse();
        Edittext(linesLevel[level - 1]);
        LevelIDInt = level;
        pretransform.localPosition = new Vector3(statusLevel.x / 2, (float)statusLevel.y / 2, (float)statusLevel.z / 2);
        Camera.main.transform.position = new Vector3((float)statusLevel.x / 2, (float)statusLevel.y / 2, -10);
        CreateMap();      
    }
    public void LoaddataFromLocal()
    {
        TextAsset mapText = Resources.Load("level-normal") as TextAsset;
        if (mapText != null)
        {     
            ProcessGameDataFromString(mapText.text);        
        }
    }
    public void ProcessGameDataFromString(string mapText)
    {

        linesLevel = mapText.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

    }

    private void Edittext(string inputString)
    {
        numbers = inputString.Split('|');
      
        arraydata = new int[numbers.Length - 3];

        //for (int i = 0; i < 3; i++)
        //{
        //    arrayxyz[i] = int.Parse(numbers[i]);
        //}
        statusLevel = new Vector3(int.Parse(numbers[1]), int.Parse(numbers[2]), int.Parse(numbers[0]));

        for (int i = 3; i < numbers.Length; i++)
        {
            arraydata[i - 3] = int.Parse(numbers[i]);
        }
    }

    public void CreateMap()
    {
        Controller.Instance.amountNumberBlock = 0;
        Controller.Instance.gameState = StateGame.AWAITLOAD;
        Controller.Instance.checkloadBlock = 0;
        pretransform.rotation = Quaternion.identity;
        flag = 0;
        childCountParent = pretransform.childCount;
        int m = 0;

        for (int i = 0; i < statusLevel.y; i++)
        {
            for (int j = 0; j < statusLevel.x; j++)
            {
                for (int g = 0; g < statusLevel.z; g++)
                {
                    if (arraydata[flag] != -1)
                    {
                        GameObject block;
                        if (m <= childCountParent - 1)
                        {
                           
                            block = pretransform.GetChild(m).gameObject;
                            block.transform.position = new Vector3(j + 0.5f, i + 0.5f, g + 0.5f);
                            block.transform.rotation = Quaternion.identity;
                            m++;
                            block.SetActive(true);
                            
                        }
                        else
                        {
                            block = Instantiate(PrefabsObject, new Vector3(j + 0.5f, i + 0.5f, g + 0.5f), Quaternion.identity, pretransform);                    
                        }
                        Block blockscript = block.GetComponent<Block>();
                        blockscript.GetDirectionBlock(arraydata[flag]);
                        blockscript.Crack();
                        blockscript.MoveBlock();
                        blockscript.statusBlock = true;
                        AddListenerBlock(blockscript);

                        Controller.Instance.amountNumberBlock++;
                    }
                    
                    flag++;
                }
            }

        }
    }

    public void CreateMapToSave()
    {
        //Debug.Log(statusLevel.x+"load truong hoop 2" + statusLevel.y);
        flag = 0;
        flag2 = 0;
        Controller.Instance.amountNumberBlock = 0;
        Controller.Instance.checkloadBlock = 0;
        Controller.Instance.gameState = StateGame.AWAITLOAD;

        for (int i = 0; i < statusLevel.y; i++)
        {
            for (int j = 0; j < statusLevel.x; j++)
            {
                for (int g = 0; g < statusLevel.z; g++)
                {
                    if (arraydata[flag] != -1)
                    {
                        GameObject block = Instantiate(PrefabsObject, new Vector3(j + 0.5f, i + 0.5f, g + 0.5f), Quaternion.identity, pretransform);
                        Block blockscript = block.GetComponent<Block>();
                        if (arrayDir[flag2] != -1)
                        {
                           
                            blockscript.GetDirectionBlock(arrayDir[flag2]);
                            blockscript.Crack();
                            blockscript.MoveBlock();
                            Controller.Instance.amountNumberBlock ++;
                        }
                        else
                        {
                            
                            blockscript.GetDirectionBlock(arraydata[flag]);
                            blockscript.Crack();
                            blockscript.MoveBlock();
                            block.SetActive(false);
                        }
                        AddListenerBlock(blockscript);
                        flag2++;
                    }
                    flag++;
                }
            }

        }
    }
    private void AddListenerBlock(Block block)
    {
        if (!Controller.Instance.ListenerBlock.Contains(block))
        {
            Controller.Instance.ListenerBlock.Add(block);
        }
    }

    public void SaveGame(int[] arraydir)
    {
        LevelData levelData = new LevelData();
        levelData.LevelID = LevelIDInt;
        levelData.statusID = statusLevel;
        levelData.arrayDir = arraydir;
        if (levelData.arrayDir.Length > 0)
        {
            string json = JsonUtility.ToJson(levelData);
            string file = Application.dataPath + "/Data/level.json";
            File.WriteAllText(file, json);
            //Debug.Log(file);
        }
        //Debug.Log("??"+ arraydir.Length);
    }

    public bool CheckSaveGame()
    {
        string json = File.ReadAllText(Application.dataPath + "/Data/level.json");
        if (json == "" || json == null)
        {
            Debug.Log("ko co giu lieu");
            return false;
        }
        else
        {
            LevelData leveldata = JsonUtility.FromJson<LevelData>(json);
            LevelIDInt = leveldata.LevelID;
            statusLevel = leveldata.statusID;
            arrayDir = leveldata.arrayDir;
            return true;
        }
    }

    public void SetAllFalse()
    {
        //for(int i = 0; i < temporarymain.childCount; i++) {
        //    temporarymain.GetChild(i).parent = pretransform;
        //    temporarymain.GetChild(i).gameObject.SetActive(false);
        //    Debug.Log(i);
        //}
        //foreach (Transform child in temporarymain)
        //{
        //    child.gameObject.SetActive(false);
        //    child.parent = pretransform;
        //}
        List<Transform> childObjects = new List<Transform>();

        foreach (Transform child in temporarymain)
        {
            childObjects.Add(child);
        }
        foreach (Transform child in childObjects)
        {
            child.SetParent(pretransform);
        }
        foreach (Transform child in pretransform)
        {
            child.gameObject.SetActive(false);
        }
    }
    
}
