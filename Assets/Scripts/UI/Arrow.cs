using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Arrow : MonoBehaviour
{
    public GiftUIManager giftUIManager;
    public TextMeshProUGUI textX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RewardNo"))
        {
            //var multipier = other.gameObject.name;
            int numberReward = other.gameObject.GetComponent<TextRewardItem>().number;
            giftUIManager.RewardTMP.text = "Get X" + numberReward;
            textX.text = $"+{numberReward* UIManager.Instance.giftUIManager.RandomReward}";
        }
        else
        {
           // Debug.Log(other.gameObject.name);
        }
    }

}