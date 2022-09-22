using Photon.Pun;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // shot by this player
    private int idPlayer;
    private float timeToRemoveIdPlayer = 2f;
    private float countDown;
    private bool trigger = false;
    [SerializeField] private float heightToDie;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private Camera flyCamera;
    [SerializeField] private GameObject[] removePlayer;
    private PhotonView pv;
    private KillPanelManager killPanelManager;
    string killerName;
    string victimName;
    private bool isDied = false;
    private void Start()
    {
        idPlayer = -1;
        pv = GetComponent<PhotonView>();
        countDown = timeToRemoveIdPlayer;
        killPanelManager = FindObjectOfType<KillPanelManager>().GetComponent<KillPanelManager>();
        flyCamera.enabled = false;
    }
    public void Update()
    {  
        if (trigger)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                idPlayer = -1;
                trigger = false;
                countDown = timeToRemoveIdPlayer;
            }
        }
        if (transform.position.y < heightToDie)
        {
             if (idPlayer > 1)
            { 
                pv.RPC("HandleAfterKillPlayer", RpcTarget.All, PhotonView.Find(idPlayer).Owner.NickName);      
                idPlayer = -1;
            }
            if (GameManager.Instance.finalPhase == true)
            {
                GameManager.Instance.playerStillLive--;
                pv.RPC("RemoveBody",RpcTarget.All);
                if (!pv.IsMine) return;
                FindObjectOfType<DisplayPlayerName>().SetCamFly(flyCamera);
                FindObjectOfType<DisplayPlayerName>().switchCam = true;
                gameObject.GetComponent<CharacterController>().enabled = false;
                gameObject.GetComponent<ThirdPersonController>().enabled = false;
                gameObject.GetComponent<Shoot>().enabled = false;
                gameObject.GetComponent<CameraTranstion>().enabled = false;
                flyCamera.transform.localPosition = gameObject.transform.position;
                flyCamera.enabled = true;
                gameObject.GetComponent<PlayerManager>().enabled = false;
                isDied = true;
            }
            else
            {
                transform.position = new Vector3(11, 22, -15f);
            }
        }
        if (GameManager.Instance.GetIsChating())
        {
            gameObject.GetComponent<ThirdPersonController>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<ThirdPersonController>().enabled = true;
        }
    }
    [PunRPC]
    private void RemoveBody()
    {
        foreach (var model in removePlayer)
        {
            model.SetActive(false);
        }
    }

    [PunRPC]
    public void HandleAfterKillPlayer(string killerName)
    {
        killPanelManager.SpawnKillPanel(killerName, pv.Owner.NickName);
        if (pv.IsMine) return;
        PhotonView.Find(idPlayer).gameObject.GetComponent<GunManager>().UpgradeGun();
    }
    public bool GetIsDiedValue()
    {
        return isDied;
    }
    public string GetName()
    {
        return pv.Owner.NickName;
    }
    public void SetIDPlayerIsShooted(int id)
    {
        idPlayer = id;
        trigger = true;
    }
}
