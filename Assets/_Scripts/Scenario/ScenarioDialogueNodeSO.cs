using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScenarioDialogueNodeSO")]
public class ScenarioDialogueNodeSO: ScenarioNodeSO
{
    public ScenarioNodeSO nextNode;
	public string dialogueStr;
	public override ScenarioNode GetNodeInstance()
	{
		return new ScenarioDialogueNode(this);
	}
    
}
