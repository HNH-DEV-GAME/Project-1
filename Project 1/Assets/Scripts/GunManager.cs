using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] private ScriptableObjectGun data;
    [SerializeField] private List<Transform> gun;
    [SerializeField] private Transform gunParent;
    private void Start()
    {
        for (int i=0;i < gunParent.childCount;i++)
        {
            gun.Add(gunParent.GetChild(i).gameObject.transform);
        }
    }

    public float GetDelayTimeShoot()
    {
        return data.delayTimeShoot;
    }
    public int GetForceValue()
    {
        return data.force;
    }
}
