using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateBoom
{
    AWAIT,
    ACTIVE,
    FLY
}
public class Boom : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;

    private StateBoom stateBoom = StateBoom.AWAIT;
    public float BoomRadius;
    public float SpeedBoom = 20;
    public Animator AnimatorBoom;
    public StateBoom BoomState
    {
        get
        {
            return stateBoom;
        }
        set
        {
            stateBoom = value;
            if(stateBoom == StateBoom.FLY)
            {
                AnimatorBoom.speed = 1f;
                if (gameObject.activeInHierarchy){
                    GameObject target = LevelManager.Instance.GetRandomActiveChild();
                    if(target != null)
                    {
                        StartCoroutine(IMoveBoom(target));
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
            }else if(stateBoom == StateBoom.ACTIVE)
            {
                AnimatorBoom.speed = 0.6f;
                if (Controller.Instance.gameState != StateGame.AWAIT)
                {
                    Controller.Instance.gameState = StateGame.AWAIT;
                }
            }
        }
    }

    private void OnEnable()
    {
        AnimatorBoom.speed = 1;
        // khi active thi khong co tuong tac xoay check them o ondisable
        Controller.Instance.gameState = StateGame.AWAIT;
        transform.position = QuadraticCurve.Instance.PreCameraPosition.transform.position;
        stateBoom = StateBoom.AWAIT;
        transform.eulerAngles = new Vector3(0, 270, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
           BoomState = StateBoom.ACTIVE;
        }
        if(Input.GetMouseButtonUp(0))
        {
            BoomState = StateBoom.FLY;
        }
        if(stateBoom == StateBoom.ACTIVE)
        {
            MoveBoom();
        }

    }

    private void OnDisable()
    {
        Controller.Instance.gameState = StateGame.PLAY;
    }
    private void MoveBoom()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 2.5f;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = Vector3.Lerp(transform.position, worldPosition, 20* Time.deltaTime);
    }

    public IEnumerator IMoveBoom(GameObject target)
    {
        QuadraticCurve.Instance.A.position = transform.position;
        QuadraticCurve.Instance.B.position = target.transform.position;
       
        float sampleTime = 0f;
        transform.position = QuadraticCurve.Instance.evaluate(sampleTime);
        transform.DORotate(new Vector3(360, 0, 0), 1f, RotateMode.FastBeyond360);
      
        while (sampleTime <= 1f)
        {
            sampleTime += Time.deltaTime;
            transform.position = QuadraticCurve.Instance.evaluate(sampleTime);
            yield return null;
        }
        transform.position = QuadraticCurve.Instance.evaluate(1);
        CheckBoomed();
        gameObject.SetActive(false);
    }

    public void CheckBoomed()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, BoomRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Block"))
            {
                hitCollider.gameObject.GetComponent<Block>().FlyBoomed();
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BoomRadius);
    }
}
