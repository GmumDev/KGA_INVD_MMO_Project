using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NPCSO")]
public class NPCSO: ScriptableObject
{
    public string npcName;
    public ScenarioIds scenarioId;
}
