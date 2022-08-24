using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PositionGunAnimation", order = 1)]
public class ScriptableObjectGunPos : ScriptableObject
{
    public float name;
    public Vector3 position;
    public Vector3 Rotation;
}
