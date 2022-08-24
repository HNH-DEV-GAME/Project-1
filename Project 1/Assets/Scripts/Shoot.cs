using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform point;
    private StarterAssetsInputs _input;
    private RigTranstion _rigTranstion;
    private Vector3 _direction;
    private void Start()
    {
        _rigTranstion = GetComponent<RigTranstion>();
        _input = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
             Vector3 directionBullet = (_direction - point.position).normalized;
            Transform bulletGameObject = Instantiate(bullet,point.position, Quaternion.LookRotation(directionBullet));        
        }
    }
    public void SetMousePos(Vector3 direction)
    {
        _direction = direction;
    }
}
