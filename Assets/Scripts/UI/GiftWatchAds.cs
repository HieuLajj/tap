using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftWatchAds : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UIBoom;
    private void OnEnable()
    {
        UIBoom.SetActive(false);
    }

    private void OnDisable()
    {
        UIBoom.SetActive(true);
    }
}
