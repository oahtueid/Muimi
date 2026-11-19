using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EventWithLocationQueue
{
    public Queue<IEvent> gameEvents;
    public Location location;
}

public enum Location
{
    School,
    Home,
}

public class EventManager : PersistentManager<EventManager>
{
    Dictionary<Location, EventWithLocationQueue> registeredEvents;

    void Awake()
    {
        registeredEvents = new();
    }

    public void RegisterEvent(Location location, IEvent gameEvent)
    {
        Debug.Log("IEvent: " + gameEvent);
        Debug.Log("Location: " + location);
        if (!registeredEvents.ContainsKey(location))
        {
            registeredEvents[location] = new EventWithLocationQueue
            {
                location = location,
                gameEvents = new(),
            };
        }
        Debug.Log("RegisteredEvent " + registeredEvents[location]);
        registeredEvents[location].gameEvents.Enqueue(gameEvent);
    }

    public void TriggerEvent(IEvent gameEvent)
    {
        if (gameEvent.IsConditionMet())
        {
            gameEvent.Execute();
        }
    }

    public void RunNextEvent(Location location)
    {
        if (
            registeredEvents.TryGetValue(location, out EventWithLocationQueue queue)
            && queue.gameEvents.Count > 0
        )
        {
            IEvent gameEvent = queue.gameEvents.Dequeue();
            TriggerEvent(gameEvent);
        }
        else
        {
            Debug.Log("No more event");
        }
    }
}
