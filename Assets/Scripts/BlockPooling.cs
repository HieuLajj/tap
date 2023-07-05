using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPooling : MonoBehaviour
{
    public List<GameObject> PoolBlock;
    private void Awake()
    {
        PoolBlock = new List<GameObject>();
    }
    public GameObject GetBullingBlock()
    {
        for (int i = 0; i < PoolBlock.Count; i++)
        {
            if (!PoolBlock[i].activeInHierarchy)
            {
                return PoolBlock[i];
            }
        }
        return null;
    }
}
