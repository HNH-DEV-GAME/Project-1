using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;


public class LobbyMain : MonoBehaviourPunCallbacks
{
    private PanelManager _panelManager;
    [SerializeField] private TMP_InputField inputFieldName;
    [SerializeField] private TMP_Text nameDisplay;
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
        PhotonNetwork.NickName = inputFieldName.text;
        nameDisplay.text = "Name: " +inputFieldName.text;
        _panelManager.PanelActive(Panel.TypePanel.SelectionPanel);
    }
    #endregion
}
