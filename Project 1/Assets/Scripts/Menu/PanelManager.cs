using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PanelType
{
    public Panel panel;
    public int level;
}
public class PanelManager : MonoBehaviour
{
    private static PanelManager _instance;
    public static PanelManager Instance
    { 
        get { return _instance; }
        set { _instance = value; }
    }
    [Tooltip("Must be to correct order")]
    [SerializeField] private List<PanelType> panels; // must to correct order

    public void PanelActive(Panel.TypePanel typePanel)
    {
        foreach (PanelType panel in panels)
        {
            if (panel.panel.GetComponent<Panel>().GetTypePanel() != typePanel)
            {
                panel.panel.gameObject.SetActive(false);
            }
            else
            {
                panel.panel.gameObject.SetActive(true);
            }
        }
    }
    public void BackOnePanel()
    {
        foreach (PanelType panel in panels)
        {
            if (panel.panel.gameObject.activeSelf == true)
            {
                panel.panel.gameObject.SetActive(false);
                panels[panels.FindIndex(x => x.level == panel.level - 1)].panel.gameObject.SetActive(true);
                break;
            }
        }
    }
}

