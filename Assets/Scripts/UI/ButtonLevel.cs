using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ButtonLevel : MonoBehaviour
{
  
    public TextMeshProUGUI textLevel;
    public Sprite[] spiteBg;
    private int level;
    public Image imageBG;
    public int Level
    {
        set { 
            level = value; 
            textLevel.text = level.ToString();
        }
        get {
            return level;
        }
    }

    private void OnEnable()
    {
        int flag = 0;
        if (Controller.Instance.DiffirentGame == DiffirentEnum.EASY)
        {
            flag = 0;
        }
        else if (Controller.Instance.DiffirentGame == DiffirentEnum.MEDIUM)
        {
            flag = Controller.Instance.constantsDiffical[DiffirentEnum.EASY];
        }
        else if (Controller.Instance.DiffirentGame == DiffirentEnum.HARD)
        {
            flag = Controller.Instance.constantsDiffical[DiffirentEnum.EASY] + Controller.Instance.constantsDiffical[DiffirentEnum.MEDIUM];
        }
        Level = transform.GetSiblingIndex() + 1 + flag;
        EditFromData();
    }

    public void LoadLevel()
    {
        LevelManager.Instance.LoadLevelInGame(level);
    }
    // Update is called once per frame

    public void EditFromData()
    {
        int flag = LevelManager.Instance.DataDiffical[Controller.Instance.DiffirentGame];
        if (Level < flag)
        {
            Actived();
        }
        else if (Level == flag){
            Activing();
        }
        else
        {
            AwaitActive();
        }
    }
    public void Actived()
    {
        imageBG.sprite = spiteBg[0];
    }
    public void Activing()
    {
        imageBG.sprite = spiteBg[1];
    }

    public void AwaitActive()
    {
        imageBG.sprite = spiteBg[2];
    }

}
