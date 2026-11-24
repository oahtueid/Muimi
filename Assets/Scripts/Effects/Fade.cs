using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : Effect
{
    private static readonly int START_HASH = Animator.StringToHash("Start");
    public Animator transition;
    [SerializeField] Image image;
    void Start()
    {
        image.enabled = false;
    }

    // Update is called once per frame
    void Update() { }

    public override void StartEffect(Action onComplete = null)
    {
        image.enabled = true;
        StartCoroutine(StartTransition(onComplete));
    }

    public override void StopEffect()
    {
        image.enabled = false;
    }

    IEnumerator StartTransition(Action onComplete = null)
    {
       transition.SetTrigger(START_HASH);
       yield return new WaitForSeconds(duration);
       onComplete?.Invoke();
    }
}
