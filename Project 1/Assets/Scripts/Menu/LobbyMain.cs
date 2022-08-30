using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class LobbyMain : MonoBehaviourPunCallbacks
{
    private PanelManager _panelManager;
    [SerializeField] private TMP_InputField inputFieldName;
    [SerializeField] private TMP_Text nameDisplay;
    [SerializeField] private TMP_InputField inputFieldRoomName;
    [SerializeField] private TMP_InputField inputFieldAmountOfPlayer;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Start()
    {
        _panelManager = GetComponent<PanelManager>();
    }
    #region UNITY

    #endregion

    #region PUN CALLBACKS
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        print("Connected to server");
        _panelManager.PanelActive(Panel.TypePanel.LoadingPanel);
    }
    public override void OnJoinedLobby()
    {
        _panelManager.PanelActive(Panel.TypePanel.LoginPanel);
        print("Joined to lobby");
    }
    #endregion
    #region UI CALLBACKS
    public void LoginButton()
    {
        if (inputFieldName.text == "") return; 
        PhotonNetwork.NickName = inputFieldName.text;
        nameDisplay.text = "Name: " +inputFieldName.text;
        _panelManager.PanelActive(Panel.TypePanel.SelectionPanel);
    }
    public void CreateRoomButton()
    {
        if (inputFieldAmountOfPlayer.text == "" || inputFieldRoomName.text == "") return;
        RoomOptions room = new RoomOptions();
        room.PlayerTtl = 2000;
        room.MaxPlayers = (byte)int.Parse(inputFieldAmountOfPlayer.text) < 4 ? (byte)int.Parse(inputFieldAmountOfPlayer.text) : (byte)4;
        PhotonNetwork.CreateRoom(inputFieldRoomName.text,room);
    }
    public void ActiveCreateRoomPanel()
    {
        _panelManager.PanelActive(Panel.TypePanel.CreateRoomPanel);
    }
    public void CancelButton()
    {
        _panelManager.BackOnePanel();
    }
    #endregion
}
