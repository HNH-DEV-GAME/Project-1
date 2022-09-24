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
    [SerializeField] private Transform[] spawnPos;
    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        pv = GetComponent<PhotonView>();
        foreach (var point in spawnPos)
        {
            point.GetComponent<MeshRenderer>().enabled = false;
        }
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
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            PhotonNetwork.Instantiate("Player",spawnPos[0].position, Quaternion.identity);
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
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
