using System;
using UnityEngine;

public class ScenarioNodePlayedEvent
{
    public ScenarioNodePlayedEventType eventtype;

    public ScenarioNodePlayedEvent(ScenarioNodePlayedEventType eventtype)
    {
        this.eventtype = eventtype;
    }
}
