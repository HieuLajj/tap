using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public enum DirectionBlock
{
    Down,
    Up,
    Forward,
    Back,
    Left,
    Right,
}
public class Block : MonoBehaviour
{
    public DirectionBlock Direction;
    public Vector3 preposition;
    public Quaternion prerotation;
    RaycastHit hit;
    public float checkmax;
    public float duration = 2.5f;


    public Vector3 startPosition;
    public Vector3 endPosition;
    private Quaternion startRotation;
    private Vector3 startScale;  
    private float randomScale = 0;
    private Vector3 dir;
    private Vector3 dir2;

    private bool animationback = true;

    public GameObject ModelBlock;
    private void Awake()
    {
        prerotation = transform.rotation;
        endPosition = transform.position;
    }
    
    public void checkRay()
    {
        checkDirection();
        if (Physics.Raycast(transform.position, dir, out hit, checkmax, 1 << 6))
        {
            if (hit.distance < 1)
            {
                RunAwaitBack(dir, dir2);
            }
            else
            {
                RunAwait2(hit.collider.GetComponentInParent<Block>());
            }
        }
        else
        {
            RunBlock();
        }
    }

    public void RunAwaitBack(Vector3 dircheck, Vector3 animationdir)
    {
     
        if(animationback == true)
        {
            animationback = false;
            Vector3 originalPosition = transform.localPosition;
            Vector3 targetPosition = originalPosition + animationdir * 0.2f;
            transform.DOLocalMove(targetPosition, 0.2f).OnComplete(() =>
            {
      
                if (Physics.Raycast(transform.position, dircheck, out hit, checkmax, 1 << 6))
                {
                    Block blockdir = hit.collider.GetComponentInParent<Block>();
                    if(hit.distance < 1)
                    {
                        blockdir.RunAwaitBack(dircheck, animationdir);
                    }
                }         
                transform.DOLocalMove(originalPosition, 0.2f).OnComplete(() =>
                {
                    animationback = true;
                });
            });
        }
    }

    public void RunAwait2(Block b)
    {
        //Debug.Log(b.name+"chay thang 2 ne"+ dir2);
        Vector3 originalPosition = transform.localPosition;

        Vector3 targetPosition = b.transform.localPosition - dir2;
        transform.DOLocalMove(targetPosition, 0.5f).OnComplete(() =>
        {
            transform.DOLocalMove(originalPosition, 0.5f);
        });
    }


    public void RunBlock()
    {
        checkDirection();
        StartCoroutine(Run());
    }
    IEnumerator Run()
    {
        
        for (float alpha = 20f; alpha >= 0; alpha -= 0.1f)
        {
            transform.Translate(dir2* 10 * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }
    public void MoveBlock()
    {
        MoveToDestination();
    }
    public void MoveToDestination()
    {
        startRotation = transform.rotation;
        startScale = transform.localScale;
        randomScale = Random.Range(0.5f, 2f);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        transform.DOMove(endPosition, 2f).SetEase(Ease.OutQuad);
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360).OnComplete(() =>
        {
            transform.rotation = startRotation;
        });
        transform.DOScale(startScale, 2f).OnComplete(() =>
        {
            transform.localScale = startScale;
        });
    }

    //IEnumerator MoveToDestination()
    //{
    //    float elapsedTime = 0f;

    //    ////rotation
    //    startRotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
    //    endRotation = transform.rotation;

    //    ////scale
    //    randomScale = Random.Range(0f, 1f);
    //    startScale = new Vector3(randomScale, randomScale, randomScale);
    //    endScale = new Vector3(1, 1, 1);
    //    while (elapsedTime < duration)
    //    {
    //        float t = elapsedTime / duration;
    //        transform.position = Vector3.Lerp(startPosition, endPosition, t);
    //        transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
    //        transform.localScale = Vector3.Lerp(startScale, endScale, t);
    //        elapsedTime += Time.deltaTime; 
    //        yield return null;
    //    }

    //    transform.position = endPosition;
    //    transform.rotation = endRotation; 
    //    transform.localScale = endScale;
    //}

    public void checkDirection()
    {
        switch (Direction)
        {
             case DirectionBlock.Left:
                dir = -transform.right;
                break;
             case DirectionBlock.Right:
                dir = transform.right;
          
                break;
             case DirectionBlock.Up:
                dir = transform.up;
               
                break;
             case DirectionBlock.Down:
                dir = -transform.up;
               
                break;
             case DirectionBlock.Forward:
                dir = transform.forward;
                
                break;
             case DirectionBlock.Back:
                dir = -transform.forward;
               
                break;
            default:
                dir = -transform.forward;
                break;
        }
    }
    public void GetDirectionBlock(int input)
    {
        switch (input)
        {
            case 1:
                Direction =  DirectionBlock.Down;
                dir2 = Vector3.down;
                ModelBlock.transform.eulerAngles = new Vector3(90, 0, 0);
                break;
            case 2:
                Direction = DirectionBlock.Up;
                dir2 = Vector3.up;
                ModelBlock.transform.eulerAngles = new Vector3(-90, 0, 0);
                break;
            case 3:
                Direction = DirectionBlock.Back;       
                dir2 = Vector3.back;
                ModelBlock.transform.eulerAngles = new Vector3(180, 0, 0);
                break;
            case 4:
                Direction = DirectionBlock.Forward;                
                dir2 = Vector3.forward;
                break;
            case 5:
                Direction = DirectionBlock.Left;              
                dir2 = Vector3.left;
                ModelBlock.transform.eulerAngles = new Vector3(0, -90, 0);
                break;
            case 6:
                Direction = DirectionBlock.Right;
                dir2 = Vector3.right;
                ModelBlock.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            default:
                Direction = DirectionBlock.Right;          
                dir2 = Vector3.right;
                break;
        }
    }
    public void Crack()
    {
        Vector3 direction = (transform.position - transform.parent.position).normalized;
        Vector3 targetPosition = transform.position + direction * 5;
        transform.position = targetPosition;
        startPosition = transform.position;
    }

}
