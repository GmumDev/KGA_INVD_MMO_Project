using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestSO")]

public class QuestSO: ScriptableObject
{
	public string id;
	public QuestConditionSO[] conditions;
	public QuestRewardSO[] rewards;

	public string title;
	public string descript;
}
