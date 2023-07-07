using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : Singleton<Test3>
{
    public GameObject PrefabsGift;
    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Debug.Log("Space key was pressed.");
            GameObject g = LevelManager.Instance.GetRandomGift();
            //GameObject t = Instantiate(PrefabsGift, g.transform.position, g.transform.rotation, g.transform);
            //t.transform.localScale = new Vector3(0,0,0);
            //t.transform.DOScale(0.5f, 1).OnComplete(() =>
            //{
            //    t.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //});
            if (g != null)
            {
                Block block = g.GetComponent<Block>();
                block.StatusBlock = StatusBlock.Gift;
            }
           // block.ModelBlock.SetActive(false);
        }
    }
}
