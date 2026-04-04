using UnityEngine;



[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Scenario/ScenarioSO")]

public class ScenarioSO: SORuntimeLoadable<ScenarioIds>
{
    public ScenarioNodeSO startNode;
    public string title;
    public bool unloadDataOnScenarioFinished;

    public ScenarioContext ToContext()
    {
        var context = new ScenarioContext();
        context.id = id;
        context.title = title;
        context.unloadDataOnScenarioFinished = unloadDataOnScenarioFinished;

        return context;
    }
}
