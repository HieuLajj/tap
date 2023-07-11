using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemUI : MonoBehaviour
{
    public Sprite[] spriteItem;
    public Image imagePlate;
    // Start is called before the first frame update
    public TextMeshProUGUI TextCoin;
    public GameObject Plate;
    public GameObject TextCoinPanel;
    public Image TargetPanel;

    public void Actived()
    {
        imagePlate.sprite = spriteItem[0];
        //Plate.gameObject.SetActive(false);
        TextCoinPanel.SetActive(false);
    }

    public void Activeting()
    {
        imagePlate.sprite = spriteItem[2];
        //Plate.gameObject.SetActive(true);
        TextCoinPanel.SetActive(true);
        TargetPanel.sprite = spriteItem[3];
    }

    public void ActiveAwait()
    {
        //Plate.gameObject.SetActive(true);
        imagePlate.sprite = spriteItem[1];
        TextCoinPanel.SetActive(false);
    }

    public void Active()
    {
        if (!TextCoinPanel.gameObject.activeInHierarchy)
        {
            TextCoinPanel.SetActive(true);
        }
        TargetPanel.sprite = spriteItem[4];
        imagePlate.sprite = spriteItem[0];

    }

}
