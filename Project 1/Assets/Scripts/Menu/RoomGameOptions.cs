using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomGameOptions : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown mapOption;
    [SerializeField] private TMP_Dropdown typeOption;
    [SerializeField] private Image mapImage;

    public int GetIndexMap()
    {
        return mapOption.value;
    }
}
