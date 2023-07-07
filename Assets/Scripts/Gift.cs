using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public void ResetGift()
    {
        UIManager.Instance.GameUIIngame.ClearGift();
    }
}
