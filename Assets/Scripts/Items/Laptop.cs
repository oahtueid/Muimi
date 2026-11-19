using System;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : Item
{
    public override void Interact()
    {
        base.Interact();
        var actions = new List<(string, Action)>
        {
            (
                "Open",
                () =>
                {
                    UIManager.Instance.Show("LaptopUI");
                }
            ),
        };

        UIManager.Instance.ShowAction(actions, gameObject.transform.position);
    }
}
