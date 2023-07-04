using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    public Vector3 targetPosition;
    public float moveDuration = 0.2f;

    void Start()
    {
        Vector3 originalPosition = transform.position;
        targetPosition = originalPosition + transform.up * 1;
        transform.DOMove(targetPosition, moveDuration).OnComplete(() =>
        {
            transform.DOMove(originalPosition, moveDuration);
        });
        Debug.Log("DFdfds"+ targetPosition);
    }
}
