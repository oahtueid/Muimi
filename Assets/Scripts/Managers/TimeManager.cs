using System;
using UnityEngine;

public class TimeManager : PersistentManager<TimeManager>
{
    public int CurrentDay { get; private set; } = 1;
    public Session CurrentSession { get; private set; } = Session.Morning;
    public event Action<int, Session> OnUpdateTime;
    public DateTime today = new(2016, 10, 27);

    private void OnDayChange(int day)
    {
        CurrentDay = day;
        today.AddDays(1);

        Debug.Log("Current Day: " + CurrentDay);
    }

    private void OnSessionChange(Session session)
    {
        CurrentSession = session;
        Debug.Log("Current Session: " + CurrentSession);
    }

    public void AdvanceTime()
    {
        switch (CurrentSession)
        {
            case Session.Morning:
                OnSessionChange(Session.Afternoon);
                break;
            case Session.Afternoon:
                OnSessionChange(Session.Evening);
                break;
            case Session.Evening:
                OnSessionChange(Session.Night);
                break;
            case Session.Night:
                OnSessionChange(Session.Morning);
                OnDayChange(CurrentDay + 1);
                break;
        }
        OnUpdateTime?.Invoke(CurrentDay, CurrentSession);
    }

    public bool IsWeekend()
    {
        return today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday;
    }

    public bool IsDayTime()
    {
        return CurrentSession == Session.Morning || CurrentSession == Session.Afternoon;
    }

    public bool IsSchoolDay()
    {
        return !IsWeekend() && IsDayTime();
    }
}

public enum Session
{
    Morning,
    Afternoon,
    Evening,
    Night,
}
