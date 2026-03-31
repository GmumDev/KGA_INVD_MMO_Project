using UnityEngine;

public abstract class QuestConditionSO : ScriptableObject
{
	public string id;
    public QuestConditionType type;
	public abstract QuestCondition GetConditionInstance();
}
