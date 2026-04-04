using UnityEngine;


public abstract class ScenarioNodeSO: ScriptableObject
{
    public ScenarioNodeSO nextNode;
    public ScenarioNodePlayedEventSO scenarioNodePlayedEventSO;
    public ScenarioNodeFinishedEventSO scenarioNodeFinishedEventSO;
    public abstract ScenarioNodeContext ToContext();
    
}
