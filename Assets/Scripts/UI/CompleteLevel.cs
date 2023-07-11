using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public GameObject BtnContinue;
    public void NextLevelWhenBtn()
    {
        gameObject.SetActive(false);
        LevelManager.Instance.NextLevel();
    }
    private void OnEnable()
    {
        UIManager.Instance.GameUIIngame.CoinsUI.SetActive(true);
    }

    private void OnDisable()
    {
        BtnContinue.SetActive(false);  
        UIManager.Instance.GameUIIngame.CoinsUI.SetActive(false);
    }

    public void DisplayButtonCotinue()
    {
        Invoke("ActiveBtC", 3);
    }
   

    public void ActiveBtC()
    {
        BtnContinue?.SetActive(true);
    }
}
