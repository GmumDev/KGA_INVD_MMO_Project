using UnityEngine;



[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScenarioSO")]

public class ScenarioSO: SORuntimeLoadable<ScenarioIds>
{
    public ScenarioNodeSO startNode;
}
