using UnityEngine;

public class QuestAcceptedEvent
{
    public QuestIds questId;
    public QuestAcceptedEvent(QuestIds id)
    {
        this.questId = id;
    }
}
