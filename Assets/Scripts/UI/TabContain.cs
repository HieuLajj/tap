using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TabContain : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textNextLevel;
    public Image imageFill;
    public Image imageNotifi;
    public Sprite[] spriteImage;
    public GameObject NextPanelGameObject;
    private void OnEnable()
    {
        //text
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUp()
    {
        int flag = 0;
        if (Controller.Instance.DiffirentGame == DiffirentEnum.EASY)
        {
            flag = 0;
        }
        else if (Controller.Instance.DiffirentGame == DiffirentEnum.MEDIUM)
        {
            flag = 10;
        }
        else if (Controller.Instance.DiffirentGame == DiffirentEnum.HARD)
        {
            flag = 30;
        }
        if (Controller.Instance.DiffirentGame == DiffirentEnum.EASY)
        {
            textLevel.text = $"Levels 1 - {Controller.Instance.constantsDiffical[DiffirentEnum.EASY]}";
        }
        else if (Controller.Instance.DiffirentGame == DiffirentEnum.MEDIUM)
        {
            textLevel.text = $"Levels {Controller.Instance.constantsDiffical[DiffirentEnum.EASY] + 1} - {Controller.Instance.constantsDiffical[DiffirentEnum.MEDIUM]}";
        }
        else if (Controller.Instance.DiffirentGame == DiffirentEnum.HARD)
        {
            textLevel.text = $"Levels {Controller.Instance.constantsDiffical[DiffirentEnum.MEDIUM] + 1} - 300";
        }
        //Debug.Log((LevelManager.Instance.DataDiffical[Controller.Instance.DiffirentGame] - flag) / Controller.Instance.constantsDiffical[Controller.Instance.DiffirentGame]);
        float a = LevelManager.Instance.DataDiffical[Controller.Instance.DiffirentGame] - flag;
        float b = Controller.Instance.constantsDiffical[Controller.Instance.DiffirentGame];
        textNextLevel.text = $"Level {LevelManager.Instance.DataDiffical[Controller.Instance.DiffirentGame]}";
        imageFill.fillAmount = a/b;
        if (imageFill.fillAmount >= 1)
        {
            imageNotifi.sprite = spriteImage[0];
            NextPanelGameObject.SetActive(false);
        }
        else
        {
            imageNotifi.sprite = spriteImage[1];
            if (!NextPanelGameObject.activeInHierarchy)
            {
                NextPanelGameObject.SetActive(true);
            }
        }

    }

    public void CallLevelPanel()
    {
        gameObject.SetActive(false);
        TabsUIHorizontal.Instance.NumberLevel.gameObject.SetActive(true);
    }

    public void NextLevelEventButton()
    {
        LevelManager.Instance.LoadLevelInGame(LevelManager.Instance.DataDiffical[Controller.Instance.DiffirentGame]);
    }
}
