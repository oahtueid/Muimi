using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : PersistentManager<InputHandler>
{
    public PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.UI.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        controls.UI.Disable();
    }

    public void SwitchActionMaps(params InputActionMap[] targetMaps)
    {
        controls.Disable();
        foreach (var map in targetMaps)
        {
            map?.Enable();
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
            controls.Dispose();
    }
}
