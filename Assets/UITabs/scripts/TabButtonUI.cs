using UnityEngine ;
using UnityEngine.UI ;

public class TabButtonUI : MonoBehaviour {
    public Button uiButton ;
    public Image uiImage ;
    public Sprite[] sprtieImage;
    public LayoutElement uiLayoutElement ;
    private RectTransform reactTransform;
    private Vector2 preRT;
    private void Awake()
    {
        reactTransform = GetComponent<RectTransform>();
        preRT = new Vector2(reactTransform.sizeDelta.x, 100);
    }
    public void Active()
   {
        uiImage.sprite = sprtieImage[0];
      
        reactTransform.sizeDelta = new Vector2(preRT.x+5, preRT.y+20);
   }
    public void DisActive()
    {
        uiImage.sprite = sprtieImage[1];
       
        reactTransform.sizeDelta = preRT;
    }
}
