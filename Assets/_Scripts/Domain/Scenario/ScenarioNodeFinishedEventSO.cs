using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Scenario/NodeFinishedEventSO")]
public class ScenarioNodeFinishedEventSO : ScriptableObject
{
    public ScenarioNodeFinishedEventType eventType;

    // Type: follow Up Quest
    public QuestIds followUpQuestId;

    public ScenarioNodeFinishedEvent ToEvent()
    {
        var obj = new ScenarioNodeFinishedEvent(
            eventType,
            followUpQuestId
        );

        return obj;
    }
}
