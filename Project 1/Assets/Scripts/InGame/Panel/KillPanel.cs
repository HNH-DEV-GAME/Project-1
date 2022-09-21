using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillPanel : MonoBehaviour
{
    public TMP_Text killerText;
    public TMP_Text victimText;

    public void Start()
    {
        Destroy(gameObject,2f);
    }
}
