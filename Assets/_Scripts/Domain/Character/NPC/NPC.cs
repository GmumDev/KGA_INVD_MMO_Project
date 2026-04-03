using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    NPCSO npcSo;

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
        scenarioManager.PlayScenario(npcSo.scenarioId);

        return InteractionType.PlayScenario;
    }
}
