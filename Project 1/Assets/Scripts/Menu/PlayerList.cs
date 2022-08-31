using UnityEngine;
using TMPro;
using Photon.Realtime;

public class PlayerList : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    //[SerializeField] private TMP_Text amountText;

    public void SetInfoPlayer(Player info)
    {
        nameText.text = info.NickName;
    }
}
