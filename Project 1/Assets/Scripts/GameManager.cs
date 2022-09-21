using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region SINGLETON
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private Button leaveButton;
    public bool finalPhase = false;
    private PhotonView pv;
    public int playerStillLive;
    private string namePlayerWinner;
    private bool isFinished = false;
    private void Start()
    {
        notificationText.text = "";
        pv = GetComponent<PhotonView>();
        playerStillLive = PhotonNetwork.CurrentRoom.PlayerCount;
        leaveButton.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            leaveButton.onClick.Invoke();
        }
    }
    [PunRPC]
    public void PrintText(string content,bool finalPhase)
    {
        notificationText.text = content;
        this.finalPhase = finalPhase;
    }
    public void FinalPhase()
    {
        if (playerStillLive == 1)
        {
            foreach (var player in FindObjectsOfType<PlayerManager>())
            {
                if (!player.GetComponent<PlayerManager>().GetIsDiedValue())
                {
                    namePlayerWinner = player.GetComponent<PlayerManager>().GetName();
                }
            }
            pv.RPC("PrintText", RpcTarget.All, "The Winner is " + namePlayerWinner,false);
            leaveButton.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            pv.RPC("PrintText", RpcTarget.All, "KILL THEMM !!!",true);
        }
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene(0);
    }
}
