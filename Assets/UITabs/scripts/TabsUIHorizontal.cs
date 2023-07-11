using UnityEngine;
using UnityEngine.UI;
using EasyUI.Tabs;
using Unity.VisualScripting;
using DG.Tweening.Core.Easing;

public class TabsUIHorizontal : TabsUI
{
    #if UNITY_EDITOR
    private void Reset() {
        OnValidate();
    }
    private void OnValidate() {
        base.Validate(TabsType.Horizontal);
    }
#endif
    private static TabsUIHorizontal instance;
    public static TabsUIHorizontal Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TabsUIHorizontal>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public NumberLevelManager numberLevelManager;
    public GameObject NumberLevel;
    private void Awake()
    {
        numberLevelManager = transform.GetComponent<NumberLevelManager>();
    }
    public override void OnTabButtonClicked(int tabIndex)
    {
        if (current != tabIndex)
        {
            if (OnTabChange != null)
                OnTabChange.Invoke(tabIndex);
            if (NumberLevel.activeInHierarchy)
            {
                NumberLevel.gameObject.SetActive(false);
            }
            

            previous = current;
            current = tabIndex;

            tabContent[previous].SetActive(false);
            tabContent[current].SetActive(true);

            tabBtns[previous].uiImage.color = tabColorInactive;
            tabBtns[current].uiImage.color = tabColorActive;

            tabBtns[previous].uiButton.interactable = true;
            tabBtns[current].uiButton.interactable = false;

            numberLevelManager.ActiveLoadLevel(tabIndex);

          
            tabBtns[previous].uiButton.GetComponent<TabButtonUI>().DisActive();
            tabBtns[current].uiButton.GetComponent<TabButtonUI>().Active();
            tabContent[current].GetComponent<TabContain>().SetUp();

            //numberLevelManager.gameObject.SetActive(false);

        }
    }
}
