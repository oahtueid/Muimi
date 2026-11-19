using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : PersistentManager<UIManager>
{
    private Dictionary<string, GameObject> panels;
    public ActionPanel actionPanel;

    private void Awake()
    {
        panels = new();
        foreach (Transform child in transform)
        {
            panels[child.name] = child.gameObject;
            child.gameObject.SetActive(false);
        }
    }

    public void Show(string panelName)
    {
        if (panels.ContainsKey(panelName))
        {
            Debug.Log("showing panel " + panelName);
            panels[panelName].SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Panel {panelName} không tồn tại!");
        }
    }

    public void Hide(string panelName)
    {
        if (panels.ContainsKey(panelName))
        {
            panels[panelName].SetActive(false);
        }
    }

    public void HideAll()
    {
        foreach (var panel in panels.Values)
        {
            panel.SetActive(false);
        }
    }

    public void ShowAction(List<(string, Action)> actions, Vector3 worldPosition)
    {
        actionPanel.gameObject.SetActive(true);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        actionPanel.transform.position = screenPos;
        Debug.Log("Panel pos: " + actionPanel.transform.position);

        actionPanel.Show(actions);
    }

    public void HideAction()
    {
        actionPanel.gameObject.SetActive(false);
    }
}
