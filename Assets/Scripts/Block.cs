using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public enum DirectionBlock
{
    Up,
    Down,
    Left,
    Right,
    Forward,
    Back
}
public class Block : MonoBehaviour
{
    public DirectionBlock Direction;
    public Vector3 preposition;
    public Quaternion prerotation;
    RaycastHit hit;
    public float checkmax;
    public float duration = 5f;


    private Vector3 startPosition;
    private Vector3 endPosition;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private Vector3 startScale;
    private Vector3 endScale;
    private float randomScale = 0;
    private Vector3 dir;
    private Vector3 dir2;
    private void Awake()
    {
        prerotation = transform.rotation;
        startPosition = transform.position;
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

        }
        else
        {
            RunBlock();
        }
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
            transform.Translate(dir2 * 10 * Time.deltaTime);
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

        //position
        startPosition = transform.position; 
        endPosition = preposition;

        //rotation
        startRotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
        endRotation = transform.rotation;

        //scale
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
        Debug.Log(endPosition+"fff"+transform.position);
        transform.rotation = endRotation; 
        transform.localScale = endScale;
    }

    public void checkDirection()
    {
        switch (Direction)
        {
             case DirectionBlock.Left:
                dir = -transform.right;
                dir2 = Vector3.left;
                break;
             case DirectionBlock.Right:
                dir = transform.right;
                dir2 = Vector3.right;
                break;
             case DirectionBlock.Up:
                dir = transform.up;
                dir2 = Vector3.up;
                break;
             case DirectionBlock.Down:
                dir = -transform.up;
                dir2 = Vector3.down;
                break;
             case DirectionBlock.Forward:
                dir = transform.forward;
                dir2 = Vector3.forward;
                break;
             case DirectionBlock.Back:
                dir = -transform.forward;
                dir2 = Vector3.back;
                break;
            default:
                dir = -transform.forward;
                dir2 = Vector3.back;
                break;
        }
    }
}
