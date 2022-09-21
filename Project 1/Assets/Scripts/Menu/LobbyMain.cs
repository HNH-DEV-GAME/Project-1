using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class LobbyMain : MonoBehaviourPunCallbacks
{
    private PanelManager _panelManager;
    [SerializeField] private TMP_InputField inputFieldName;
    [SerializeField] private TMP_Text namePlayerDisplay;
    [SerializeField] private TMP_Text nameRoomDisplay;
    [SerializeField] private TMP_InputField inputFieldRoomName;
    [SerializeField] private TMP_InputField inputFieldAmountOfPlayer;
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private GameObject ListRoomPos;


    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject ListPlayerPos;

    [SerializeField] private GameObject buttonStart;

    private Dictionary<string,RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListEntries;
    private Dictionary<int, GameObject> playerListEntries;
    private void Awake()
    {
        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListEntries = new Dictionary<string,GameObject>();
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Start()
    {
        _panelManager = GetComponent<PanelManager>();
    }
    #region UNITY
    public void ClearRoomListView()
    {
        foreach (var room in roomListEntries.Values)
        {
            Destroy(room.gameObject);
        }
        roomListEntries.Clear();
    }
    public void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (var room in roomList)
        {
            if (!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(room.Name))
                {
                    cachedRoomList.Remove(room.Name);
                }
                continue;
            }
            if (cachedRoomList.ContainsKey(room.Name))
            {
                cachedRoomList[room.Name] = room;
            }
            else
            {
                cachedRoomList.Add(room.Name,room);
            }
        }
    }
    public void UpdateRoomListView()
    {
        foreach (var info in cachedRoomList)
        {
            GameObject roomGameObject = Instantiate(roomPrefab,ListRoomPos.transform);
            roomGameObject.GetComponent<Room>().SetInfoRoom(info.Value);
            roomListEntries.Add(info.Key, roomGameObject);
        }
    }
    #endregion

    #region PUN CALLBACKS
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
        print("Connected to server");
        _panelManager.PanelActive(Panel.TypePanel.LoadingPanel);
    }

    public override void OnJoinedLobby()
    {
        cachedRoomList.Clear();
        ClearRoomListView();
        _panelManager.PanelActive(Panel.TypePanel.LoginPanel);
        print("Joined to lobby");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        MasterClient(newMasterClient);
    }

    private void MasterClient(Player player)
    {
        if (!player.IsMasterClient)
        {
            buttonStart.SetActive(false);
        }
        else
        {
            buttonStart.SetActive(true);
        }
    }

    public override void OnLeftLobby()
    {
        cachedRoomList.Clear();
        ClearRoomListView();
    }
    public override void OnCreatedRoom()
    {
        print("Created Room Successfully");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Created Room failed that reason is "+message);
        _panelManager.PanelActive(Panel.TypePanel.CreateRoomPanel);
    }
    public override void OnJoinedRoom()
    {
        nameRoomDisplay.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        cachedRoomList.Clear();
        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<int, GameObject>();
        }
        foreach (var player in PhotonNetwork.PlayerList)
        {
            GameObject playerGameObject = Instantiate(playerPrefab, ListPlayerPos.transform);
            playerGameObject.GetComponent<PlayerList>().SetInfoPlayer(player);
            playerListEntries.Add(player.ActorNumber, playerGameObject);
            MasterClient(player);
        }
        _panelManager.PanelActive(Panel.TypePanel.InsideRoomPanel);
    }
    public override void OnLeftRoom()
    {
        nameRoomDisplay.text = "";
        foreach (var player in playerListEntries.Values)
        {
            Destroy(player.gameObject);
        }
        playerListEntries.Clear();
        playerListEntries = null;
        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (playerListEntries.ContainsKey(newPlayer.ActorNumber)) return;
        GameObject playerGameObject = Instantiate(playerPrefab,ListPlayerPos.transform);
        playerGameObject.GetComponent<PlayerList>().SetInfoPlayer(newPlayer);
        playerListEntries.Add(newPlayer.ActorNumber,playerGameObject);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (playerListEntries.ContainsKey(otherPlayer.ActorNumber))
        {
            Destroy(playerListEntries[otherPlayer.ActorNumber].gameObject);
            playerListEntries.Remove(otherPlayer.ActorNumber);
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomListView();
        UpdateCachedRoomList(roomList);
        UpdateRoomListView();
    }
    #endregion
    #region UI CALLBACKS
    public void LoginButton()
    {
        if (inputFieldName.text == "") return; 
        PhotonNetwork.NickName = inputFieldName.text;
        namePlayerDisplay.text = "Player Name: " +inputFieldName.text;
        _panelManager.PanelActive(Panel.TypePanel.SelectionPanel);
    }
    public void CreateRoomButton()
    {
        if (inputFieldAmountOfPlayer.text == "" || inputFieldRoomName.text == "") return;
        RoomOptions room = new RoomOptions();
        room.PlayerTtl = 2000;
        room.MaxPlayers = (byte)int.Parse(inputFieldAmountOfPlayer.text) < 4 ? (byte)int.Parse(inputFieldAmountOfPlayer.text) : (byte)4;
        PhotonNetwork.CreateRoom(inputFieldRoomName.text,room);
        nameRoomDisplay.text = "Room Name: " + inputFieldRoomName.text;
        _panelManager.PanelActive(Panel.TypePanel.LoadingPanel);
    }
    public void StartGameButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void JoinRandomRoomButton()
    {
        _panelManager.PanelActive(Panel.TypePanel.RandomRoomPanel);
    }
    public void ListRoomPanel()
    {
        _panelManager.PanelActive(Panel.TypePanel.ListRoomPanel);
    }
    public void ActiveCreateRoomPanel()
    {
        _panelManager.PanelActive(Panel.TypePanel.CreateRoomPanel);
    }
    public void LeaveRoomButton()
    {
        PhotonNetwork.LeaveRoom();
        print("Left current room");
    }
    public void CancelButton()
    {
        _panelManager.BackOnePanel();
    }
    #endregion
}
