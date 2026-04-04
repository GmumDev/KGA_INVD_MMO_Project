using UnityEngine;

public class ScenarioDialogueNodeHandler : IScenarioNodeHandler
{
    void IScenarioNodeHandler.FisishNode(IScenarioContextRunner ctxRunner, ScenarioNodeContext context)
    {
        ctxRunner.ClearDialogue();
    }

    void IScenarioNodeHandler.PlayAsFirstNode(IScenarioContextRunner ctxRunner, ScenarioNodeContext context)
    {
        ctxRunner.DoDialogue(context);
    }

    void IScenarioNodeHandler.PlayAsNextNode(IScenarioContextRunner ctxRunner, ScenarioNodeContext context)
    {
        ctxRunner.DoDialogue(context);
    }
}
