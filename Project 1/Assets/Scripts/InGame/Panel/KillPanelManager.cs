using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject KillPanelPrefab;
    [SerializeField] private Transform content;

    public void SpawnKillPanel(string killer,string victim)
    {
        GameObject killPanelGO = Instantiate(KillPanelPrefab,content.position,Quaternion.identity);
        killPanelGO.transform.SetParent(content);
        killPanelGO.GetComponent<KillPanel>().killerText.text = killer;
        killPanelGO.GetComponent<KillPanel>().victimText.text = victim;
    }
}
