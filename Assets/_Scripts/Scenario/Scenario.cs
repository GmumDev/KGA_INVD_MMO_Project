using UnityEngine;

public class Scenario
{
    ScenarioIds id;
    ScenarioNode curNode;
    public Scenario(ScenarioSO so)
    {
        this.id = so.id;

        this.curNode = so.startNode.GetNodeInstance();
        
    }
    public bool PlayCurrentNode(IScenarioPlayer scenarioPlayer)
	{
		if (curNode == null)
			return false;
		curNode.OnPlayed(scenarioPlayer);
		return true;
	}
    public bool PlayNextNode(IScenarioPlayer scenarioPlayer)
	{
		if (curNode == null)
			return false;
		curNode.OnFinished(scenarioPlayer);
        curNode = curNode.GetNextNode();

        return PlayCurrentNode(scenarioPlayer);
    }
}
