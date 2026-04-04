using UnityEngine;

public class ScenarioDialogueNodeHandler : IScenarioNodeHandler
{
    void publishNodeEvent(IScenarioContextRunner ctxRunner, ScenarioNodeContext context)
    {
        ScenarioNodePlayedEvent ev = context.scenarioNodePlayedEventSO?.ToEvent();

        if (ev != null && ev.eventId != ScenarioNodePlayedEventIds.None)
        {
            EventBus.Publish(ev);
        }

        ctxRunner.DoDialogue(context);
    }
    void IScenarioNodeHandler.FisishNode(IScenarioContextRunner ctxRunner, ScenarioNodeContext context)
    {
        ctxRunner.ClearDialogue();
    }

    void IScenarioNodeHandler.PlayAsFirstNode(IScenarioContextRunner ctxRunner, ScenarioNodeContext context)
    {
        publishNodeEvent(ctxRunner, context);
        ctxRunner.DoDialogue(context);
    }

    void IScenarioNodeHandler.PlayAsNextNode(IScenarioContextRunner ctxRunner, ScenarioNodeContext context)
    {
        publishNodeEvent(ctxRunner, context);
        ctxRunner.DoDialogue(context);
    }
}
