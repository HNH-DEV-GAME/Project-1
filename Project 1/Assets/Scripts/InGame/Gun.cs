using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Gun : MonoBehaviour
{
    [SerializeField] private RigBuilder rig;
    [SerializeField] private TwoBoneIKConstraint rightHandBone;
    [SerializeField] private TwoBoneIKConstraint leftHandBone;
    [SerializeField] private Transform rightHandTarget;
    [SerializeField] private Transform leftHandTarget;
    [SerializeField] private Transform point;
    [SerializeField] private ScriptableObjectGun data;
    private void Start()
    {
        rightHandBone.data.target = rightHandTarget;
        leftHandBone.data.target = leftHandTarget;
        rig.Build();
    }
    public Transform GetPointPos()
    {
        return point;
    }
    public ScriptableObjectGun GetDataGun()
    {
        return data;
    }
}
