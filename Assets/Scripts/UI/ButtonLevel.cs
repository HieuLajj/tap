using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ButtonLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textLevel;
    private int level;
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
  
    void Start()
    {
        Level = transform.GetSiblingIndex()+1;
    }

    public void LoadLevel()
    {
        LevelManager.Instance.LoadLevelInGame(level);
    }
    // Update is called once per frame
   
}
