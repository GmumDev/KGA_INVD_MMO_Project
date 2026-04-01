using UnityEngine;


public abstract class ScenarioNodeSO: ScriptableObject
{
	public int index;
	public ScenarioNodeType type;
	public abstract ScenarioNode GetNodeInstance();

}
