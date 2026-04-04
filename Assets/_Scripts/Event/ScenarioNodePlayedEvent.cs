using System;
using UnityEngine;

public class ScenarioNodePlayedEvent
{
    public ScenarioNodePlayedEventIds eventId;

    public ScenarioNodePlayedEvent(ScenarioNodePlayedEventIds eventId)
    {
        this.eventId = eventId;
    }
}
