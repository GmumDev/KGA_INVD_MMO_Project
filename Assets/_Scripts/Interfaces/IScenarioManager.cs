using UnityEngine;

public interface IScenarioManager
{
    bool IsPlaying { get; }
    bool PlayScenario(ScenarioIds scenarioId);
    void NextNode();
}
