using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GunManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private ScriptableObjectGun data;
    [SerializeField] private List<Transform> guns;
    [SerializeField] private Transform gunParent;
    [SerializeField] private int levelGun;
    private Transform point;
    private PhotonView pv;
    public bool trigger = false;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        for (int i = 0; i < gunParent.childCount; i++)
        {
            guns.Add(gunParent.GetChild(i).gameObject.transform);
        }
        foreach (Transform gun in guns)
        {
            gun.gameObject.SetActive(false);
        }
        guns[levelGun].gameObject.SetActive(true);
        point = guns[levelGun].GetComponent<Gun>().GetPointPos();
        data = guns[levelGun].GetComponent<Gun>().GetDataGun();
    }
    private void Update()
    {
        if (!pv.IsMine) return;
        if (Input.GetKeyDown(KeyCode.U) || trigger == true)
        {
            UpgradeGun();
            trigger = false;
        }
    }
    public void UpgradeGun()
    {
        if (levelGun > guns.Count) { return; }
        levelGun++;
        Hashtable hash = new Hashtable();
        hash.Add("GunLevel", levelGun);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (pv.Owner == targetPlayer)
        {
            UpgradeGunLevel((int)changedProps["GunLevel"]);
        }
    }
    private void UpgradeGunLevel(int levelGun)
    {
        guns[levelGun - 1].gameObject.SetActive(false);
        guns[levelGun].gameObject.SetActive(true);
        point = guns[levelGun].GetComponent<Gun>().GetPointPos();
        data = guns[levelGun].GetComponent<Gun>().GetDataGun();
    }
    public float GetDelayTimeShoot()
    {
        return data.delayTimeShoot;
    }
    public int GetForceValue()
    {
        return data.force;
    }
    public Transform GetPointPos()
    {
        return point;
    }
}
