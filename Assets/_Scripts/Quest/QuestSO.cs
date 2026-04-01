using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestSO")]

public class QuestSO: SORuntimeLoadable<QuestIds>
{
	public QuestConditionSO[] conditions;
	public QuestRewardSO[] rewards;

	public string title;
	public string descript;
}
