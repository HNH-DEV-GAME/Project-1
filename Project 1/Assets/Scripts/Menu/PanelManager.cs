using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    private List<Panel> panels = new List<Panel>();
    private void Start()
    {
        foreach (Panel panel in FindObjectsOfType<Panel>())
        {
            panels.Add(panel);
        }
    }

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
}
