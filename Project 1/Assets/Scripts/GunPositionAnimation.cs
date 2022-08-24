using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPositionAnimation : MonoBehaviour
{
    [SerializeField] private ScriptableObjectGunPos[] data;
    private RigTranstion rigTranstion;

    private void Start()
    {
        rigTranstion = GameObject.FindGameObjectWithTag("Player").GetComponent<RigTranstion>();

    }
    private void Update()
    {
        if (rigTranstion._stateCharacter.ToString() == RigTranstion.StateCharacter.hasGunShoot.ToString())
        {
            transform.localPosition = data[1].position;
            transform.localRotation = Quaternion.Euler(data[1].Rotation.x, data[1].Rotation.y, data[1].Rotation.z);
        }
        else if(rigTranstion._stateCharacter.ToString() == RigTranstion.StateCharacter.hasGunRun.ToString())
        {
            transform.localPosition = data[0].position;
            transform.localRotation = Quaternion.Euler(data[0].Rotation.x, data[0].Rotation.y, data[0].Rotation.z);
        }
    }
}
