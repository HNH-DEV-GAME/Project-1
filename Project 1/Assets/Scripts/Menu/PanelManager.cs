using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [Tooltip("Must be to correct order")]
    [SerializeField] private List<Panel> panels; // must to correct order

    public void PanelActive(Panel.TypePanel typePanel)
    {
        foreach (Panel panel in panels)
        {
            if (panel.GetComponent<Panel>().GetTypePanel() != typePanel)
            {
                panel.gameObject.SetActive(false);
            }
            else
            {
                panel.gameObject.SetActive(true);
            }
        }
    }
    public void BackOnePanel()
    {
        foreach (Panel panel in panels)
        {
            if (panel.gameObject.activeSelf == true)
            {
                panel.gameObject.SetActive(false);
                panels[panels.FindIndex(x => x.typePanel == panel.typePanel) - 1].gameObject.SetActive(true);
                break;
            }
        }
    }
}

