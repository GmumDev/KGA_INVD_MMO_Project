using UnityEngine;

public abstract class ScenarioNode
{

    protected ScenarioNodeType type { get; }
    public ScenarioNode(ScenarioNodeType type)
    {
        this.type = type;
    }
	public abstract void OnPlayed(IScenarioPlayer scenarioPlayer);
	public abstract void OnFinished(IScenarioPlayer scenarioPlayer);
    public abstract ScenarioNode GetNextNode();
}
