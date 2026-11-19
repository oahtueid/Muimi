using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEvent
{
}
public record LocationEnteredEvent(string locationId) : IGameEvent;
public record ItemInteractedEvent(string itemId) : IGameEvent;
public record ObjectiveCompletedEvent(string objectiveId) : IGameEvent;
public record MinigameFinishedEvent(string minigameId) : IGameEvent;
public record DialogueFinishedEvent(string dialogueName) : IGameEvent; // TODO: find way to define or get current story name
public static class EventBus
{
    public static readonly Dictionary<Type, Delegate> listeners = new();
    public static void Subcribe<T> (Action<T> listener) where T : IGameEvent
    {
        Type type = typeof(T);
        if (listeners.ContainsKey(type))
        {
            listeners[type] = Delegate.Combine(listeners[type], listener);
        }else
        {
            listeners[type] = listener;
        }
    }
    public static void UnSubcribe<T>(Action<T> listener) where T : IGameEvent
    {
        Type type = typeof(T);
        if (listeners.ContainsKey(type))
        {
            listeners[type] = Delegate.Remove(listeners[type], listener);
        }
    }
    public static void Publish<T>(T e) where T : IGameEvent
    {
        Type type = typeof(T);
        if(listeners.TryGetValue(type, out Delegate del)){
            if(del is Action<T> callback)
            {
                callback.Invoke(e);
            }
        }
    }
}
 namespace System.Runtime.CompilerServices
    {
          internal static class IsExternalInit {}
    }