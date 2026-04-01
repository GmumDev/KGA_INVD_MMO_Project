using UnityEngine;


public abstract class ScenarioNodeSO: ScriptableObject
{
	public ScenarioNodeType type;
    public ScenarioNodeSO nextNode;
    public abstract ScenarioNodeContext ToContext();

}
