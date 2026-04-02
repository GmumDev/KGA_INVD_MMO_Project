using UnityEngine;

public interface IScenarioContextRunner
{
    void DoDialogue(ScenarioNodeContext ctx);
    void ClearDialogue();
}
