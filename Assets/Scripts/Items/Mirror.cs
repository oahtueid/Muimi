using System.Collections;
using UnityEngine;

public class Mirror : Item
{
    static bool isFirstTime = true;
    [SerializeField] GameObject scene2Trigger;

    public override void Interact()
    {
        if (isFirstTime)
        {
            // TODO: show main character face in mirror timeline
            ShowFace();
            scene2Trigger.SetActive(true);
        }
        else
            Debug.Log("Not first time");
    }

    private void ShowFace()
    {
        isFirstTime = false;
    }
}
