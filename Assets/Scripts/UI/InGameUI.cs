using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
  
    //public GameObject Boom;
    public GameObject UIGift;
    public GameObject CoinsUI;

   
    public void ActiveBoom()
    {
        if (Controller.Instance.Boom.activeInHierarchy)
        {
            Controller.Instance.Boom.SetActive(false);
        }
        else
        {
            Controller.Instance.Boom.SetActive(true);
        }
    }

    public void OpenGift()
    {
        UIManager.Instance.BlockGiftPresent.gift.GetComponent<Animator>().SetBool("GiftOpen", true);
    }

    public void ClearGift()
    {
        LevelManager.Instance.pretransform.localScale = new Vector3(1,1,1);
        UIManager.Instance.BlockGiftPresent.gift.gameObject.SetActive(false);
        UIManager.Instance.BlockGiftPresent.gift.transform.parent = GiftPooling.Instance.transform;
        UIManager.Instance.BlockGiftPresent.gift = null;
        UIManager.Instance.BlockGiftPresent.gameObject.SetActive(false);

        Controller.Instance.gameState = StateGame.PLAY;
        UIGift.gameObject.SetActive(false);

        // check win
        LevelManager.Instance.CheckWin();
    }
}
