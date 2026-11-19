using System.Collections.Generic;
using UnityEngine;

public class InputStateManager : PersistentManager<InputStateManager>
{
    Stack<IInputState> stateHistory = new();
    public IInputState CurrentState => stateHistory.Count > 0 ? stateHistory.Peek() : null;

    public void SetState(IInputState newState)
    {
        Debug.Log("current state: " + CurrentState);
        CurrentState?.Exit();
        stateHistory.Clear();
        stateHistory.Push(newState);
        CurrentState?.Enter();
    }

    public void PushState(IInputState newState)
    {
        CurrentState?.Exit();
        stateHistory.Push(newState);
        CurrentState?.Enter();
    }

    public void PopState()
    {
        if (stateHistory.Count == 0)
            return;
        CurrentState?.Exit();
        stateHistory.Pop();
        CurrentState?.Enter();
    }

    public void Disable()
    {
        stateHistory.Peek()?.Exit();
    }

    public void Enable()
    {
        CurrentState?.Enter();
    }
}
