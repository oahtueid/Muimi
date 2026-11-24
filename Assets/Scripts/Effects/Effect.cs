using System;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Effect : MonoBehaviour
{
    [SerializeField]
    protected float duration = 2f;

    void Start() { }

    void Update() { }

    public abstract void StartEffect(Action onComplete = null);
    public abstract void StopEffect();
}
