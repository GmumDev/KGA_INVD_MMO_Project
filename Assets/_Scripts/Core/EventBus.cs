using System;
using System.Collections.Generic;
using UnityEngine;
public class SubscriptionToken
{
    public Type eventType;
    public Action<object> action;
}
public static class EventBus
{
    private static Dictionary<Type, Action<object>> events = new Dictionary<Type, Action<object>>();

    public static SubscriptionToken Subscribe<T>(Action<T> listener)
    {
        Type type = typeof(T);

        Action<object> wrapper = (e) => listener((T)e);
        
        if (!events.ContainsKey(type))
            events[type] = null;

        events[type] += wrapper;

        return new SubscriptionToken
        {
            eventType = type,
            action = wrapper
        };
    }
	static void Wrap<T>(object e, Action<T> listener) => listener((T)e);
    public static void Unsubscribe(SubscriptionToken token)
    {
        if (events.ContainsKey(token.eventType))
        {
            events[token.eventType] -= token.action;
        }
    }
    public static void Publish<T>(T e)
    {
        Type type = typeof(T);

        if (events.ContainsKey(type))
            events[type]?.Invoke(e);
    }

    public static void Clear()
    {
        events.Clear();
    }
}