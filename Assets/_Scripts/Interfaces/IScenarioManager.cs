using UnityEngine;

public interface IScenarioManager
{
    bool IsPlaying { get; }
    void PlayScenario(ScenarioIds scenarioId);
    void NextNode();
}
