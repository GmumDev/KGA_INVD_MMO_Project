using UnityEngine;

public class ScenarioNodeContext
{
    public ScenarioNodeType type;

    // dialogue
    public ScenarioNodeType nextNodeType;
    public string speakerStr;
	public string dialogueStr;
    // dialogue with event
    public ScenarioNodePlayedEventSO scenarioNodePlayedEventSO;
    public ScenarioNodeFinishedEventSO scenarioNodeFinishedEventSO;



}
