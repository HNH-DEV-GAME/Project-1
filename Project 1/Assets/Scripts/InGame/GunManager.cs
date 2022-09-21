using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GunManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private ScriptableObjectGun data;
    [SerializeField] private List<Transform> guns;
    [SerializeField] private Transform gunParent;
    [SerializeField] private int levelGun;
    [SerializeField] private TMP_Text levelGunText;
    private Transform point;
    private PhotonView pv;
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
        if (!pv.IsMine)
        {
            levelGunText.text = "";
            return;
        }
        levelGunText.text = "LEVEL " + (levelGun + 1).ToString();
    }
    private void Update()
    {
        if (!pv.IsMine) return;
        if (levelGun >= guns.Count)
        {
            GameManager.Instance.FinalPhase();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpgradeGun();
        }
    }
    public void UpgradeGun()
    {
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
        if (levelGun >= guns.Count)
        {
            return;
        }
        guns[levelGun - 1].gameObject.SetActive(false);
        guns[levelGun].gameObject.SetActive(true);
        point = guns[levelGun].GetComponent<Gun>().GetPointPos();
        data = guns[levelGun].GetComponent<Gun>().GetDataGun();
        if (!pv.IsMine)
        {
            levelGunText.text = "";
            return;
        }
        levelGunText.text = "LEVEL " + (levelGun + 1).ToString();
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
    public int GetLevelGun()
    {
        return levelGun;
    }
}
