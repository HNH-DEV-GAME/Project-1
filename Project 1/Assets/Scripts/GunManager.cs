using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] private ScriptableObjectGun data;
    [SerializeField] private List<Transform> guns;
    [SerializeField] private Transform gunParent;
    [SerializeField] private int levelGun;
    private void Start()
    {
        levelGun = 0;
        for (int i=0;i < gunParent.childCount;i++)
        {
            guns.Add(gunParent.GetChild(i).gameObject.transform);
        }
        foreach (Transform gun in guns)
        {
            gun.gameObject.SetActive(false);
        }
        guns[levelGun].gameObject.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            levelGun++;
            UpgradeGunLevel();
        }
    }

    private void UpgradeGunLevel()
    {
        if (levelGun < guns.Count)
        {
            guns[levelGun - 1].gameObject.SetActive(false);
            guns[levelGun].gameObject.SetActive(true);
        }
        else
        {
            levelGun = guns.Count;
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
