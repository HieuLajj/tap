using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public float duration = 5f;


    public Vector3 startPosition;
    public Vector3 endPosition;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private Vector3 startScale;
    private Vector3 endScale;
    private float randomScale = 0;
    private Vector3 dir;
    private Vector3 dir2;


    //test
    public Vector3 testdir;
    public Renderer renderMaterial;
    private bool animationback = true;
    private void Awake()
    {
        prerotation = transform.rotation;
        endPosition = transform.position;
        renderMaterial.material.SetFloat("_FlipX", -1);
    }
    
    private void Update()
    {
        checkDirection();
        Debug.DrawRay(transform.position, dir * 1000, Color.yellow);
    }
    public void checkRay()
    {
        checkDirection();
        //if (Physics.Raycast(transform.position, -transform.forward, out hit, checkmax,1<<6))
        //{

        //}
        //else
        //{
        //    RunBlock();
        //}
        if (Physics.Raycast(transform.position, dir, out hit, checkmax, 1 << 6))
        {
            RunAwaitBack(dir);
        }
        else
        {
            RunBlock();
        }
    }

    public void RunAwaitBack(Vector3 diranimation)
    {
     
        if(animationback == true)
        {
            animationback = false;
            Vector3 originalPosition = transform.position;
            Vector3 targetPosition = originalPosition + diranimation * 0.2f;
            transform.DOMove(targetPosition, 0.2f).OnComplete(() =>
            {
                if (Physics.Raycast(transform.position, diranimation, out hit, checkmax, 1 << 6))
                {
                    Block blockdir = hit.collider.GetComponentInParent<Block>();
                    blockdir.RunAwaitBack(diranimation);
                }
                transform.DOMove(originalPosition, 0.2f).OnComplete(() =>
                {
                    animationback = true;
                    Debug.Log("ket thuc");
                });
            });
        }
        //if (Physics.Raycast(transform.position, diranimation, out hit, checkmax, 1 << 6))
        //{
        //    Block blockdir = hit.collider.GetComponentInParent<Block>();
        //    blockdir.RunAwaitBack(diranimation);
        //}  
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
            //transform.Translate(-Vector3.forward * 10 * Time.deltaTime);
            transform.Translate(dir2* 10 * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }
    public void MoveBlock()
    {
        StartCoroutine(MoveToDestination());
    }

   

    IEnumerator MoveToDestination()
    {
        float elapsedTime = 0f;

        ////rotation
        startRotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
        endRotation = transform.rotation;

        ////scale
        randomScale = Random.Range(0f, 1f);
        startScale = new Vector3(randomScale, randomScale, randomScale);
        endScale = new Vector3(1, 1, 1);
        while (elapsedTime < duration)
        {
           
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsedTime += Time.deltaTime; 
            yield return null;
        }

        transform.position = endPosition;
        transform.rotation = endRotation; 
        transform.localScale = endScale;
    }

    public void checkDirection()
    {
        switch (Direction)
        {
             case DirectionBlock.Left:
                dir = -transform.right;
                //dir2 = Vector3.left;
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
                testdir = Vector3.down;
                dir2 = Vector3.down;
                break;
            case 2:
                Direction = DirectionBlock.Up;
                testdir = Vector3.up;
                dir2 = Vector3.up;
                break;
            case 3:
                Direction = DirectionBlock.Forward;
                testdir = Vector3.forward;
                dir2 = Vector3.forward;
                break;
            case 4:
                Direction = DirectionBlock.Back;
                testdir = Vector3.back;
                dir2 = Vector3.back;
                break;
            case 5:
                Direction = DirectionBlock.Left;
                testdir = Vector3.left;
                dir2 = Vector3.left;
                break;
            case 6:
                Direction = DirectionBlock.Right;
                testdir = Vector3.right;
                dir2 = Vector3.right;
                break;
            default:
                Direction = DirectionBlock.Right;
                testdir = Vector3.right;
                dir2 = Vector3.right;
                break;
        }
    }
    public void Crack()
    {
        transform.position = transform.position + testdir * 5;
        startPosition = transform.position;
    }

}
