using UnityEngine;



[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScenarioSO")]

public class ScenarioSO: SORuntimeLoadable<ScenarioIds>
{
    public ScenarioNodeSO startNode;
    public string title;

    public ScenarioContext ToContext()
    {
        var context = new ScenarioContext();
        context.id = id;
        context.title = title;

        return context;
    }
}
