using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScenarioDialogueNodeSO")]
public class ScenarioDialogueNodeSO: ScenarioNodeSO
{
    public string speakerStr;
	public string dialogueStr;

    public override ScenarioNodeContext ToContext()
    {
        var context = new ScenarioNodeContext();
        context.speakerStr = speakerStr;
        context.type = type;
        context.dialogueStr = dialogueStr;

        return context;
    }
    
}
