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
public enum DiffirentEnum
{
    EASY,
    MEDIUM,
    HARD
}
public class Controller : Singleton<Controller>
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    private float timer = 0f;
    public UIManager manager;

    public ParticleSystem WinPS;
    public List<IListenerBlock> ListenerBlock = new List<IListenerBlock>();
    public int amountNumberBlock = 0;
    public int checkloadBlock = 0;
    private StateGame stateGame = StateGame.AWAIT;
    public GameObject Boom;

    public DiffirentEnum DiffirentGame = DiffirentEnum.EASY;
    
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
    public readonly Dictionary<DiffirentEnum, int> constantsDiffical = new Dictionary<DiffirentEnum, int>()
    {
        { DiffirentEnum.EASY, 10 },
        { DiffirentEnum.MEDIUM, 20 },
        { DiffirentEnum.HARD, 30 }
    };
    private void Awake()
    {
        Application.targetFrameRate = 60;
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
        if (stateGame == StateGame.PLAY)
        {
            userInteraction();
        }
    }
 
    private void LateUpdate()
    {
        //if(stateGame == StateGame.PLAY) {
        //    userInteraction();
        //}
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

        // kiem tra xem mang co toan -1 hay khong
        bool checkminus1 = false;
        foreach(var item in ListenerBlock)
        {
            arraytest[i] = item.IType();
            if (arraytest[i] != -1 && !checkminus1) {
                checkminus1 = true;
            }
            i++;
        }
        if (checkminus1)
        {
            LevelManager.Instance.SaveGame(arraytest);
        }
        else
        {
            LevelManager.Instance.ClearDataSaveGame();
        }
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
        //if (!WinPS.isPlaying)
        //{
        //    WinPS.Play();
        //}
        //Invoke("AwaitNext",2.0f);
        AwaitNext();
    }
    public void AwaitNext()
    {
        Debug.Log("?");
        UIManager.Instance.CompleteLevelUI.SetActive(true);
    }
    //IEnumerator CheckTimeParticle()
    //{
    //    float time = 0;
    //    while (WinPS.isPlaying && time<3)
    //    {
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //    UIManager.Instance.CompleteLevelUI.SetActive(true);
    //}







}
