using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GunStat", order = 1)]
public class ScriptableObjectGun : ScriptableObject
{
    public float delayTimeShoot;
    public int force;
}
