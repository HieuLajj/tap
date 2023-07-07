using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject Boom;
    public GameObject UIGift;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveBoom()
    {
        if (Boom.activeInHierarchy)
        {
            Boom.SetActive(false);
        }
        else
        {
            Boom.SetActive(true);
        }
    }

    public void OpenGift()
    {
        UIManager.Instance.BlockGiftPresent.gift.GetComponent<Animator>().SetBool("GiftOpen", true);
    }

    public void ClearGift()
    {
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
