using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestSO")]

public class QuestSO: ScriptableObject
{
	public QuestIds id;
	public QuestConditionSO[] conditions;
	public QuestRewardSO[] rewards;

	public string title;
	public string descript;
}
