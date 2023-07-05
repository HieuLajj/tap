using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LevelPanel;
    public void CheckActive()
    {
        if (LevelPanel.activeInHierarchy)
        {
            LevelPanel.SetActive(false);
        }
        else
        {
            LevelPanel.SetActive(true);
        }
    }
}
