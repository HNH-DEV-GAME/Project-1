using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGamObject : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Hide",0.3f);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
