using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum StateGame
{
    AWAIT,
    AWAITLOAD,
    PLAY,
    WIN
}
public class Controller : Singleton<Controller>
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    private float timer = 0f;
    public UIManager manager;


    public List<IListenerBlock> ListenerBlock = new List<IListenerBlock>();
    public int amountNumberBlock = 0;
    public int checkloadBlock = 0;
    private StateGame stateGame = StateGame.AWAIT;


    //test
    public GameObject WinEffect;
    public StateGame gameState
    {
        get
        {
            return stateGame;
        }
        set
        {
            stateGame = value;
            if(value == StateGame.WIN)
            {
                WhenWin();
            }
        }
    }

    private void Start()
    {
       // timer = 0;
    }
    private void Update()
    {
        //if(stateGame == StateGame.AWAITLOAD) {
        //    awaitload();
        //}
    }

    private void LateUpdate()
    {
        if(stateGame == StateGame.PLAY) {
            userInteraction();
        }
        //userInteraction();
    }

    public void Checkawaitload()
    {
        checkloadBlock++;
        if(checkloadBlock >= amountNumberBlock) {
            stateGame = StateGame.PLAY;
        }
    }

    private void OnApplicationQuit()
    {
        int i = 0;
        int[] arraytest = new int[ListenerBlock.Count];
        foreach(var item in ListenerBlock)
        {
            arraytest[i] = item.IType();
            i++;
        }
        //for(int j=0; j<arraytest.Length; j++)
        //{
        //    Debug.Log(arraytest[j]);
        //}
        LevelManager.Instance.SaveGame(arraytest);  
    }

    
    public void userInteraction()
    {
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (timer > 0.15f)
            {
                manager.SwipeScreen();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (timer <= 0.15f)
            {
                
                    screenPosition = Input.mousePosition;
                    Ray ray = Camera.main.ScreenPointToRay(screenPosition);

                    if (Physics.Raycast(ray, out RaycastHit hitData, Mathf.Infinity, 1 << 6))
                    {

                        Block block = hitData.collider.GetComponent<Block>();
                        block.checkRayInput();

                    }
                
            }
            timer = 0;
        }
    }

    private void WhenWin()
    {
        LevelManager.Instance.ClearDataSaveGame();
        //WinEffect.SetActive(true);
        LevelManager.Instance.NextLevel();
    }



}
