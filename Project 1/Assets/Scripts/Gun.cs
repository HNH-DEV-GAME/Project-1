using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private ScriptableObjectGun data;  

    public float GetDelayTimeShoot()
    {
        return data.delayTimeShoot;
    }
    public int GetForceValue()
    {
        return data.force;
    }
}
