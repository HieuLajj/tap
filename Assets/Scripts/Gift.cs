using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public void ResetGift()
    {        
        UIManager.Instance.RewardManager.CountCoins();
        StartCoroutine(AwaitClear());
    }
    IEnumerator AwaitClear()
    {
        yield return new WaitForSeconds(3f);
        UIManager.Instance.GameUIIngame.ClearGift();
    }
}
