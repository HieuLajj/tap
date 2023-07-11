using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLevelManager : MonoBehaviour
{
    // Start is called before the first frame update
  
    // Start is called before the first frame update
    public int easy = 10;
    public int medium = 20;
    public int mediurm = 30;
    public GameObject BtnLevelPrefabs;
    public Transform ParentBtnLevel;


    public void ActiveLoadLevel(int level)
    {
        if (level==0)
        {
            Controller.Instance.DiffirentGame = DiffirentEnum.EASY;
        }else if(level == 1)
        {
            Controller.Instance.DiffirentGame = DiffirentEnum.MEDIUM;
        }
        else if(level == 2)
        {
            Controller.Instance.DiffirentGame = DiffirentEnum.HARD;
        }
        DisActveBtn();
        Spawn(Controller.Instance.constantsDiffical[Controller.Instance.DiffirentGame]);
    }

    public void DisActveBtn()
    {
        foreach (Transform child in ParentBtnLevel)
        {
            child.gameObject.SetActive(false);
        }
    }
    public void Spawn(int count)
    {
        int flag = 0;
        int childCount = ParentBtnLevel.childCount;
        int index = count - childCount;
        if (index <= 0)
        {
            flag = count;
            for (int i = 0; i < flag; i++)
            {
                ParentBtnLevel.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            flag = index;
            for (int i = 0; i < childCount; i++)
            {
                ParentBtnLevel.GetChild(i).gameObject.SetActive(true);
            }
            for(int i = 0; i < flag; i++)
            {
                Instantiate(BtnLevelPrefabs, ParentBtnLevel);
            }
        }
       
    }
}
