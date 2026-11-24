using System;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Item
{
    Sleep sleepEvent;

    public void Awake()
    {
        sleepEvent = gameObject.AddComponent<Sleep>();
    }

    public override void Interact()
    {
        base.Interact();
        var actions = new List<(string, Action)>
        {
            (
                "Sleep",
                () =>
                {
                    sleepEvent.Execute();
                }
            ),
        };

        UIManager.Instance.ShowAction(actions, gameObject.transform.position);
    }
}
