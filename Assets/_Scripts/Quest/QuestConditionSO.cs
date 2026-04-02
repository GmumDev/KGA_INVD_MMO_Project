using UnityEngine;

public abstract class QuestConditionSO : ScriptableObject
{
	public QuestConditionIds id;
	public QuestConditionType type;

	public abstract QuestConditionContext ToContext();
}
