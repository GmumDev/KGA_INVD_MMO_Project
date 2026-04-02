using UnityEngine;

public abstract class QuestConditionSO : ScriptableObject
{
	public QuestConditionType type;

	public abstract QuestConditionContext ToContext();
}
