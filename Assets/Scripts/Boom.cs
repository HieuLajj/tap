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
    private Coroutine BoomCoroutine;
    public ParticleSystem ExplosionPS;
    public GameObject[] ModelBoom;
    private bool hasExploded = false;
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
                        BoomCoroutine = StartCoroutine(IMoveBoom(target));
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
        ActiveModel();
        AnimatorBoom.speed = 1;
        // khi active thi khong co tuong tac xoay check them o ondisable
        Controller.Instance.gameState = StateGame.AWAIT;
        transform.position = QuadraticCurve.Instance.PreCameraPosition1.transform.position;
        stateBoom = StateBoom.AWAIT;
        transform.eulerAngles = new Vector3(0, 270, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && stateBoom == StateBoom.AWAIT)
        {
           BoomState = StateBoom.ACTIVE;
        }
        if(Input.GetMouseButtonUp(0) && stateBoom != StateBoom.FLY)
        {
            BoomState = StateBoom.FLY;
        }
        if(stateBoom == StateBoom.ACTIVE)
        {
            MoveBoom();
        }

    }

    //private void OnDisable()
    //{


    //    Controller.Instance.gameState = StateGame.PLAY;
    //}
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
        //gameObject.SetActive(false);
    }

    public void CheckBoomed()
    {
        ActiveEffect();
        if (hasExploded)
        {
            return;
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, BoomRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Block"))
            {
                hitCollider.gameObject.GetComponent<Block>().StatusBlock = StatusBlock.Die;
                hitCollider.gameObject.GetComponent<Block>().FlyBoomed();
            }
        }
        hasExploded = true;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BoomRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasExploded)
        {
            return;
        }
        if (other.gameObject.CompareTag("Block"))
        {
            if (BoomCoroutine != null)
            {
                StopCoroutine(BoomCoroutine);
            }
            CheckBoomed();
            //gameObject.SetActive(false);
        }
    }

    public void ActiveEffect()
    {
        hasExploded = false;
        DisActiveModel();
        if (!ExplosionPS.isPlaying)
        {
            ExplosionPS.Play();
        }
    }

    public void DisActiveModel()
    {
        for(int i=0; i<ModelBoom.Length; i++)
        {
            ModelBoom[i].SetActive(false);
        }
        Controller.Instance.gameState = StateGame.PLAY;
        Invoke("DisActiveGame", 2f);
        
    }
    public void DisActiveGame()
    {
        gameObject.SetActive(false);
    }
    public void ActiveModel()
    {
        for (int i = 0; i < ModelBoom.Length; i++)
        {
            ModelBoom[i].SetActive(true);
        }
    }
}
