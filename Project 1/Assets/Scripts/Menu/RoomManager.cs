using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class RoomManager : MonoBehaviourPunCallbacks
{
    #region Singleton 
    private static RoomManager _instance;
    public static RoomManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        pv = GetComponent<PhotonView>();
    }
    #endregion
    PhotonView pv;
    private PanelManager _panelManager;
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {    
            PhotonNetwork.Instantiate("Player", new Vector3(1, 1, 1), Quaternion.identity);
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
       SceneManager.LoadScene(0);
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }


}
