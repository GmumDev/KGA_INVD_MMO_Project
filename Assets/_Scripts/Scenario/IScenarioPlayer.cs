using UnityEngine;

public interface IScenarioPlayer
{
    void Play(ScenarioNodeIds id);
    void Next();
}
