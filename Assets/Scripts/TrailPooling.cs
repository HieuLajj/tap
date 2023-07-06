using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPooling : Singleton<TrailPooling>
{

    public GameObject GetTrail()
    {
        int count = transform.childCount;
        for(int i = 0; i < count; i++)
        {
            if (!transform.GetChild(i).gameObject.activeInHierarchy)
            {
                return transform.GetChild(i).gameObject;
            }
        }
        return null;
    }
}
