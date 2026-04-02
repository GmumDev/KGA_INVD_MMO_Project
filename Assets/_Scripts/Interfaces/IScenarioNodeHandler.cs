using UnityEngine;

public interface IScenarioNodeHandler
{
    void PlayAsFirstNode(IScenarioContextRunner scenarioPlayer, ScenarioNodeContext context);
    void PlayAsNextNode(IScenarioContextRunner scenarioPlayer, ScenarioNodeContext context);
    void FisishNode(IScenarioContextRunner scenarioPlayer, ScenarioNodeContext context);
}
