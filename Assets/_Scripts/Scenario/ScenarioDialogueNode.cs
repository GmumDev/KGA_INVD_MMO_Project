using UnityEngine;

public class ScenarioDialogueNode : ScenarioNode
{
	ScenarioDialogueNodeSO nodeSO;
	public ScenarioDialogueNode(ScenarioDialogueNodeSO so) 
		: base(ScenarioNodeType.Dialogue)
	{
		this.nodeSO = so;
	}
	public override ScenarioNode GetNextNode()
	{
		return nodeSO.nextNode.GetNodeInstance();
	}
	public override void OnFinished(IScenarioPlayer scenarioPlayer)
	{
		scenarioPlayer.ClearDialogue();
	}

	public override void OnPlayed(IScenarioPlayer scenarioPlayer)
	{
		scenarioPlayer.DoDialogue(this);
	}
}
