using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform point;
    [SerializeField] private Transform muzzleEffect;
    private StarterAssetsInputs _input;
    private RigTranstion _rigTranstion;
    private Gun dataGun;
    private Animator _ani;

    private Vector3 _direction;
    private float tempCountDown;
    private void Start()
    {
        dataGun = GetComponent<Gun>();
        _rigTranstion = GetComponent<RigTranstion>();
        _input = GetComponent<StarterAssetsInputs>();
        _ani = GetComponent<Animator>();
        tempCountDown = dataGun.GetDelayTimeShoot();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && tempCountDown <= 0)
        {
             Vector3 directionBullet = (_direction - point.position).normalized;
            Transform bulletGameObject = Instantiate(bullet,point.position, Quaternion.LookRotation(directionBullet));
            Instantiate(muzzleEffect, point.position, Quaternion.LookRotation(directionBullet));
            bulletGameObject.GetComponent<Bullet>().SetForceValue(dataGun.GetForceValue());
            tempCountDown = dataGun.GetDelayTimeShoot();
            _ani.SetBool("Shoot",true);
        }else
        {
            tempCountDown -= Time.deltaTime;
            _ani.SetBool("Shoot",false);
        }
    }
    public void SetMousePos(Vector3 direction)
    {
        _direction = direction;
    }
}
