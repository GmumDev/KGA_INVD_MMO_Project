using UnityEngine;

public abstract class QuestRewardSO : ScriptableObject
{
	public QuestRewardIds id;
	public QuestRewardType type;
    public abstract QuestRewardContext ToContext();
}