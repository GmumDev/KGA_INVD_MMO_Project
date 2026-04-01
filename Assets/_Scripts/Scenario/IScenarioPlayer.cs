using UnityEngine;

public interface IScenarioPlayer
{
    bool IsPlaying { get; }
    void PlayScenario(ScenarioIds scenarioId);
    void NextNode();
}
