using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuTransitionEventBus
{
    private static readonly IDictionary<UIMenu, UnityEvent> Events = new Dictionary<UIMenu, UnityEvent>();

    public static void Subscribe(UIMenu menuType, UnityAction listener)
    {
        UnityEvent thisEvent;
        if(Events.TryGetValue(menuType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener); //gain subscriber
            Events.Add(menuType, thisEvent);
        }
    }
    public static void Unsubscribe(UIMenu menuType, UnityAction listener)
    {
        UnityEvent thisEvent;
        if(Events.TryGetValue(menuType, out thisEvent))
        {
            thisEvent.RemoveListener(listener); //lose subscriber
        }
    }
    public static void Publish(UIMenu type)
    {
        UnityEvent thisEvent;
        if(Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke(); //tell listener to do the thing
        }
    }
}
