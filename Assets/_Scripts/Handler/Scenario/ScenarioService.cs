using System.Collections.Generic;
using UnityEngine;

public static class ScenarioService
{
    private static readonly Dictionary<ScenarioNodeType, IScenarioNodeHandler> handlers
        = new Dictionary<ScenarioNodeType, IScenarioNodeHandler>()
        {
            {ScenarioNodeType.Dialogue, new ScenarioDialogueNodeHandler() }
        };

    public static void FinishNode(IScenarioContextRunner ctxRunner, ScenarioNodeContext context)
    {
        handlers[context.type].FisishNode(ctxRunner, context);
    }
    public static void PlayAsFirstNode(IScenarioContextRunner ctxRunner, ScenarioNodeContext context)
    {
        handlers[context.type].PlayAsFirstNode(ctxRunner, context);
    }
    public static void PlayAsNextNode(IScenarioContextRunner ctxRunner, ScenarioNodeContext context)
    {
        handlers[context.type].PlayAsNextNode(ctxRunner, context);
    }

}
