using System;
using UnityEngine;

public class ScenarioNodeFinishedEvent
{
    public ScenarioNodeFinishedEventType eventType;

    public QuestIds followUpQuestId;

    public ScenarioNodeFinishedEvent(ScenarioNodeFinishedEventType eventType, QuestIds followUpQuestId)
    {
        this.eventType = eventType;
        this.followUpQuestId = followUpQuestId;
    }
}
