using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Photon.Pun;
public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform point;
    [SerializeField] private Transform muzzleEffect;
    private StarterAssetsInputs _input;
    private RigTranstion _rigTranstion;
    private GunManager dataGun;
    private Animator _ani;

    private Vector3 _direction;
    private float tempCountDown;
    private PhotonView photonView;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        dataGun = GetComponent<GunManager>();
        _rigTranstion = GetComponent<RigTranstion>();
        _input = GetComponent<StarterAssetsInputs>();
        _ani = GetComponent<Animator>();
        tempCountDown = dataGun.GetDelayTimeShoot();
        point = dataGun.GetPointPos();
    }
    private void Update()
    {
        point = dataGun.GetPointPos();
        if (!photonView.IsMine) return;
        if (Input.GetMouseButton(0) && tempCountDown <= 0)
        {
            Vector3 directionBullet = (_direction - point.position).normalized;
            photonView.RPC("RPC_SpawnMuzzleEffect", RpcTarget.All, directionBullet);
            tempCountDown = dataGun.GetDelayTimeShoot();
            _ani.SetBool("Shoot",true);
        }else
        {
            tempCountDown -= Time.deltaTime;
            _ani.SetBool("Shoot",false);
        }
    }
    [PunRPC]
    public void RPC_SpawnMuzzleEffect(Vector3 directionBullet)
    {
        Instantiate(muzzleEffect,point.position,Quaternion.LookRotation(directionBullet));
        Transform bulletGameObject = Instantiate(bullet, point.position, Quaternion.LookRotation(directionBullet));
        bulletGameObject.GetComponent<Bullet>().SetForceValue(dataGun.GetForceValue());
        bulletGameObject.GetComponent<Bullet>().SetIDPlayer(photonView.ViewID);
    }
    public void SetMousePos(Vector3 direction)
    {
        _direction = direction;
    }
}
