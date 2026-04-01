using UnityEngine;


public abstract class ScenarioNodeSO: ScriptableObject
{
	public ScenarioNodeIds id;
	public ScenarioNodeType type;
	public abstract ScenarioNode GetNodeInstance();

}
