using UnityEngine;

public abstract class ScenarioNode
{
    ScenarioNodeIds id;
    ScenarioNodeType type;

    public ScenarioNode(ScenarioNodeIds id, ScenarioNodeType type)
    {
        this.id = id;
        this.type = type;
    }
}
