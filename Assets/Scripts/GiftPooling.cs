using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPooling : Singleton<GiftPooling>
{
    public GameObject PrefabsGift;
    public GameObject GetGift()
    {
        for(int i=0; i< transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                return transform.GetChild(i).gameObject;
            }
        }
        return Instantiate(PrefabsGift);
    }
}
