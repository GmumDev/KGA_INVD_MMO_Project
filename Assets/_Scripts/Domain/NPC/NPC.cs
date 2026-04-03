using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    ScenarioIds scenarioId;

    static IQuestManager questManager;
	static IScenarioManager scenarioManager;

	void Start()
    {
        if(questManager == null)
        {
            questManager = QuestManager.Instance;
        }
        if(scenarioManager == null)
        {
            scenarioManager = ScenarioManager.Instance;
		}
    }

	public InteractionType OnInteract()
    {
        scenarioManager.PlayScenario(scenarioId);

        return InteractionType.PlayScenario;
    }
}
