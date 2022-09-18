using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayPlayerName : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    [SerializeField] private PhotonView pv;
    [SerializeField] private TMP_Text text;
    void Awake()
    {
        pv = gameObject.GetComponentInParent<PhotonView>();
    }
    private void Start()
    {
        if (pv.IsMine)
        {
            gameObject.SetActive(false);
        }
        text.text = pv.Owner.NickName;
    }

    void Update()
    {
        if (cam == null)
            cam = FindObjectOfType<CinemachineVirtualCamera>();

        if (cam == null)
            return;

        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 180);
    }
}
