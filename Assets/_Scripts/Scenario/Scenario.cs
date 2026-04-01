using UnityEngine;

public class Scenario
{
    ScenarioIds id;
	ScenarioNode startNode;
	ScenarioNode curNode;
    public Scenario(ScenarioSO so)
    {
        this.id = so.id;

		this.startNode = so.startNode.GetNodeInstance();
        this.curNode = startNode;
        
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
    public void ResetScenario()
    {
        this.curNode = startNode;
    }
}
