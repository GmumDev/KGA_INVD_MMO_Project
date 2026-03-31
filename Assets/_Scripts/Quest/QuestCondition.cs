using UnityEngine;

public abstract class QuestCondition
{
	protected QuestConditionType type { get; }
	public abstract bool IsMet(IQuestRunner runner);
	public QuestCondition(QuestConditionType type)
	{
		this.type = type;
	}
}
