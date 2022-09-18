using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // shot by this player
    private int idPlayer;
    private float timeToRemoveIdPlayer = 4f;
    private float countDown;
    private bool trigger = false;
    [SerializeField] private float heightToDie;
    [SerializeField] private Transform[] spawnPoint;
    private PhotonView pv;

    private void Start()
    {
        idPlayer = -1;
        pv = GetComponent<PhotonView>();
        countDown = timeToRemoveIdPlayer;
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
                PhotonView.Find(idPlayer).gameObject.GetComponent<GunManager>().trigger = true;
            }
            transform.position = new Vector3(11, 22, -15f);
            idPlayer = -1;
        }
    }
    public void SetIDPlayerIsShooted(int id)
    {
        idPlayer = id;
        trigger = true;
    }
}
