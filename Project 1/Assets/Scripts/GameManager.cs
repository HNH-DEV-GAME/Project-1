using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
    [SerializeField] private GameObject leaveText;
    public bool finalPhase = false;
    private PhotonView pv;
    //IN-GAME
    private int playerStillLive;
    //----
    //IN ROOM
    //---
    private string namePlayerWinner;
    private bool isFinished = false;
    private bool isChating = false;
    private void Start()
    {
        notificationText.text = "";
        notificationText.GetComponent<DOTweenAnimation>().enabled = false;
        pv = GetComponent<PhotonView>();
        playerStillLive = PhotonNetwork.CurrentRoom.PlayerCount;
        leaveText.SetActive(false);
    }
    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount <= 1)
        {
            PrintText("The Winner is " + pv.Owner.NickName, false, true, true);
        }
        if (Input.GetKeyDown(KeyCode.L) && isFinished)
        {
            LeaveRoom();
        }
    }
    [PunRPC]
    public void PrintText(string content,bool finalPhase,bool trigger, bool isFinished)
    {
        this.finalPhase = finalPhase;
        this.isFinished = isFinished;
        notificationText.text = content;
        if (finalPhase)
        {
            notificationText.transform.DOShakePosition(1,1, 47, 90, false, true).SetLoops(-1, LoopType.Restart);
        }
        else
        {
            notificationText.color = new Color32(59, 250, 0, 255);
        }
        leaveText.SetActive(trigger);
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
            pv.RPC("PrintText", RpcTarget.All, "The Winner is " + namePlayerWinner,false,true,true);  
            isFinished = true;
        }
        else
        {
            pv.RPC("PrintText", RpcTarget.All, "KILL THEM !!!",true,false,false);
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
    public bool GetIsFinished()
    {
        return isFinished;
    }
    public void SetIsChating(bool value)
    {
        isChating = value;
    }
    public bool GetIsChating()
    {
        return isChating;
    }
    public void DecreasePlayer()
    {
        playerStillLive--;
    }
    public int GetAmountOfPlayerStillLive()
    {
        return playerStillLive;
    }
}
