using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text amountText;

    public void SetInfoRoom(RoomInfo room)
    {
        nameText.text = room.Name;
        amountText.text = room.PlayerCount.ToString() +"/"+room.MaxPlayers;
    }
    public void JoinRoomButton()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }

        PhotonNetwork.JoinRoom(nameText.text);
    }
}
